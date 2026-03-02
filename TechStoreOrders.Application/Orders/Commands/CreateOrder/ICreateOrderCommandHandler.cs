namespace TechStoreOrders.Application.Orders.Commands.CreateOrder;

public interface ICreateOrderCommandHandler
{
    Task<Guid> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default);
}
