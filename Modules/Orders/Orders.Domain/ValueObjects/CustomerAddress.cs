namespace Orders.Domain.ValueObjects;

public record CustomerAddress : AddressBase
{
    public CustomerAddress(string street, string city, string country, string postalCode)
        : base(street, city, country, postalCode) { }
}