using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries
{
    public record GetAvailableProductsQuery() : IRequest<List<ProductDto>>;
}
