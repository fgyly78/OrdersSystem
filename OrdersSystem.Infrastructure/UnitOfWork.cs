using OrdersSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
            return _appDbContext.SaveChangesAsync(ct);
        }
    }
}
