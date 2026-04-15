using MediatR;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Events;

namespace Orders.Application.Orders.EventHandlers;

public class OrderPaidEventHandler :
        INotificationHandler<OrderPaidEvent>
{
    private readonly ICacheService _cacheService;
    
    public OrderPaidEventHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Handle(OrderPaidEvent notification, CancellationToken ct)
    {
        await Task.WhenAll(
            _cacheService.RemoveAsync($"order:{notification.OrderId}"),
            _cacheService.RemoveAsync($"customer:{notification.CustomerId}"));
    }
}