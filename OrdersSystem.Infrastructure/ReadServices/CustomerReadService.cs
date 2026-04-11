using Microsoft.EntityFrameworkCore;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Dtos.Queries.Customers;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Application.Common.Interfaces.ReadServices;
using OrdersSystem.Application.Common.Mapping;
using OrdersSystem.Application.Customers.Queries;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;

namespace OrdersSystem.Infrastructure.ReadServices
{
    public class CustomerReadService : ICustomerReadService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerReadService(AppDbContext appDbContext,  ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _appDbContext = appDbContext;
            _customerRepository = customerRepository;
            _cacheService = cacheService;
        }
        
        public async Task<CustomerDto> GetByIdAsync(CustomerId customerId, CancellationToken ct)
        {
            var cacheKey = $"customer:{customerId}";
            
            var cached = await _cacheService.GetAsync<CustomerDto>(cacheKey);
            if (cached is not null) return cached;
            
            var customer = await _customerRepository.GetByIdAsync(customerId, ct);
            if (customer == null) return null;
            
            var dto = CustomerMapper.MapToDto(customer);
            
            await _cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(5), ct);

            return dto;
        }

        public async Task<List<CustomerProductSummaryDto>> GetCustomerProductsAsync(Guid customerId, CancellationToken ct)
        {
            return await _appDbContext.Orders
                .Where(o=>o.CustomerId == new CustomerId(customerId))
                .SelectMany(o=>o.Items)
                .GroupBy(i=>new {i.ProductId, i.ProductName})
                .Select(g=>new CustomerProductSummaryDto()
                {
                    ProductId = g.Key.ProductId.Value,
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(i=>i.Quantity),
                    TotalAmount = g.Sum(i=>i.UnitPrice.Amount),
                    Currency = g.First().UnitPrice.Currency,
                })
                .ToListAsync(ct);
        }
    }
}
