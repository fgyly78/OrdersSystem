using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Products.Commands
{
    public record UpdatePriceCommand(Guid ProductId, decimal Price, string Currency) : IRequest<Unit>;
}
