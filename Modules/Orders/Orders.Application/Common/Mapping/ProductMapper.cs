using Orders.Application.Common.Dtos.Queries.Products;
using Orders.Domain.Entities;
using Orders.Application.Common.Dtos.Queries.Customers;

namespace Orders.Application.Common.Mapping;

public class ProductMapper
{
    public static ProductDto MapToDto(Product product) => new ProductDto
    {
        Id = product.Id.Value,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price.Amount,
        StockQuantity = product.StockQuantity,
        IsAvailable = product.IsAvailable
    };   
    
    public static List<ProductDto> MapToDtos(List<Product> products) => products.Select(p => MapToDto(p)).ToList();
}