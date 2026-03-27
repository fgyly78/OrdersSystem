using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrdersSystem.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            var customerId = new CustomerId(command.CustomerId);
            var address = new Address(command.Street, command.City, command.Country, command.PostalCode);

            var order = Order.Create(customerId, address);
            await _orderRepository.AddAsync(order, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return order.Id.Value;
        }
    }
}
