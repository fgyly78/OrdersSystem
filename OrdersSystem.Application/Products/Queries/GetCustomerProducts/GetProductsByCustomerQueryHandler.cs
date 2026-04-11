using MediatR;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Interfaces.ReadServices;

namespace OrdersSystem.Application.Products.Queries.GetCustomerProducts
{
    public class GetProductsByCustomerQueryHandler : IRequestHandler<GetProductsBuCustomerQuery, List<CustomerProductSummaryDto>>
    {
        private readonly ICustomerReadService _customerReadService;

        public GetProductsByCustomerQueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<List<CustomerProductSummaryDto>> Handle(GetProductsBuCustomerQuery request, CancellationToken ct)
        {
            var orders = await _customerReadService.GetCustomerProductsAsync(request.CustomerId, ct);
            if (orders is null || !orders.Any()) throw new DomainException("Orders not found");
            return orders;
        }
    }
}
