using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.PayOrder
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PayOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(PayOrderCommand command, CancellationToken ct)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(command.OrderId) , ct);

            if (order == null) throw new DomainException("Order not found");

            order.Pay();

            await _orderRepository.UpdateAsync(order, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
