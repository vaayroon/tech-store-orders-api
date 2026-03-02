namespace TechStoreOrders.Application.Products.Commands.AddProductStock;

public interface IAddProductStockCommandHandler
{
    Task HandleAsync(AddProductStockCommand command, CancellationToken cancellationToken = default);
}
