namespace TechStoreOrders.Application.Products.Commands.AddProductStock;

public sealed record AddProductStockCommand(
    Guid ProductId,
    int Quantity);
