using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Common;

namespace Orders.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public UnitOfWork(AppDbContext appDbContext,  IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _appDbContext.SaveChangesAsync(ct);
            await DispatchDomainEventsAsync(ct);
        }

        public async Task DispatchDomainEventsAsync(CancellationToken ct = default)
        {
            var entites = _appDbContext.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();
            
            var events = entites
                .SelectMany(e=> e.DomainEvents)
                .ToList();

            foreach (var entity in entites)
            {
                entity.ClearDomainEvents();
            }

            foreach (var e in events)
            {
                await _mediator.Publish(e, ct);
            }

        }
    }
}
