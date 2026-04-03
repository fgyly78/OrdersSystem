using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Commands.DeactivateCustomer
{
    public class DeactivateCustomerCommandHandler : IRequestHandler<DeactivateCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeactivateCustomerCommand command, CancellationToken ct)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId), ct);
            if (customer is null) throw new DomainException("Customer not found");

            customer.Deactivate();

            await _customerRepository.UpdateAsync(customer, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
