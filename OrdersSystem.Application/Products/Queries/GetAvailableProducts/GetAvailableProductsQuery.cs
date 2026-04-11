using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Products;

namespace OrdersSystem.Application.Products.Queries.GetAvailableProducts
{
    public record GetAvailableProductsQuery() : IRequest<List<ProductDto>>;
}
