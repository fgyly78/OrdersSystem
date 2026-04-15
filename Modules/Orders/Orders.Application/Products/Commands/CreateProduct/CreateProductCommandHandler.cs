using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken ct)
        {
            var money = new Money(command.Price, command.Currency);
            var product = Product.Create(command.Name, money, command.InitialStock);

            await _productRepository.AddAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return product.Id.Value;
        }
    }
}
