namespace TechStoreOrders.Application.Orders.Commands.RemoveProductFromOrder;

public interface IRemoveProductFromOrderCommandHandler
{
    Task HandleAsync(RemoveProductFromOrderCommand command, CancellationToken cancellationToken = default);
}
