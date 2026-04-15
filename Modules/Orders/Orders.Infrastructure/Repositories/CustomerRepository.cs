using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Customer customer, CancellationToken ct = default)
        {
            await _appDbContext.Customers.AddAsync(customer, ct);
        }

        public async Task<Customer?> GetByEmailAsync(Email email, CancellationToken ct = default)
        {
            return await _appDbContext.Customers
                .FirstOrDefaultAsync(x => x.Email == email, ct);
        }

        public async Task<Customer?> GetByIdAsync(CustomerId id, CancellationToken ct = default)
        {
            return await _appDbContext.Customers
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public Task UpdateAsync(Customer customer, CancellationToken ct = default)
        {
            _appDbContext.Customers.Update(customer);
            return Task.CompletedTask;
        }
    }
}
