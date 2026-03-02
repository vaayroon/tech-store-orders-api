namespace TechStoreOrders.Application.Orders.Commands.ConfirmOrder;

public interface IConfirmOrderCommandHandler
{
    Task HandleAsync(ConfirmOrderCommand command, CancellationToken cancellationToken = default);
}
