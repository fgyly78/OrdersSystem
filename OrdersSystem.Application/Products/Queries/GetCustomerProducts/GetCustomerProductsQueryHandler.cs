using MediatR;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries.GetCustomerProducts
{
    public class GetCustomerProductsQueryHandler : IRequestHandler<GetCustomerProductsQuery, List<CustomerProductSummaryDto>>
    {
        private readonly ICustomerReadService _customerReadService;

        public GetCustomerProductsQueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<List<CustomerProductSummaryDto>> Handle(GetCustomerProductsQuery request, CancellationToken ct)
        {
            var orders = await _customerReadService.GetCustomerProductsAsync(request.CustomerId, ct);
            if (orders is null || !orders.Any()) throw new DomainException("Orders not found");
            return orders;
        }
    }
}
