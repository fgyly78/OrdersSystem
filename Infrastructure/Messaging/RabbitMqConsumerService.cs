using System.Text;
using Contracts.Messaging.Configs;
using Contracts.Messaging.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Infrastructure.Messaging;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly IConfiguration _config;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqConsumerService(IConfiguration config)
    {
        _config = config;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
        while (!stoppingToken.IsCancellationRequested)
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
                await Task.Delay(3000, stoppingToken);
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

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received: {message}");
        };

        _channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);

        // держим сервис живым
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

}