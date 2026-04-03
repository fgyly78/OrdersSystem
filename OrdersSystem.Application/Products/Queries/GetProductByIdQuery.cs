using MediatR;
using OrdersSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>;
}
