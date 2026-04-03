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
    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private readonly IProductRepository _producRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReplenishStockCommandHandler(IUnitOfWork unitOfWork, IProductRepository producRepository)
        {
            _producRepository = producRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReplenishStockCommand command, CancellationToken ct)
        {
            var product = await _producRepository.GetByIdAsync(new ProductId(command.ProductId), ct);
            if (product is null) throw new DomainException("Product not found");

            product.ReplenishStock(command.Quantity);

            await _producRepository.UpdateAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
