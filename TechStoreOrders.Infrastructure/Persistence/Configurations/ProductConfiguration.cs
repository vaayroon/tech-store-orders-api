using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStoreOrders.Domain.Entities;

namespace TechStoreOrders.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(product => product.PriceEur)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(product => product.Stock)
            .IsRequired();

        builder.HasData(
            new
            {
                Id = new Guid("9DDBF7B4-6D18-4AB5-B5C5-43A4A4BE84BB"),
                Name = "Running Sneakers",
                PriceEur = 120m,
                Stock = 40
            },
            new
            {
                Id = new Guid("9FDC765E-F7B5-49E6-925E-EF0A0A18A6D0"),
                Name = "Basic T-Shirt",
                PriceEur = 30m,
                Stock = 120
            },
            new
            {
                Id = new Guid("3AE0AB8D-5B07-4B2D-BE2D-5F6AA3F27A51"),
                Name = "Cotton Socks",
                PriceEur = 10m,
                Stock = 300
            },
            new
            {
                Id = new Guid("D3A08C3F-D32B-4C7E-AC4A-8A4D5E8B1F37"),
                Name = "Slim Jeans",
                PriceEur = 65m,
                Stock = 80
            },
            new
            {
                Id = new Guid("1A6B7FEE-972A-47CF-9185-A8AC2E10CF9B"),
                Name = "Hoodie",
                PriceEur = 75m,
                Stock = 60
            });
    }
}
