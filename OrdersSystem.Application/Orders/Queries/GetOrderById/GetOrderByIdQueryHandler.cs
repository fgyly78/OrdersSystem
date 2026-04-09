using MediatR;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Interfaces.ReadServices;

namespace OrdersSystem.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderReadService _orderReadService;

        public GetOrderByIdQueryHandler(IOrderReadService orderReadService)
        {
            _orderReadService = orderReadService;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken ct)
        {
            var orderDto = await _orderReadService.GetByIdAsync(new OrderId(query.OrderId), ct);

            if (orderDto is null) throw new DomainException("Order not found");

            return orderDto;
        }
    }
}
