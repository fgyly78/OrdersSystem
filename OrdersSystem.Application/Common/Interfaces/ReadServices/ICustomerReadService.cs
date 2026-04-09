using OrdersSystem.Application.Products.Queries.GetCustomerProducts;

namespace OrdersSystem.Application.Common.Interfaces.ReadServices
{
    public interface ICustomerReadService
    {
        Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(
            Guid customerId, CancellationToken ct);
    }
}
