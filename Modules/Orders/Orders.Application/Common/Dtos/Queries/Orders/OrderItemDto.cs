using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Orders.Queries
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
