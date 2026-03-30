using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.AddOrderItem
{
    public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddOrderItemCommand command, CancellationToken ct)
        {
            var productId = new ProductId(command.ProductId);
            var money = new Money(command.UnitPrice, command.Currency);

            var order = await _orderRepository.GetByIdAsync(new OrderId(command.OrderId), ct);
            if (order is null) throw new DomainException("Order not found");

            order.AddItem(productId, command.ProductName, money, command.Quantity);

            await _orderRepository.UpdateAsync(order, ct);
        }
    }
}
