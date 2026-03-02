using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Entities;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Orders.Commands.AddProductToOrder;

public sealed class AddProductToOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IAddProductToOrderCommandHandler
{
    public async Task HandleAsync(AddProductToOrderCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            order = new Order(command.OrderId);
            await orderRepository.AddAsync(order, cancellationToken);
        }

        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new DomainException("Product was not found.");

        await unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            order.AddProduct(product, command.Quantity);
            productRepository.Update(product);
            await Task.CompletedTask;
        }, cancellationToken);
    }
}
