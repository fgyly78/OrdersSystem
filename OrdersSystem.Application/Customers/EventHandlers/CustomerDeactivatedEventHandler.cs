using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Events;

namespace OrdersSystem.Application.Customers.EventHandlers;

public class CustomerDeactivatedEventHandler : INotificationHandler<CustomerDeactivatedEvent>
{
    private readonly ICacheService _cacheService;

    public CustomerDeactivatedEventHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public async Task Handle(CustomerDeactivatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            _cacheService.RemoveAsync($"customer:{notification.CustomerId}"));
    }
}