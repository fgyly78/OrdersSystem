using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Customers;

namespace OrdersSystem.Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
}
