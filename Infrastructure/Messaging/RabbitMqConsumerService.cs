using System.Text;
using System.Text.Json;
using Contracts.Events;
using Contracts.Messaging.Configs;
using Contracts.Messaging.Queues;
using Infrastructure.Integration_Event_Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orders.Domain.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Infrastructure.Messaging;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly IConfiguration _config;
    private IConnection _connection;
    private IModel _channel;
    private readonly OrderCreatedIntegrationEventHandler _handler;

    public RabbitMqConsumerService(IConfiguration config,  OrderCreatedIntegrationEventHandler handler)
    {
        _config = config;
        _handler = handler;
    }
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var host = _config["RabbitMq:Host"];
        var port = int.Parse(_config["RabbitMq:Port"]!);
        var user = _config["RabbitMq:User"];
        var pass = _config["RabbitMq:Password"];

        var factory = new ConnectionFactory()
        {
            HostName = host,
            Port = port,
            UserName = user,
            Password = pass
        };

        // retry loop
        while (!ct.IsCancellationRequested)
        {
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                break;
            }
            catch
            {
                Console.WriteLine("RabbitMQ not ready, retrying...");
                await Task.Delay(3000, ct);
            }
        }

        // ВАЖНО: защита
        if (_channel == null)
            throw new Exception("RabbitMQ channel not initialized");

        var queueName = QueueNames.OrderCreated;

        _channel.QueueDeclare(
            queue: queueName,
            durable: QueueConfig.Durable,
            exclusive: QueueConfig.Exclusive,
            autoDelete: QueueConfig.AutoDelete,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var evt = JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(message);

                if (evt is null) return;

                await _handler.Handle(evt, ct);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);

        // держим сервис живым
        await Task.Delay(Timeout.Infinite, ct);
    }

}