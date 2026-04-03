using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(query.ProductId), ct);

            if (product is null) throw new DomainException("Product not found");

            return new ProductDto()
            {
                Id = product.Id.Value,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.Amount,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable
            };
        }
    }
}
