
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Application.Common.Interfaces.ReadServices;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using OrdersSystem.Application.Common.Mapping;
using OrdersSystem.Application.Orders.Queries;


namespace OrdersSystem.Infrastructure.ReadServices;

public class OrderReadService : IOrderReadService
{
    private readonly ICacheService _cacheService;
    private readonly IOrderRepository _orderRepository;

    public OrderReadService(ICacheService cacheService, IOrderRepository orderRepository)
    {
        _cacheService = cacheService;
        _orderRepository = orderRepository;
    }
    
    public async Task<OrderDto?> GetByIdAsync(OrderId orderId, CancellationToken ct = default)
    {
        return await _cacheService.GetOrCreateAsync(
            $"order:{orderId}",
            async () =>
            {
                var order = await _orderRepository.GetByIdAsync(orderId, ct);
                return OrderMapper.MapToDto(order);
            },
            TimeSpan.FromMinutes(5),
            ct);
    }

    public async Task<List<OrderDto>> GetByCustomerAsync(CustomerId customerId, CancellationToken ct = default)
    {
        return await _cacheService.GetOrCreateAsync(
            $"orders-customer:{customerId}",
            async () =>
            {
                var orders = await _orderRepository.GetByCustomerIdAsync(customerId, ct);
                return OrderMapper.MapToDtos(orders.ToList());
            },
            TimeSpan.FromMinutes(5),
            ct);
    }
}