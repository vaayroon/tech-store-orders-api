using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Domain.Entities;

public sealed class OrderItem
{
    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPriceSnapshotEur { get; private set; }

    private OrderItem()
    {
    }

    public OrderItem(Guid productId, int quantity, decimal unitPriceSnapshotEur)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException("Product id is required.");
        }

        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        if (unitPriceSnapshotEur < 0m)
        {
            throw new DomainException("Unit price cannot be negative.");
        }

        ProductId = productId;
        Quantity = quantity;
        UnitPriceSnapshotEur = unitPriceSnapshotEur;
    }
}
