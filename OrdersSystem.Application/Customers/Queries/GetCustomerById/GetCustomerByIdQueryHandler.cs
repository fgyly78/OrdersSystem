using MediatR;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Customers;
using OrdersSystem.Application.Common.Interfaces.ReadServices;

namespace OrdersSystem.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerReadService _customerReadService;

        public GetCustomerByIdQueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery query, CancellationToken ct)
        {
            var customer = await _customerReadService.GetByIdAsync(new CustomerId(query.Id), ct);

            if (customer is null) throw new DomainException("Customer not found");

            return customer;
        }
    }
}
