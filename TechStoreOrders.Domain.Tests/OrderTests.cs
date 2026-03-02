using TechStoreOrders.Domain.Entities;
using TechStoreOrders.Domain.Exceptions;

namespace TechStoreOrders.Domain.Tests;

public sealed class OrderTests
{
    [Fact]
    public void Order_Should_Apply_Discount_When_Over_200_Eur()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "Running Sneakers", 120m, 10);

        // Act
        order.AddProduct(product, 2);
        var total = order.CalculateTotalEur();

        // Assert
        Assert.Equal(216m, total);
    }

    [Fact]
    public void Order_Should_Throw_If_Confirmed_Without_Items()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());

        // Act
        var action = () => order.Confirm();

        // Assert
        var exception = Assert.Throws<DomainException>(action);
        Assert.Equal("Cannot confirm an order without items.", exception.Message);
    }

    [Fact]
    public void Order_Should_Freeze_Price_At_Addition_Time()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "Hoodie", 75m, 20);

        // Act
        order.AddProduct(product, 1);
        product.UpdatePriceEur(95m);

        // Assert
        var addedItem = Assert.Single(order.Items);
        Assert.Equal(75m, addedItem.UnitPriceSnapshotEur);
    }
}
