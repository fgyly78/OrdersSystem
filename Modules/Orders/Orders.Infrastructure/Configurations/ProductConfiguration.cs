using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            // 1. Ключ
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value)
                )
                .HasColumnName("product_id");

            // 2. Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            // 3. Description
            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            // 4. Price (Value Object)
            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("price_amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                price.Property(p => p.Currency)
                    .HasColumnName("price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // 5. StockQuantity
            builder.Property(x => x.StockQuantity)
                .IsRequired()
                .HasColumnName("stock_quantity");

            // 6. IsAvailable
            builder.Property(x => x.IsAvailable)
                .IsRequired()
                .HasColumnName("is_available");
        }
    }
}
