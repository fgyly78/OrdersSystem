using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Application.Common.Dtos.Queries.Customers;
using Orders.Application.Common.Interfaces;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Application.Common.Mapping;
using Orders.Application.Products.Queries.GetCustomerProducts;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;
using Orders.Application.Customers.Queries;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.ReadServices
{
    public class CustomerReadService : ICustomerReadService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerReadService(AppDbContext appDbContext, ICacheService cacheService,
            ICustomerRepository customerRepository)
        {
            _appDbContext = appDbContext;
            _customerRepository = customerRepository;
            _cacheService = cacheService;
        }

        public async Task<CustomerDto> GetByIdAsync(CustomerId customerId, CancellationToken ct)
        {
            return await _cacheService.GetOrCreateAsync(
                $"customer:{customerId}",
                async () =>
                {
                    var customer = await _customerRepository.GetByIdAsync(customerId, ct);
                    return CustomerMapper.MapToDto(customer);
                },
                TimeSpan.FromMinutes(5),
                ct);
        }

        public async Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(Guid customerId,
            CancellationToken ct)
        {
            return await _appDbContext.Orders
                .Where(o => o.CustomerId == new CustomerId(customerId))
                .SelectMany(o => o.Items)
                .GroupBy(i => new { i.ProductId, i.ProductName })
                .Select(g => new CustomerProductSummaryDto()
                {
                    ProductId = g.Key.ProductId.Value,
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(i => i.Quantity),
                    TotalAmount = g.Sum(i => i.UnitPrice.Amount),
                    Currency = g.First().UnitPrice.Currency,
                })
                .ToListAsync(ct);
        }
    }
}