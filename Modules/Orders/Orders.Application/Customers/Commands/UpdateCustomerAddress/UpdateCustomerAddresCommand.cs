using MediatR;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Customers.Commands.UpdateCustomerAddress
{
    public record UpdateCustomerAddresCommand(
        Guid CustomerId,
        string Street,
        string City,
        string Country,
        string PostalCode) : IRequest<Unit>;
}
