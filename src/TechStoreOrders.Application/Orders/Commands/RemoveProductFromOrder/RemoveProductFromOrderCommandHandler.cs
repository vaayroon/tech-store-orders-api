using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Orders.Commands.RemoveProductFromOrder;

public sealed class RemoveProductFromOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRemoveProductFromOrderCommandHandler
{
    public async Task HandleAsync(RemoveProductFromOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new DomainException("Order was not found.");

        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new DomainException("Product was not found.");

        await unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var restoredQuantity = order.RemoveProduct(command.ProductId);
            product.AddStock(restoredQuantity);
            productRepository.Update(product);
            await Task.CompletedTask;
        }, cancellationToken);
    }
}
