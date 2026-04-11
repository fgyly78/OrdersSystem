using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Events;

namespace OrdersSystem.Application.Orders.EventHandlers;

public class OrderCancelEventHandler
    : INotificationHandler<OrderCancelledEvent>
{
   private readonly ICacheService _cacheService;

   public OrderCancelEventHandler(ICacheService cacheService)
   {
       _cacheService = cacheService;
   }

    public async Task Handle(OrderCancelledEvent notification, CancellationToken ct)
    {
        await Task.WhenAll(
            _cacheService.RemoveAsync($"order:{notification.OrderId}", ct),
            _cacheService.RemoveAsync($"customer:{notification.CustomerId}", ct)
        );
    }
}

