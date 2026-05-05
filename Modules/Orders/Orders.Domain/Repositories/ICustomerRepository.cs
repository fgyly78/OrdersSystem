using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id, CancellationToken ct = default);
        Task<Customer> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task AddAsync(Customer customer, CancellationToken ct = default);
        Task UpdateAsync(Customer customer, CancellationToken ct = default);
    }
}
