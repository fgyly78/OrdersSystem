using Microsoft.EntityFrameworkCore;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure.ReadServices
{
    public class CustomerReadService : ICustomerReadService
    {
        private readonly AppDbContext _appDbContext;

        public CustomerReadService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
