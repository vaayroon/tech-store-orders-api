using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Entities;

namespace TechStoreOrders.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICreateOrderCommandHandler
{
    public async Task<Guid> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var orderId = Guid.NewGuid();
        var order = new Order(orderId);

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return orderId;
    }
}
