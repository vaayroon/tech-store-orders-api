namespace TechStoreOrders.Application.Orders.Dtos;

public sealed record OrderItemOutputDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPriceSnapshotEur,
    decimal UnitPriceConverted,
    decimal LineTotalEur,
    decimal LineTotalConverted);
