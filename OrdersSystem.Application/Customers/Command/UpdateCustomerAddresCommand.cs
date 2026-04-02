using MediatR;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Command
{
    public record UpdateCustomerAddresCommand(
        Guid CustomerId,
        string Street,
        string City,
        string Country,
        string PostalCode) : IRequest;
}
