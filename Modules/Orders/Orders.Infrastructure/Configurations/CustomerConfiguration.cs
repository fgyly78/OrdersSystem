using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;

namespace Orders.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CustomerId(value)
                )
                .HasColumnName("customer_id");

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(256);
            });

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("first_name");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("last_name");

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasColumnName("street")
                    .HasMaxLength(200);

                address.Property(a => a.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                address.Property(a => a.Country)
                    .HasColumnName("country")
                    .HasMaxLength(3);

                address.Property(a => a.PostalCode)
                    .HasColumnName("postal_сode")
                    .HasMaxLength(20);
            });

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("is_active");
        }
    }
}
