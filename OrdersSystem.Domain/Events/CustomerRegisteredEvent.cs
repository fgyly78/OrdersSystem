using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Domain.Events
{
    public record CustomerRegisteredEvent(CustomerId CustomerId, Email Email) : IDomainEvent;
}
