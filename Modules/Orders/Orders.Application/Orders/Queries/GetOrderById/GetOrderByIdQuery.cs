using MediatR;
using Orders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;
}
