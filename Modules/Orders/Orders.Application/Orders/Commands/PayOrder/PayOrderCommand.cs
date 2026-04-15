using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Commands.PayOrder
{
    public record PayOrderCommand(Guid OrderId) : IRequest<Unit>;
}
