
using Orders.Application.Orders.Queries;
using Orders.Domain.Entities;

namespace Orders.Application.Common.Mapping;

public class OrderMapper
{
    public static OrderDto MapToDto(Order order) => new OrderDto{
        Id = order.Id.Value,
        CustomerId = order.CustomerId.Value,
        Status = order.Status.ToString(),
        CreatedAt = order.CreatedAt,
        TotalPrice = order.TotalPrice.Amount,
        ShippingAddress = $"{order.ShippingAddressBase.Street}, {order.ShippingAddressBase.City}, {order.ShippingAddressBase.Country}, {order.ShippingAddressBase.PostalCode}",
        Items = order.Items.Select(i => new OrderItemDto
        {
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice.Amount,
            TotalPrice = i.TotalPrice.Amount,
        }).ToList()
    };
    
    public static List<OrderDto> MapToDtos(List<Order> orders) => orders.Select(o => MapToDto(o)).ToList();
}