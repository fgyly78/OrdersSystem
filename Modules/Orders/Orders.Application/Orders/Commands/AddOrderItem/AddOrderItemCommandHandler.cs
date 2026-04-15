using MediatR;
using Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Common;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Orders.Commands.AddOrderItem
{
    public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddOrderItemCommand command, CancellationToken ct)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(command.ProductId), ct);
            if (product is null) throw new DomainException("Product not found");

            var order = await _orderRepository.GetByIdAsync(new OrderId(command.OrderId), ct);
            if (order is null) throw new DomainException("Order not found");

            order.AddItem(product.Id, product.Name, product.Price, command.Quantity);
            product.ReserveStock(command.Quantity);

            await _orderRepository.UpdateAsync(order, ct);
            await _productRepository.UpdateAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
