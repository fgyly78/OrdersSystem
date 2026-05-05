using Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Common.Interfaces.Messaging;

namespace Infrastructure.Integration_Event_Handlers;

public class OrderCreatedIntegrationEventHandler
{
    private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;

    public OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger)
    {
        _logger =  logger;
    }
    
    public Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing order {OrderId}", notification.OrderId);
        return Task.CompletedTask;
    }
}