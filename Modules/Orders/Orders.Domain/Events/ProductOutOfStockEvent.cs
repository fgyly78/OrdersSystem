using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Events
{
    public record ProductOutOfStockEvent(ProductId ProductId, string ProductName) : IDomainEvent;
}
