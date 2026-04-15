
using Orders.Application.Orders.Queries;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Common.Interfaces.ReadServices;

public interface IOrderReadService
{
    Task<OrderDto?> GetByIdAsync(OrderId orderId, CancellationToken ct = default);
    Task<List<OrderDto?>> GetByCustomerAsync(CustomerId customerId, CancellationToken ct = default);
}