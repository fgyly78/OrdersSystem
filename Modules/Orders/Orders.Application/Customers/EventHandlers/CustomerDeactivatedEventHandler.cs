using MediatR;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Events;

namespace Orders.Application.Customers.EventHandlers;

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