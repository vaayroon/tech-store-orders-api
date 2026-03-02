using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Domain.Entities;

public sealed class Product
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public decimal PriceEur { get; private set; }

    public int Stock { get; private set; }

    private Product()
    {
    }

    public Product(Guid id, string name, decimal priceEur, int stock)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Product id is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Product name is required.");
        }

        if (priceEur < 0m)
        {
            throw new DomainException("Product price cannot be negative.");
        }

        if (stock < 0)
        {
            throw new DomainException("Product stock cannot be negative.");
        }

        Id = id;
        Name = name;
        PriceEur = priceEur;
        Stock = stock;
    }

    public void ReduceStock(int qty)
    {
        if (qty <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        if (Stock < qty)
        {
            throw new DomainException("Insufficient stock.");
        }

        Stock -= qty;
    }

    public void AddStock(int qty)
    {
        if (qty <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        Stock += qty;
    }
}
