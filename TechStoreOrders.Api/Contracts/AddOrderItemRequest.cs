namespace TechStoreOrders.Api.Contracts;

public sealed record AddOrderItemRequest(Guid ProductId, int Quantity);
