using MediatR;
using Orders.Application.Orders.Queries.GetOrderById;
using Orders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerQueryHandler : IRequestHandler<GetOrderByCustomerQuery, List<OrderDto>>
    {
        private readonly IOrderReadService _orderReadService;

        public GetOrderByCustomerQueryHandler(IOrderReadService orderReadService)
        {
            _orderReadService = orderReadService;
        }

        public async Task<List<OrderDto>> Handle(GetOrderByCustomerQuery query, CancellationToken ct)
        {
            var ordersDto = await _orderReadService.GetByCustomerAsync(new CustomerId(query.CustomerId), ct);

            if (ordersDto is null) throw new DomainException("Orders.Api not found");

            return ordersDto;
        }
    }
}
