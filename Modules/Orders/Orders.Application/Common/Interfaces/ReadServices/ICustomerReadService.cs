using Orders.Application.Common.Dtos.Queries.Customers;
using Orders.Application.Products.Queries.GetCustomerProducts;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Common.Interfaces.ReadServices
{
    public interface ICustomerReadService
    {
        Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(
            Guid customerId, CancellationToken ct);
        
        Task<CustomerDto> GetByIdAsync(CustomerId customerId, CancellationToken ct);
    }
}
