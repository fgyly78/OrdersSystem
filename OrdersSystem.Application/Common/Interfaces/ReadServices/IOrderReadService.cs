
using OrdersSystem.Application.Orders.Queries;
using OrdersSystem.Domain.ValueObjects;

namespace OrdersSystem.Application.Common.Interfaces.ReadServices;

public interface IOrderReadService
{
    Task<OrderDto?> GetByIdAsync(OrderId orderId, CancellationToken ct = default);
    Task<List<OrderDto?>> GetByCustomerAsync(CustomerId customerId, CancellationToken ct = default);
}