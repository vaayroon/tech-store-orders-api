using Microsoft.AspNetCore.Mvc;
using TechStoreOrders.Api.Contracts;
using TechStoreOrders.Application.Products.Commands.AddProductStock;
using TechStoreOrders.Application.Products.Queries.GetProducts;

namespace TechStoreOrders.Api.Controllers;

[ApiController]
[Route("products")]
public sealed class ProductsController(
    IGetProductsQueryHandler getProductsQueryHandler,
    IAddProductStockCommandHandler addProductStockCommandHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] string? currency, CancellationToken cancellationToken)
    {
        var result = await getProductsQueryHandler.HandleAsync(new GetProductsQuery(currency ?? "EUR"), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id:guid}/stock")]
    public async Task<IActionResult> AddStock(Guid id, [FromBody] AddProductStockRequest request, CancellationToken cancellationToken)
    {
        await addProductStockCommandHandler.HandleAsync(new AddProductStockCommand(id, request.Quantity), cancellationToken);
        return NoContent();
    }
}
