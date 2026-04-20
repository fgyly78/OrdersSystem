using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Common;

namespace Orders.Domain.ValueObjects
{
    public record AddressBase
    {
        public string Street { get; }
        public string City { get; }
        public string Country { get; }
        public string PostalCode { get; }

        public AddressBase(string street, string city, string country, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new DomainException("Street is required");
            if (string.IsNullOrWhiteSpace(city)) throw new DomainException("City is required");
            if (string.IsNullOrWhiteSpace(country)) throw new DomainException("Country is required");

            Street = street.Trim();
            City = city.Trim();
            Country = country.Trim().ToUpperInvariant();
            PostalCode = postalCode?.Trim() ?? string.Empty;
        }

        public override string ToString() => $"{Street}, {City}, {Country}, {PostalCode}";
    }
}
