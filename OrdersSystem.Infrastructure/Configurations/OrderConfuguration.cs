using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure.Configurations
{
    public class OrderConfuguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o=>o.Id);

            builder.Property(o => o.Id)
                .IsRequired()
                .HasConversion(
                  id => id.Value,
                  value => new OrderId(value)
                  );

            builder.Property(c => c.CustomerId)
                .IsRequired()
                .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
                );

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.OwnsOne(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street)
                    .HasColumnName("ShippingStreet")
                    .IsRequired()
                    .HasMaxLength(200);

                addressBuilder.Property(a => a.City)
                    .HasColumnName("ShippingCity")
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.Country)
                    .HasColumnName("ShippingCountry")
                    .IsRequired()
                    .HasMaxLength(3);

                addressBuilder.Property(a => a.PostalCode)
                    .HasColumnName("ShippingPostalCode")
                    .HasMaxLength(20);
            });

            builder.Property(o => o.CreatedAt).IsRequired();
            
          builder.Ignore(o => o.TotalPrice);
          builder.Ignore(o => o.TotalAmount);

            builder.HasMany(i => i.Items)
                .WithOne()
                .HasForeignKey("OrderId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
