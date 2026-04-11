using MediatR;
using OrdersSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Products;

namespace OrdersSystem.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>;
}
