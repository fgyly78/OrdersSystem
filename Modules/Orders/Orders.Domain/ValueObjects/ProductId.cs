using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.ValueObjects
{
    public record ProductId(Guid Value)
    {
        public static ProductId New() => new(Guid.NewGuid()); 
        public static ProductId From(Guid Value) => new(Value); 
        public override string ToString() => Value.ToString(); 
    }
}
