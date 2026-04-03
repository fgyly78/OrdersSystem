using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries.GetAvailableProducts
{
    public class GetAvailableProductsQueryHandler : IRequestHandler<GetAvailableProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableProductsQueryHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> Handle(GetAvailableProductsQuery query, CancellationToken ct)
        {
            var products = await _productRepository.GetAvailiableAsync(ct);

            return products.Select(product => new ProductDto
            {
                Id = product.Id.Value,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.Amount,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable
            }).ToList();
        }
    }
}
