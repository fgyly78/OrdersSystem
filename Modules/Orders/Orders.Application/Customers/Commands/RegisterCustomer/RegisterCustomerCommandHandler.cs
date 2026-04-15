using MediatR;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Application.Customers.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RegisterCustomerCommand command, CancellationToken ct)
        {
            var customer = Customer.Create(command.FirstName, command.LastName, command.Email);

            await _customerRepository.AddAsync(customer, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return customer.Id.Value;
        }
    }
}
