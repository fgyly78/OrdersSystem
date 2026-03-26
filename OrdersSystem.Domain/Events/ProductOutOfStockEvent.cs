using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Domain.Events
{
    public record ProductOutOfStockEvent(ProductId ProductId, string ProductName) : IDomainEvent;
}
