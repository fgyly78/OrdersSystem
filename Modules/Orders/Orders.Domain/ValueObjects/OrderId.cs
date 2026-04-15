using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.ValueObjects
{
    public record OrderId(Guid Value)
    {
        public static OrderId New() => new(Guid.NewGuid());
        public static OrderId From(Guid Value) => new(Value);
        public override string ToString() => Value.ToString();
    }
}
