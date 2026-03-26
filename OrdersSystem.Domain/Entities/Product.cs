using OrdersSystem.Domain.Common;
using OrdersSystem.Domain.Events;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Domain.Entities
{
    public class Product : AggragateRoot
    {
        public ProductId Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsAvailiable { get; private set; }

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
                IsAvailiable = initialStock > 0
            };
        }

        public void ReserveStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be positive");
            if (StockQuantity < quantity) throw new DomainException($"Insufficient stock. Availiable: {StockQuantity}");

            StockQuantity -= quantity;

            if (StockQuantity == 0)
            {
                IsAvailiable = false;
                RaiseDomainEvent(new ProductOutOfStockEvent(Id, Name));
            }
        }

        public void PeplenishStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must ve positive");

            var wasUnavailiable = !IsAvailiable;
            StockQuantity += quantity;
            IsAvailiable = true;

            if (wasUnavailiable)
                RaiseDomainEvent(new ProductOutOfStockEvent(Id, Name));
        }

        public void updatePrice(Money newPrice)
        {
            Price = newPrice;
        }
    }
}
