
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
        var cacheKey = $"order{orderId}";
        
        var cached = await _cacheService.GetAsync<OrderDto>(cacheKey, ct);
        if (cached is not null) return cached;

        var order = await _orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return null;

        var dto = OrderMapper.MapToDto(order);
        
        await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(5), ct);

        return dto;
    }
}