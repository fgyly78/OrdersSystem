using System.Text.Json.Serialization;
using Orders.Domain.Common;
using Orders.Domain.Enums;
using Orders.Domain.Events;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Entities
{
    public class Order : AggregateRoot
    {
        private readonly List<OrderItem> _items = new();

        public OrderId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public OrderState Status { get; private set; }
        public Address ShippingAddress { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Money TotalPrice => _items.Aggregate(Money.Zero(), (sum, i) => sum.Add(i.TotalPrice));
        public int TotalAmount => _items.Sum(i => i.Quantity);

        private Order() { }
        
        public static Order Create(CustomerId customerId, Address shippingAddress)
        {
            if (customerId is null)
                throw new DomainException("CustomerId is required");
            if (shippingAddress is null)
                throw new DomainException("shippingAddress is required");

            var order = new Order
            {
                Id = OrderId.New(),
                CustomerId = customerId,
                ShippingAddress = shippingAddress,
                Status = OrderState.Created,
                CreatedAt = DateTime.UtcNow,
            };

            order.RaiseDomainEvent(new OrderCreatedEvent(order.Id, customerId));
            return order;
        }

        public void AddItem(ProductId productId, string productName, Money unitPrice, int quantity)
        {
            if (productId is null) throw new DomainException("ProductId required");
            if (string.IsNullOrWhiteSpace(productName)) throw new DomainException("Product name required");
            if (unitPrice is null) throw new DomainException("Unit price required");
            if (quantity <= 0) throw new DomainException("Quantity must be positive");

            GuardAgainstModification();

            var existing = _items.FirstOrDefault(i => i.ProductName == productName);
            if (existing != null) existing.IncreaseQuantity(quantity);
            else
                _items.Add(OrderItem.Create(productId, productName, unitPrice, quantity));
        }

        public void RemoveItem(ProductId productId)
        {
            if (productId is null) throw new DomainException("ProductId required");

            GuardAgainstModification();

            var item = _items.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new DomainException($"Item with product {productId} not found");

            _items.Remove(item);
        }

        public void Pay()
        {
            if (Status != OrderState.Created)
                throw new InvalidOperationException($"Cannot pay order in state {Status}");
            if (!_items.Any()) throw new InvalidOperationException("Cannot pay empty order");

            Status = OrderState.Paid;
            PaidAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderPaidEvent(Id, CustomerId));
        }

        public void Complete()
        {
            if (Status != OrderState.Paid)
                throw new InvalidOperationException($"Cannot complete order in state {Status}");

            Status = OrderState.Completed;
            RaiseDomainEvent(new OrderCompletedEvent(Id, CustomerId));
        }

        public void Cancel(string reason)
        {
            if (Status == OrderState.Completed)
                throw new InvalidOperationException("Cannot cancel completed order");

            else Status = OrderState.Cancelled;
        }

        private void GuardAgainstModification()
        {
            if (Status != OrderState.Created)
                throw new DomainException($"Cannot modify order in state {Status}");
        }
    }
}
