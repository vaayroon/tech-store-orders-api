using TechStoreOrders.Application.Abstractions.Currency;
using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Application.Products.Dtos;

namespace TechStoreOrders.Application.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler(
    IProductRepository productRepository,
    IExchangeRateClient exchangeRateClient) : IGetProductsQueryHandler
{
    public async Task<ProductListDto> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        var requestedCurrencyCode = string.IsNullOrWhiteSpace(query.CurrencyCode)
            ? "EUR"
            : query.CurrencyCode.Trim().ToUpperInvariant();

        var exchangeRate = await exchangeRateClient.GetRateFromEurAsync(requestedCurrencyCode, cancellationToken);

        var products = await productRepository.ListAsync(cancellationToken);
        var productDtos = products
            .Select(product => new ProductOutputDto(
                product.Id,
                product.Name,
                product.PriceEur,
                product.PriceEur * exchangeRate.Rate,
                product.Stock))
            .ToList();

        return new ProductListDto(
            requestedCurrencyCode,
            exchangeRate.CurrencyCode,
            exchangeRate.IsFallbackToEur,
            exchangeRate.Rate,
            productDtos);
    }
}
