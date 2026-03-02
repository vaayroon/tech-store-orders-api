using Microsoft.EntityFrameworkCore;
using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Entities;
using TechStoreOrders.Infrastructure.Persistence;

namespace TechStoreOrders.Infrastructure.Repositories;

public sealed class OrderRepository(TechStoreDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await dbContext.Orders.AddAsync(order, cancellationToken);
    }

    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return dbContext.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }
}
