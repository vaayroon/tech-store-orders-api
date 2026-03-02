namespace TechStoreOrders.Application.Products.Dtos;

public sealed record ProductListDto(
    string RequestedCurrencyCode,
    string CurrencyCode,
    bool IsFallbackToEur,
    decimal ExchangeRate,
    IReadOnlyList<ProductOutputDto> Products);
