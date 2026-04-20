using System.Text;
using System.Text.Json;
using Contracts.Messaging.Configs;
using Contracts.Messaging.Queues;
using Microsoft.Extensions.Configuration;
using Orders.Application.Common.Interfaces.Messaging;
using RabbitMQ.Client;


namespace Infrastructure.Messaging;

public class RabbitMqMessageBus : IMessageBus
{
    private readonly IConnection _connection;
    
    public RabbitMqMessageBus(IConfiguration config)
    {
        var host = config["RabbitMq:Host"];
        var port = int.Parse(config["RabbitMq:Port"]!);
        var user = config["RabbitMq:User"];
        var pass = config["RabbitMq:Password"];
        
        var factory = new ConnectionFactory()
        {
            HostName = host,
            Port = port,
            UserName = user,
            Password = pass
        };
        
        _connection = factory.CreateConnection();
    }
    
    public Task PublishAsync<T>(string queue, T message, CancellationToken ct = default)
    {
        using var channel = _connection.CreateModel();
        
        var properties = channel.CreateBasicProperties();
        
        channel.QueueDeclare(
            queue,
            durable: QueueConfig.Durable,
            exclusive: QueueConfig.Exclusive,
            autoDelete: QueueConfig.AutoDelete,
            arguments: null);
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _connection?.Dispose();
    }
}