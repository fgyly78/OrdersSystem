using MediatR;
using Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Products;

namespace Orders.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>;
}
