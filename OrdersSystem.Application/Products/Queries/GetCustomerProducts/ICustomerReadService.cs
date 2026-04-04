using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries.GetCustomerProducts
{
    public interface ICustomerReadService
    {
        Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(
            Guid customerId, CancellationToken ct);
    }
}
