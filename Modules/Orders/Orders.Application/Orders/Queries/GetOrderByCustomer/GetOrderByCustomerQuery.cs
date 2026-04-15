using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrderByCustomerQuery(Guid CustomerId) : IRequest<List<OrderDto>>;
}
