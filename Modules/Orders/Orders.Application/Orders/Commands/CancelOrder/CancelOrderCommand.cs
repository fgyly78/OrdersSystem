using MediatR;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, string Reason) : IRequest<Unit>;
}
