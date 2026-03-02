using Microsoft.EntityFrameworkCore;
using TechStoreOrders.Domain.Entities;

namespace TechStoreOrders.Infrastructure.Persistence;

public sealed class TechStoreDbContext(DbContextOptions<TechStoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechStoreDbContext).Assembly);
    }
}
