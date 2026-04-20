using MediatR;
using Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Common;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Orders.Commands.CreateOrder
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
            
            var order = Order.Create(customer.Id, ShippingAddress.FromCustomer(customer.Address));
            await _orderRepository.AddAsync(order, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return order.Id.Value;
        }
    }
}
