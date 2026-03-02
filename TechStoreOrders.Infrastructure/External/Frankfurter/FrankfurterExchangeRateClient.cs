using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using TechStoreOrders.Application.Abstractions.Currency;

namespace TechStoreOrders.Infrastructure.External.Frankfurter;

public sealed class FrankfurterExchangeRateClient(
    HttpClient httpClient,
    IMemoryCache memoryCache) : IExchangeRateClient
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public async Task<ExchangeRateResult> GetRateFromEurAsync(
        string requestedCurrencyCode,
        CancellationToken cancellationToken = default)
    {
        var normalizedCode = NormalizeCurrencyCode(requestedCurrencyCode);

        if (normalizedCode == "EUR")
        {
            return new ExchangeRateResult("EUR", 1m, false);
        }

        var cacheKey = $"frankfurter:latest:EUR:{normalizedCode}";
        if (memoryCache.TryGetValue(cacheKey, out ExchangeRateResult? cachedRate) && cachedRate is not null)
        {
            return cachedRate;
        }

        var endpoint = $"latest?from=EUR&to={normalizedCode}";
        FrankfurterLatestResponseDto? response;

        try
        {
            response = await httpClient.GetFromJsonAsync<FrankfurterLatestResponseDto>(endpoint, cancellationToken);
        }
        catch
        {
            return new ExchangeRateResult("EUR", 1m, true);
        }

        if (response?.Rates is not null && response.Rates.TryGetValue(normalizedCode, out var rate) && rate > 0m)
        {
            var result = new ExchangeRateResult(normalizedCode, rate, false);
            memoryCache.Set(cacheKey, result, CacheDuration);
            return result;
        }

        return new ExchangeRateResult("EUR", 1m, true);
    }

    private static string NormalizeCurrencyCode(string? currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return "EUR";
        }

        return currencyCode.Trim().ToUpperInvariant();
    }
}
