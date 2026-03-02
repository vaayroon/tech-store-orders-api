namespace TechStoreOrders.Application.Abstractions.Currency;

public sealed record ExchangeRateResult(
    string CurrencyCode,
    decimal Rate,
    bool IsFallbackToEur);
