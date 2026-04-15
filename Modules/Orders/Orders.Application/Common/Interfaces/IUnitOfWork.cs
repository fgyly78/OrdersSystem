using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken ct = default);
        Task DispatchDomainEventsAsync(CancellationToken ct = default);
    }
}
