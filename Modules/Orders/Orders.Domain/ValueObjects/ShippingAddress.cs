namespace Orders.Domain.ValueObjects;

public record ShippingAddress : AddressBase
{
    public ShippingAddress(string street, string city, string country, string postalCode)
        : base(street, city, country, postalCode) { }
    
    public static ShippingAddress FromCustomer(CustomerAddress customerAddress)
    {
        return new ShippingAddress(customerAddress.Street, customerAddress.City, customerAddress.Country, customerAddress.PostalCode);
    }
}