using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, decimal Price, string Currency, int InitialStock) : IRequest<Guid>;
}
