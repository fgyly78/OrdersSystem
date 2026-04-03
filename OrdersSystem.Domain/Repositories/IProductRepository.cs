using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(ProductId id, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetAvailiableAsync(CancellationToken ct = default);
        Task AddAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);

    }
}
