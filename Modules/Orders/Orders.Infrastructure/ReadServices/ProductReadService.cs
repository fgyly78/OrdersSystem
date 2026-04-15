using Orders.Application.Common.Dtos.Queries.Products;
using Orders.Application.Common.Interfaces;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Application.Common.Mapping;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.ReadServices;

public class ProductReadService : IProductReadService
{
    private readonly ICacheService _cacheService;
    private readonly IProductRepository _productRepository;

    public ProductReadService(ICacheService cacheService, IProductRepository productRepository)
    {
        _cacheService = cacheService;
        _productRepository = productRepository;
    }
    
    public async Task<ProductDto?> GetByIdAsync(ProductId productId, CancellationToken ct = default)
    {
        return await _cacheService.GetOrCreateAsync(
            $"product:{productId}",
            async () =>
            {
                var product = await _productRepository.GetByIdAsync(productId, ct);
                return ProductMapper.MapToDto(product);
            },
            TimeSpan.FromMinutes(5),
            ct);
    }

    public async Task<List<ProductDto>> GetAvailableAsync(CancellationToken ct = default)
    {
        return await _cacheService.GetOrCreateAsync(
            "products:available",
            async () =>
            {
                var products = await _productRepository.GetAvailiableAsync();
                return ProductMapper.MapToDtos(products.ToList());
            },
            TimeSpan.FromMinutes(10),
            ct);
    }
}