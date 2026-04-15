using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Common;

namespace Orders.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new DomainException("Amount cannot be negative");
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
                throw new DomainException("Currency must be a 3-letter ISO code");

            Amount = amount;
            Currency = currency.ToUpperInvariant();
        }

        public Money Add(Money other)
        {
            GuardSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be positive");
            return new Money(Amount * quantity, Currency);
        }

        public bool IsGreaterThan(Money other)
        {
            GuardSameCurrency(other);
            return Amount > other.Amount; 
        }

        private void GuardSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new DomainException($"Currency mismatch: {Currency} vs {other.Currency}");
        }

        public static Money Zero(string  currency = "USD") => new Money(0, currency);
        public override string ToString() => $"{Amount:F2} {Currency}";
    }
}
