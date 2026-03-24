using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Products;

public class ProductTests
{
    private readonly StoreId _storeId = StoreId.New();
    private readonly ProductTemplateId _templateId = ProductTemplateId.New();
    private const string Name = "Sample Product";
    private const string Description = "Sample Description";
    private const int Quantity = 100;
    private const decimal Price = 99.99m;

    [Fact]
    public void Create_WithValidParameters_ReturnsProductAndRaisesEvent()
    {
        // Act
        var product = Product.Create(_storeId, _templateId, Name, Description, Quantity, Price);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(_storeId, product.StoreId);
        Assert.Equal(_templateId, product.ProductTemplateId);
        Assert.Equal(Name, product.Name);
        Assert.Equal(Description, product.Description);
        Assert.Equal(Quantity, product.Quantity);
        Assert.Equal(Price, product.Price);

        var events = product.Events;
        Assert.Single(events);
        var @event = Assert.IsType<V1ProductCreatedDomainEvent>(events.First());

        Assert.Equal(product.Id, @event.ProductId);
        Assert.Equal(_storeId, @event.StoreId);
        Assert.Equal(_templateId, @event.ProductTemplateId);
        Assert.Equal(Name, @event.Name);
        Assert.Equal(Description, @event.Description);
        Assert.Equal(Quantity, @event.Quantity);
        Assert.Equal(Price, @event.Price);
    }

    [Fact]
    public void Update_NameAndDescription_UpdatesPropertiesAndRaisesEvents()
    {
        // Arrange
        var product = Product.Create(_storeId, _templateId, Name, Description, Quantity, Price);
        product.ClearEvents();

        var newName = "Updated Name";
        var newDescription = "Updated Description";

        // Act
        product.Update(newName, newDescription, null, null);

        // Assert
        Assert.Equal(newName, product.Name);
        Assert.Equal(newDescription, product.Description);
        Assert.Equal(Quantity, product.Quantity); // Unchanged
        Assert.Equal(Price, product.Price); // Unchanged

        var events = product.Events;
        Assert.Equal(2, events.Count);

        var nameEvent = Assert.IsType<V1ProductNameUpdatedDomainEvent>(events.ElementAt(0));
        Assert.Equal(newName, nameEvent.Name);

        var descEvent = Assert.IsType<V1ProductDescriptionUpdatedDomainEvent>(events.ElementAt(1));
        Assert.Equal(newDescription, descEvent.Description);
    }

    [Fact]
    public void Update_IncreaseQuantityAndPrice_UpdatesPropertiesAndRaisesIncreasedEvents()
    {
        // Arrange
        var product = Product.Create(_storeId, _templateId, Name, Description, Quantity, Price);
        product.ClearEvents();

        var newQuantity = 150;
        var newPrice = 149.99m;

        // Act
        product.Update(null, null, newQuantity, newPrice);

        // Assert
        Assert.Equal(Name, product.Name); // Unchanged
        Assert.Equal(newQuantity, product.Quantity);
        Assert.Equal(newPrice, product.Price);

        var events = product.Events;
        Assert.Equal(2, events.Count);

        var qtyEvent = Assert.IsType<V1ProductQuantityIncreasedDomainEvent>(events.ElementAt(0));
        Assert.Equal(newQuantity, qtyEvent.Quantity);

        var priceEvent = Assert.IsType<V1ProductPriceIncreasedDomainEvent>(events.ElementAt(1));
        Assert.Equal(newPrice, priceEvent.Price);
    }

    [Fact]
    public void Update_DecreaseQuantityAndPrice_UpdatesPropertiesAndRaisesDecreasedEvents()
    {
        // Arrange
        var product = Product.Create(_storeId, _templateId, Name, Description, Quantity, Price);
        product.ClearEvents();

        var newQuantity = 50;
        var newPrice = 49.99m;

        // Act
        product.Update(null, null, newQuantity, newPrice);

        // Assert
        var events = product.Events;
        Assert.Equal(2, events.Count);

        var qtyEvent = Assert.IsType<V1ProductQuantityDecreasedDomainEvent>(events.ElementAt(0));
        Assert.Equal(newQuantity, qtyEvent.Quantity);

        var priceEvent = Assert.IsType<V1ProductPriceDecreasedDomainEvent>(events.ElementAt(1));
        Assert.Equal(newPrice, priceEvent.Price);
    }

    [Fact]
    public void Update_WithSameValues_DoesNotRaiseEvents()
    {
        // Arrange
        var product = Product.Create(_storeId, _templateId, Name, Description, Quantity, Price);
        product.ClearEvents();

        // Act (Sending exactly the identical values)
        product.Update(Name, Description, Quantity, Price);

        // Assert
        var events = product.Events;
        Assert.Empty(events);
    }
}
