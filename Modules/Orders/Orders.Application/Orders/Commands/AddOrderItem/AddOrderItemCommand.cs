using MediatR;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Commands.AddOrderItem
{
    public record AddOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity) : IRequest<Unit>;
}
