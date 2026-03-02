namespace TechStoreOrders.Application.Abstractions.Currency;

public interface IExchangeRateClient
{
    Task<ExchangeRateResult> GetRateFromEurAsync(
        string requestedCurrencyCode,
        CancellationToken cancellationToken = default);
}
