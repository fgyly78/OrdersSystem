using MediatR;
using OrdersSystem.Application.Orders.Queries.GetOrderById;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Interfaces.ReadServices; 

namespace OrdersSystem.Application.Orders.Queries.GetOrderByCustomer
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

            if (ordersDto is null) throw new DomainException("Orders not found");

            return ordersDto;
        }
    }
}
