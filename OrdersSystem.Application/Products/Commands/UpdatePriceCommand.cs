using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Commands
{
    public record UpdatePriceCommand(Guid ProductId, decimal Price, string Currency) : IRequest;
}
