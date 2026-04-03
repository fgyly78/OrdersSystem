using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.PayOrder
{
    public record PayOrderCommand(Guid OrderId) : IRequest<Unit>;
}
