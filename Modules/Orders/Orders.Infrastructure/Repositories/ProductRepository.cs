using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            await _appDbContext.Products.AddAsync(product, ct);
        }

        public async Task<IReadOnlyList<Product>> GetAvailiableAsync(CancellationToken ct = default)
        {
            return await _appDbContext.Products
                .Where(p => p.IsAvailable)
                .ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken ct = default)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(o => o.Id == id, ct);
        }

        public Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _appDbContext.Products.Update(product);
            return Task.CompletedTask;
        }
    }
}
