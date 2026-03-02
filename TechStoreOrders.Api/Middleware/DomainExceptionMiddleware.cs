using System.Text.Json;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Api.Middleware;

public sealed class DomainExceptionMiddleware(
    RequestDelegate next,
    ILogger<DomainExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException exception)
        {
            logger.LogWarning(exception, "Domain exception captured by middleware.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(new { message = exception.Message });
            await context.Response.WriteAsync(payload);
        }
    }
}
