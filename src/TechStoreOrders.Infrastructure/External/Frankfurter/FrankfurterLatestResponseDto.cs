using System.Text.Json.Serialization;

namespace TechStoreOrders.Infrastructure.External.Frankfurter;

public sealed class FrankfurterLatestResponseDto
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("base")]
    public string Base { get; init; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; init; } = string.Empty;

    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; init; } = new(StringComparer.OrdinalIgnoreCase);
}
