using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Orders.Commands.SendOrder;

public sealed class SendOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ISendOrderCommandHandler
{
    public async Task HandleAsync(SendOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new DomainException("Order was not found.");

        order.Send();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
