using Microsoft.EntityFrameworkCore;
using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Entities;
using TechStoreOrders.Infrastructure.Persistence;

namespace TechStoreOrders.Infrastructure.Repositories;

public sealed class ProductRepository(TechStoreDbContext dbContext) : IProductRepository
{
    public Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return dbContext.Products
            .FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
    }
}
