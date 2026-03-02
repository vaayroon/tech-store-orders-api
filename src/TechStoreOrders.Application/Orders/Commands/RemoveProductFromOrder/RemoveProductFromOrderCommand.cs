namespace TechStoreOrders.Application.Orders.Commands.RemoveProductFromOrder;

public sealed record RemoveProductFromOrderCommand(
    Guid OrderId,
    Guid ProductId);
