using MediatR;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Customers;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Application.Customers.Queries.GetCustomerById
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
