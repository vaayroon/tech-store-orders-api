using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechStoreOrders.Application.Abstractions.Currency;
using TechStoreOrders.Application.Abstractions.Persistence;
using TechStoreOrders.Infrastructure.External.Frankfurter;
using TechStoreOrders.Infrastructure.Persistence;
using TechStoreOrders.Infrastructure.Repositories;

namespace TechStoreOrders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "Data Source=techstore-orders.db";
        }

        services.AddDbContext<TechStoreDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddMemoryCache();
        services.AddHttpClient<IExchangeRateClient, FrankfurterExchangeRateClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.frankfurter.app/");
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
