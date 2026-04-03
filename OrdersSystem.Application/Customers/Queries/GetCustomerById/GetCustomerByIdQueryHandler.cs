using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery query, CancellationToken ct)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(query.Id), ct);

            if (customer is null) throw new DomainException("Customer not found");

            return new CustomerDto
            {
                Id = customer.Id.Value,
                Email = customer.Email.Value,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = $"{customer.Address.Street}, {customer.Address.City}, {customer.Address.Country}, {customer.Address.PostalCode}",
                CreatedAt = customer.CreatedAt,
                IsActive = customer.IsActive
            };
        }
    }
}
