using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Products.Queries.GetCustomerProducts
{
    public class CustomerProductSummaryDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount{ get; set; }
        public string Currency {  get; set; }
    }
}
