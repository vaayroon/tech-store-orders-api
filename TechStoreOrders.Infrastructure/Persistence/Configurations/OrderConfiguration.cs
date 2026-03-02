using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStoreOrders.Domain.Entities;

namespace TechStoreOrders.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsMany(order => order.Items, orderItemBuilder =>
        {
            orderItemBuilder.ToTable("OrderItems");

            orderItemBuilder.WithOwner().HasForeignKey("OrderId");

            orderItemBuilder.Property<int>("Id");
            orderItemBuilder.HasKey("Id");

            orderItemBuilder.Property(item => item.ProductId)
                .IsRequired();

            orderItemBuilder.Property(item => item.Quantity)
                .IsRequired();

            orderItemBuilder.Property(item => item.UnitPriceSnapshotEur)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
    }
}
