using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Products.Commands.RepnenishStock
{
    public record ReplenishStockCommand(Guid ProductId, int Quantity) : IRequest<Unit>;
}
