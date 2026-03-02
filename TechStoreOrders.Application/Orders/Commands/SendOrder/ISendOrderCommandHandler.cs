namespace TechStoreOrders.Application.Orders.Commands.SendOrder;

public interface ISendOrderCommandHandler
{
    Task HandleAsync(SendOrderCommand command, CancellationToken cancellationToken = default);
}
