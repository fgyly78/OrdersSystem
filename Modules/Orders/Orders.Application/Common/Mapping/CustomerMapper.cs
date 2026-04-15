using Orders.Application.Common.Dtos.Queries.Customers;
using Orders.Domain.Entities;
using Orders.Application.Orders.Queries;

namespace Orders.Application.Common.Mapping;

public class CustomerMapper
{
    public static CustomerDto MapToDto(Customer customer) => new CustomerDto
    {
        Id = customer.Id.Value,
        Email = customer.Email.Value,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        Address =
            $"{customer.Address.Street}, {customer.Address.City}, {customer.Address.Country}, {customer.Address.PostalCode}",
        CreatedAt = customer.CreatedAt,
        IsActive = customer.IsActive
    };
}