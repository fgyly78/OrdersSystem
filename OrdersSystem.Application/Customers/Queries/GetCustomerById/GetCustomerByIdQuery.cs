using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
}
