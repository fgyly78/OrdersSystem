using MediatR;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId, string Street, string City, string Country, string PostalCode
        ) : IRequest<Guid>;
}
