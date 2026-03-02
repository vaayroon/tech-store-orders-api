using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Orders.Commands.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IConfirmOrderCommandHandler
{
    public async Task HandleAsync(ConfirmOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new DomainException("Order was not found.");

        order.Confirm();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
