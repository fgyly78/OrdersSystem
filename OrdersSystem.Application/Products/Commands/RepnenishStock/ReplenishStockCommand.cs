using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Commands.RepnenishStock
{
    public record ReplenishStockCommand(Guid ProductId, int Quantity) : IRequest<Unit>;
}
