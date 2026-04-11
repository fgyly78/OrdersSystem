using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Products;
using OrdersSystem.Application.Common.Interfaces.ReadServices;

namespace OrdersSystem.Application.Products.Queries.GetProductById
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
