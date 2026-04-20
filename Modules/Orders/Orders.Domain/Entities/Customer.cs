using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Orders.Domain.Common;
using Orders.Domain.Events;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Entities
{
    public class Customer : AggregateRoot
    {
        public CustomerId Id { get; private set; }
        public Email Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public CustomerAddress? Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public Customer() { }

        public static Customer Create(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required");

            var customer = new Customer
            {
                Id = CustomerId.New(),
                Email = new Email(email),
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            customer.RaiseDomainEvent(new CustomerRegisteredEvent(customer.Id,  customer.Email));
            return customer;
        }

        public void UpdateAddress(CustomerAddress newAddressBase)
        {
            Address = newAddressBase ?? throw new DomainException("Address canot be null");
        }

        public void Deactivate()
        {
            if (!IsActive) throw new DomainException("Customer is already inactive");
            IsActive = false;
            RaiseDomainEvent(new CustomerDeactivatedEvent(Id));
        }

        public string FullName => $"{FirstName}  {LastName}";
    }
}
