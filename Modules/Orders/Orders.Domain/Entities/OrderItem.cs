using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Common;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Guid Id { get; private set; }
        public ProductId ProductId { get; private set; }
        public string ProductName { get; private set; }
        public Money UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public Money TotalPrice => UnitPrice.Multiply(Quantity);

        private OrderItem() { }

        internal static OrderItem Create(ProductId productId, string productName, Money unitPrice, int quantity)
        {
            if (productId is null) throw new DomainException("ProductId required");
            if (string.IsNullOrWhiteSpace(productName)) throw new DomainException("Product name required");
            if (unitPrice is null) throw new DomainException("Unit price required");
            if (quantity <= 0) throw new DomainException("Quantity must be positive");

            return new OrderItem()
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = quantity,
                UnitPrice = unitPrice
            };
        }

        internal void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be positive");
            Quantity += quantity;
        }
    }
}
