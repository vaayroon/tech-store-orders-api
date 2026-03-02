using Microsoft.Extensions.DependencyInjection;
using TechStoreOrders.Application.Orders.Commands.AddProductToOrder;
using TechStoreOrders.Application.Orders.Commands.ConfirmOrder;
using TechStoreOrders.Application.Orders.Commands.CreateOrder;
using TechStoreOrders.Application.Orders.Commands.RemoveProductFromOrder;
using TechStoreOrders.Application.Orders.Commands.SendOrder;
using TechStoreOrders.Application.Orders.Queries.GetOrder;
using TechStoreOrders.Application.Products.Commands.AddProductStock;
using TechStoreOrders.Application.Products.Queries.GetProducts;

namespace TechStoreOrders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderCommandHandler, CreateOrderCommandHandler>();
        services.AddScoped<IAddProductToOrderCommandHandler, AddProductToOrderCommandHandler>();
        services.AddScoped<IRemoveProductFromOrderCommandHandler, RemoveProductFromOrderCommandHandler>();
        services.AddScoped<IConfirmOrderCommandHandler, ConfirmOrderCommandHandler>();
        services.AddScoped<ISendOrderCommandHandler, SendOrderCommandHandler>();
        services.AddScoped<IGetOrderQueryHandler, GetOrderQueryHandler>();
        services.AddScoped<IGetProductsQueryHandler, GetProductsQueryHandler>();
        services.AddScoped<IAddProductStockCommandHandler, AddProductStockCommandHandler>();

        return services;
    }
}
