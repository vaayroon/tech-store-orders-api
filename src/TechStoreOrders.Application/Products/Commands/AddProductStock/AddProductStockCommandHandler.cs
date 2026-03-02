using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Products.Commands.AddProductStock;

public sealed class AddProductStockCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IAddProductStockCommandHandler
{
    public async Task HandleAsync(AddProductStockCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new DomainException("Product was not found.");

        product.AddStock(command.Quantity);
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
