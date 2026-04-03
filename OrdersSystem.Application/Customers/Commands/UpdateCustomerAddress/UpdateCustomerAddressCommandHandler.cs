using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Commands.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddresCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerAddressCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCustomerAddresCommand command, CancellationToken ct)
        {
            var address = new Address(command.Street, command.City, command.Country, command.PostalCode);
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId), ct);
            if (customer is null) throw new DomainException("Customer not found");

            customer.UpdateAddress(address);

            await _customerRepository.UpdateAsync(customer, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
