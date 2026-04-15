using MediatR;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Products;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Repositories;

namespace Orders.Application.Products.Queries.GetAvailableProducts
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
