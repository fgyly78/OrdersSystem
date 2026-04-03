using MediatR;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.AddOrderItem
{
    public record AddOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity) : IRequest;
}
