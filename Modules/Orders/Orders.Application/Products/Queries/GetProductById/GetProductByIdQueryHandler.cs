using MediatR;
using Orders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Products;
using Orders.Application.Common.Interfaces;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductReadService _productReadService;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IProductReadService productReadService, IUnitOfWork unitOfWork)
        {
            _productReadService = productReadService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _productReadService.GetByIdAsync(new ProductId(query.ProductId), ct);

            if (product is null) throw new DomainException("Product not found");

            return product;
        }
    }
}
