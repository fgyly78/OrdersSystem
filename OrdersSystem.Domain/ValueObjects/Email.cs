using OrdersSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email cannot be empty");

            if (!value.Contains('@') || value.IndexOf('.') < 0)
                throw new DomainException("Email format is invalid");

            Value = value.Trim().ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}
