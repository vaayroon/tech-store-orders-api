using Microsoft.AspNetCore.Mvc;
using TechStoreOrders.Api.Contracts;
using TechStoreOrders.Application.Orders.Commands.AddProductToOrder;
using TechStoreOrders.Application.Orders.Commands.ConfirmOrder;
using TechStoreOrders.Application.Orders.Commands.CreateOrder;
using TechStoreOrders.Application.Orders.Commands.RemoveProductFromOrder;
using TechStoreOrders.Application.Orders.Commands.SendOrder;
using TechStoreOrders.Application.Orders.Queries.GetOrder;

namespace TechStoreOrders.Api.Controllers;

[ApiController]
[Route("orders")]
public sealed class OrdersController(
    ICreateOrderCommandHandler createOrderCommandHandler,
    IAddProductToOrderCommandHandler addProductToOrderCommandHandler,
    IRemoveProductFromOrderCommandHandler removeProductFromOrderCommandHandler,
    IConfirmOrderCommandHandler confirmOrderCommandHandler,
    ISendOrderCommandHandler sendOrderCommandHandler,
    IGetOrderQueryHandler getOrderQueryHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var orderId = await createOrderCommandHandler.HandleAsync(new CreateOrderCommand(), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = orderId, currency = "EUR" }, new { orderId });
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        var command = new AddProductToOrderCommand(id, request.ProductId, request.Quantity);
        await addProductToOrderCommandHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}/items/{productId:guid}")]
    public async Task<IActionResult> RemoveItem(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var command = new RemoveProductFromOrderCommand(id, productId);
        await removeProductFromOrderCommandHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id, CancellationToken cancellationToken)
    {
        await confirmOrderCommandHandler.HandleAsync(new ConfirmOrderCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/send")]
    public async Task<IActionResult> Send(Guid id, CancellationToken cancellationToken)
    {
        await sendOrderCommandHandler.HandleAsync(new SendOrderCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromQuery] string? currency, CancellationToken cancellationToken)
    {
        var result = await getOrderQueryHandler.HandleAsync(new GetOrderQuery(id, currency ?? "EUR"), cancellationToken);
        return Ok(result);
    }
}
