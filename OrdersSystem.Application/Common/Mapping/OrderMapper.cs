
using OrdersSystem.Application.Orders.Queries;
using OrdersSystem.Domain.Entities;

namespace OrdersSystem.Application.Common.Mapping;

public class OrderMapper
{
    public static OrderDto MapToDto(Order order) => new OrderDto{
        Id = order.Id.Value,
        CustomerId = order.CustomerId.Value,
        Status = order.Status.ToString(),
        CreatedAt = order.CreatedAt,
        TotalPrice = order.TotalPrice.Amount,
        ShippingAddress = $"{order.ShippingAddress.Street}, {order.ShippingAddress.City}, {order.ShippingAddress.Country}, {order.ShippingAddress.PostalCode}",
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