using TechStoreOrders.Application.Abstractions.Currency;
using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Application.Orders.Dtos;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Application.Orders.Queries.GetOrder;

public sealed class GetOrderQueryHandler(
    IOrderRepository orderRepository,
    IExchangeRateClient exchangeRateClient) : IGetOrderQueryHandler
{
    public async Task<OrderDetailsDto> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(query.OrderId, cancellationToken)
            ?? throw new DomainException("Order was not found.");

        var requestedCurrencyCode = string.IsNullOrWhiteSpace(query.CurrencyCode)
            ? "EUR"
            : query.CurrencyCode.Trim().ToUpperInvariant();

        var exchangeRate = await exchangeRateClient.GetRateFromEurAsync(requestedCurrencyCode, cancellationToken);

        var subtotalEur = order.Items.Sum(item => item.Quantity * item.UnitPriceSnapshotEur);
        var totalEur = order.CalculateTotalEur();
        var discountAppliedEur = subtotalEur - totalEur;

        var itemDtos = order.Items
            .Select(item =>
            {
                var unitPriceConverted = item.UnitPriceSnapshotEur * exchangeRate.Rate;
                var lineTotalEur = item.Quantity * item.UnitPriceSnapshotEur;
                var lineTotalConverted = lineTotalEur * exchangeRate.Rate;

                return new OrderItemOutputDto(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPriceSnapshotEur,
                    unitPriceConverted,
                    lineTotalEur,
                    lineTotalConverted);
            })
            .ToList();

        return new OrderDetailsDto(
            order.Id,
            order.Status.ToString(),
            requestedCurrencyCode,
            exchangeRate.CurrencyCode,
            exchangeRate.IsFallbackToEur,
            exchangeRate.Rate,
            totalEur,
            discountAppliedEur,
            totalEur * exchangeRate.Rate,
            itemDtos);
    }
}
