using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Common;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Customers.Commands.UpdateCustomerAddress
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
            var address = new CustomerAddress(command.Street, command.City, command.Country, command.PostalCode);
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId), ct);
            if (customer is null) throw new DomainException("Customer not found");

            customer.UpdateAddress(address);

            await _customerRepository.UpdateAsync(customer, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
