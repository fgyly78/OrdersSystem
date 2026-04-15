using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Customers;

namespace Orders.Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
}
