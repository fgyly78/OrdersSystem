namespace Orders.Requests.Customers
{
    public record UpdateCustomerAddressRequest(string Street, string City, string Country, string PostalCode);
}
