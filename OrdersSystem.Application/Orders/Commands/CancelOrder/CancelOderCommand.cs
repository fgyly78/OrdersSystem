using MediatR;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, string Reason) : IRequest;
}
