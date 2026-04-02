using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersSystem.Domain.Entities;
using OrdersSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            // 1. Ключ
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value)
                );

            // 2. Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            // 3. Description
            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            // 4. Price (Value Object)
            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("PriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                price.Property(p => p.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // 5. StockQuantity
            builder.Property(x => x.StockQuantity)
                .IsRequired();

            // 6. IsAvailable
            builder.Property(x => x.IsAvailable)
                .IsRequired();
        }
    }
}
