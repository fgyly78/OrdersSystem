using Contracts.Events;
using Contracts.Messaging.Queues;
using MediatR;
using Orders.Application.Common.Interfaces.Messaging;
using Orders.Domain.Events;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Orders.EventHandlers;

public class OrderCreatedDomainEventHandler 
    : INotificationHandler<OrderCreatedEvent>
{
    private readonly IMessageBus _messageBus;

    public OrderCreatedDomainEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    
    public async Task Handle(OrderCreatedEvent notification, CancellationToken ct)
    {
        await _messageBus.PublishAsync(QueueNames.OrderCreated,
            new OrderCreatedIntegrationEvent(notification.OrderId, notification.CreatedAt) , ct);
    }
}