using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Domain.Entities;

public sealed class OrderItem
{
    public Guid ProductId { get; }

    public int Quantity { get; }

    public decimal UnitPriceSnapshotEur { get; }

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
