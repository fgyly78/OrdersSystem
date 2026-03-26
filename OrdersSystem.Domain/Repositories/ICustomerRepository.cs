using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrdersSystem.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(CustomerId id, CancellationToken ct = default);
        Task<Customer?> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task AddAsync(Customer customer, CancellationToken ct = default);
    }
}
