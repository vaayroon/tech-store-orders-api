using TechStoreOrders.Application.Orders.Dtos;

namespace TechStoreOrders.Application.Orders.Queries.GetOrder;

public interface IGetOrderQueryHandler
{
    Task<OrderDetailsDto> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken = default);
}
