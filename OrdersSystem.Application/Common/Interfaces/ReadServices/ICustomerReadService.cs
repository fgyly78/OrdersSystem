using OrdersSystem.Application.Common.Dtos.Queries.Customers;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
using OrdersSystem.Domain.ValueObjects;

namespace OrdersSystem.Application.Common.Interfaces.ReadServices
{
    public interface ICustomerReadService
    {
        Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(
            Guid customerId, CancellationToken ct);
        
        Task<CustomerDto> GetByIdAsync(CustomerId customerId, CancellationToken ct);
    }
}
