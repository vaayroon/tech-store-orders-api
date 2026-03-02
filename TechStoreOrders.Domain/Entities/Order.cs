using TechStoreOrders.Domain.Enums;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Domain.Entities;

public sealed class Order
{
    private const decimal DiscountThreshold = 200m;
    private const decimal DiscountRate = 0.10m;

    public Guid Id { get; private set; }

    public OrderStatus Status { get; private set; }

    public List<OrderItem> Items { get; private set; } = [];

    private Order()
    {
    }

    public Order(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Order id is required.");
        }

        Id = id;
        Status = OrderStatus.Draft;
    }

    public decimal CalculateTotalEur()
    {
        var subtotal = Items.Sum(item => item.Quantity * item.UnitPriceSnapshotEur);

        if (subtotal > DiscountThreshold)
        {
            return subtotal * (1m - DiscountRate);
        }

        return subtotal;
    }

    public void Confirm()
    {
        if (Items.Count == 0)
        {
            throw new DomainException("Cannot confirm an order without items.");
        }

        if (Status != OrderStatus.Draft)
        {
            throw new DomainException("Only draft orders can be confirmed.");
        }

        Status = OrderStatus.Confirmed;
    }

    public void AddProduct(Product product, int qty)
    {
        if (Status != OrderStatus.Draft)
        {
            throw new DomainException("Confirmed orders are immutable.");
        }

        if (qty <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        if (product is null)
        {
            throw new DomainException("Product is required.");
        }

        if (product.Stock < qty)
        {
            throw new DomainException("Insufficient stock.");
        }

        product.ReduceStock(qty);

        var orderItem = new OrderItem(product.Id, qty, product.PriceEur);
        Items.Add(orderItem);
    }

    public int RemoveProduct(Guid productId)
    {
        if (Status != OrderStatus.Draft)
        {
            throw new DomainException("Confirmed orders are immutable.");
        }

        var itemsToRemove = Items
            .Where(item => item.ProductId == productId)
            .ToList();

        if (itemsToRemove.Count == 0)
        {
            throw new DomainException("Product is not part of the order.");
        }

        var restoredQuantity = itemsToRemove.Sum(item => item.Quantity);

        foreach (var item in itemsToRemove)
        {
            Items.Remove(item);
        }

        return restoredQuantity;
    }

    public void Send()
    {
        if (Status != OrderStatus.Confirmed)
        {
            throw new DomainException("Only confirmed orders can be shipped.");
        }

        Status = OrderStatus.Shipped;
    }
}
