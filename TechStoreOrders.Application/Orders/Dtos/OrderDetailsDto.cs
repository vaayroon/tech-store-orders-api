namespace TechStoreOrders.Application.Orders.Dtos;

public sealed record OrderDetailsDto(
    Guid OrderId,
    string Status,
    string RequestedCurrencyCode,
    string CurrencyCode,
    bool IsFallbackToEur,
    decimal ExchangeRate,
    decimal TotalEur,
    decimal DiscountAppliedEur,
    decimal TotalConverted,
    IReadOnlyList<OrderItemOutputDto> Items);
