using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Orders.Queries.GetOrderById
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
