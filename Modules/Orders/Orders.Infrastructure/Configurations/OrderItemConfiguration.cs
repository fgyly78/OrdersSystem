using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(x => x.Id);

            builder.Property(o => o.ProductId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    Value => new ProductId(Value))
                .HasColumnName("product_id");

            builder.Property(o => o.ProductName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("product_name");

            builder.Property(x => x.Quantity)
           .IsRequired()
           .HasColumnName("quantity");

            builder.OwnsOne(x => x.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("unit_price_amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                price.Property(p => p.Currency)
                    .HasColumnName("unit_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

        
        }
    }
}
