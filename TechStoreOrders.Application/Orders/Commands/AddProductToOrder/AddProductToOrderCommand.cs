namespace TechStoreOrders.Application.Orders.Commands.AddProductToOrder;

public sealed record AddProductToOrderCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity);
