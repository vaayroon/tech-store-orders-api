using Microsoft.Extensions.DependencyInjection;
using TechStoreOrders.Application.Orders.Commands.AddProductToOrder;
using TechStoreOrders.Application.Orders.Queries.GetOrder;

namespace TechStoreOrders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddProductToOrderCommandHandler, AddProductToOrderCommandHandler>();
        services.AddScoped<IGetOrderQueryHandler, GetOrderQueryHandler>();

        return services;
    }
}
