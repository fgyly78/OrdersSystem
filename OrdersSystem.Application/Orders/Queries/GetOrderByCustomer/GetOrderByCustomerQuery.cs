using MediatR;
using OrdersSystem.Application.Orders.Queries.GetOrderById;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrderByCustomerQuery(Guid CustomerId) : IRequest<List<OrderDto>>;
}
