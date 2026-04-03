using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Commands
{
    public class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task Handle(UpdatePriceCommand command, CancellationToken ct)
        {
            var money = new Money(command.Price, command.Currency);
            var product = await _productRepository.GetByIdAsync(new ProductId(command.ProductId), ct);
            if (product is null) throw new DomainException("Product not found");

            product.UpdatePrice(money);

            await _productRepository.UpdateAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
