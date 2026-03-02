namespace TechStoreOrders.Application.Orders.Commands.AddProductToOrder;

public interface IAddProductToOrderCommandHandler
{
    Task HandleAsync(AddProductToOrderCommand command, CancellationToken cancellationToken = default);
}
