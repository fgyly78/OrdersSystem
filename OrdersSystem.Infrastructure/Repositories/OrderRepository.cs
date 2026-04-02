using Microsoft.EntityFrameworkCore;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Order order, CancellationToken ct = default)
        {
            await _appDbContext.Orders.AddAsync(order, ct);
        }

        public async Task<IReadOnlyList<Order>> GetByCustomerIdAsync(CustomerId customerId, CancellationToken ct = default)
        {
            return await _appDbContext.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync(ct);
        }

        public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken ct = default)
        {
            return await _appDbContext.Orders
                .Include(o=>o.Items)
                .FirstOrDefaultAsync(o=>o.Id == id, ct);
        }

        public Task UpdateAsync(Order order, CancellationToken ct = default)
        {
            _appDbContext.Orders.Update(order);
            return Task.CompletedTask;
        }
    }
}
