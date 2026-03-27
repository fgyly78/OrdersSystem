using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(o => o.ProductId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    Value => new ProductId(Value));

            builder.Property(o => o.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Quantity)
           .IsRequired();

            builder.OwnsOne(x => x.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("UnitPriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                price.Property(p => p.Currency)
                    .HasColumnName("UnitPriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Ignore(x => x.TotalPrice);
        }
    }
}
