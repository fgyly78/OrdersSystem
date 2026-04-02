using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId), ct);
            if (customer is null) throw new DomainException("Customer not found");
            if (customer.Address is null) throw new DomainException("Customer has no address");

            var order = Order.Create(customer.Id, customer.Address);
            await _orderRepository.AddAsync(order, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return order.Id.Value;
        }
    }
}
