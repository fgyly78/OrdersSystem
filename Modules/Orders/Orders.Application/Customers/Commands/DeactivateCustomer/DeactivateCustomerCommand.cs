using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Customers.Commands.DeactivateCustomer
{
    public record DeactivateCustomerCommand(Guid CustomerId) : IRequest<Unit>;
}
