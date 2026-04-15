using MediatR;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId) : IRequest<Guid>;
}
