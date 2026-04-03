using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Commands.RegisterCustomer
{
    public record RegisterCustomerCommand(string FirstName, string LastName, string Email) : IRequest<Guid>;
}
