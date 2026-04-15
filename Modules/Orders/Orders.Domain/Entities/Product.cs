using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Common;
using Orders.Domain.Events;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Entities
{
    public class Product : AggregateRoot
    {
        public ProductId Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsAvailable { get; private set; }

        public Product() { }

        public static Product Create(string name, Money price, int initialStock = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name is required");
            if (initialStock < 0) throw new DomainException("Stock cannot be negative");

            return new Product
            {
                Id = ProductId.New(),
                Name = name.Trim(),
                Price = price,
                StockQuantity = initialStock,
                IsAvailable = initialStock > 0
            };
        }

        public void ReserveStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be positive");
            if (StockQuantity < quantity) throw new DomainException($"Insufficient stock. Availiable: {StockQuantity}");

            StockQuantity -= quantity;

            if (StockQuantity == 0)
            {
                IsAvailable = false;
                RaiseDomainEvent(new ProductOutOfStockEvent(Id, Name));
            }
        }

        public void ReplenishStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must ve positive");

            var wasUnavailiable = !IsAvailable;
            StockQuantity += quantity;
            IsAvailable = true;

            if (wasUnavailiable)
                RaiseDomainEvent(new ProductOutOfStockEvent(Id, Name));
        }

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
        }
    }
}
