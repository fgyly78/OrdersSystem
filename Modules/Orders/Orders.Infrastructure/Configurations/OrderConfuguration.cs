using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Configurations
{
    public class OrderConfuguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o=>o.Id);

            builder.Property(o => o.Id)
                .IsRequired()
                .HasConversion(
                  id => id.Value,
                  value => new OrderId(value)
                  )
                .HasColumnName("order_id");

            builder.Property(c => c.CustomerId)
                .IsRequired()
                .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
                )
                .HasColumnName("customer_id");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("status");

            builder.OwnsOne(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street)
                    .HasColumnName("shipping_street")
                    .IsRequired()
                    .HasMaxLength(200);

                addressBuilder.Property(a => a.City)
                    .HasColumnName("shipping_city")
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.Country)
                    .HasColumnName("shipping_country")
                    .IsRequired()
                    .HasMaxLength(3);

                addressBuilder.Property(a => a.PostalCode)
                    .HasColumnName("shipping_postal_code")
                    .HasMaxLength(20);
            });

            builder.Property(o => o.CreatedAt).IsRequired()
                .HasColumnName("created_at");
            

            builder.HasMany(i => i.Items)
                .WithOne()
                .HasForeignKey("order_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
