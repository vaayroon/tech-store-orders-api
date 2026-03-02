namespace TechStoreOrders.Application.Orders.Queries.GetOrder;

public sealed record GetOrderQuery(
    Guid OrderId,
    string CurrencyCode = "EUR");
