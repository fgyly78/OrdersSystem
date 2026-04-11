using OrdersSystem.Application.Common.Dtos.Queries.Products;
using OrdersSystem.Domain.ValueObjects;

namespace OrdersSystem.Application.Common.Interfaces.ReadServices;

public interface IProductReadService
{
    Task<ProductDto?> GetByIdAsync(ProductId productId, CancellationToken ct = default);
    Task<List<ProductDto>> GetAvailableAsync(CancellationToken ct = default);
}