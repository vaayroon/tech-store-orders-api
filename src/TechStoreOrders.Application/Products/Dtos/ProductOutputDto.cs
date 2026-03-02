namespace TechStoreOrders.Application.Products.Dtos;

public sealed record ProductOutputDto(
    Guid Id,
    string Name,
    decimal PriceEur,
    decimal PriceConverted,
    int Stock);
