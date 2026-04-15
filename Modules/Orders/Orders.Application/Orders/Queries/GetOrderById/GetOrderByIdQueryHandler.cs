using MediatR;
using Orders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Orders.Queries.GetOrderById
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
