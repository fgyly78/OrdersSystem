using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries.GetCustomerProducts
{
    public record GetCustomerProductsQuery(Guid CustomerId) : IRequest<List<CustomerProductSummaryDto>>;

}
