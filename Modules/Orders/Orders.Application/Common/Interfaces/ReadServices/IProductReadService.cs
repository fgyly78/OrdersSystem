using Orders.Application.Common.Dtos.Queries.Products;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Common.Interfaces.ReadServices;

public interface IProductReadService
{
    Task<ProductDto?> GetByIdAsync(ProductId productId, CancellationToken ct = default);
    Task<List<ProductDto>> GetAvailableAsync(CancellationToken ct = default);
}