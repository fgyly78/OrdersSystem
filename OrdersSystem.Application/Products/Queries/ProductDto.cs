using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Application.Products.Queries
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
