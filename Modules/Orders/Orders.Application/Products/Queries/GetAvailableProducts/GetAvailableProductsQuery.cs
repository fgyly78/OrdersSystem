using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Products;

namespace Orders.Application.Products.Queries.GetAvailableProducts
{
    public record GetAvailableProductsQuery() : IRequest<List<ProductDto>>;
}
