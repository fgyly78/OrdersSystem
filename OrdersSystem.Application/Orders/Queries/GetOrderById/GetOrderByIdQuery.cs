using MediatR;
using OrdersSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;
}
