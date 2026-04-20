using Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Common.Interfaces.Messaging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        services.AddHostedService<RabbitMqConsumerService>();
        services.AddScoped<IMessageBus, RabbitMqMessageBus>();

        return services;
    }
}
