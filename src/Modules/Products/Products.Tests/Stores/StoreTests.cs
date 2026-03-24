using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Xunit;

namespace Products.Tests.Stores;

public class StoreTests
{
    private readonly ApplicationUserId _ownerId = new(DefaultIdType.CreateVersion7());
    private const string Name = "Sample Store";
    private const string Description = "Sample Description";
    private const string Address = "Sample Address";

    [Fact]
    public void Create_WithValidParameters_ReturnsStoreAndRaisesEvent()
    {
        // Act
        var store = Store.Create(_ownerId, Name, Description, Address);

        // Assert
        Assert.NotNull(store);
        Assert.Equal(_ownerId, store.OwnerId);
        Assert.Equal(Name, store.Name);
        Assert.Equal(Description, store.Description);
        Assert.Equal(Address, store.Address);
        Assert.Empty(store.Products);

        var events = store.Events;
        Assert.Single(events);
        var @event = Assert.IsType<V1StoreCreatedDomainEvent>(events.First());

        Assert.Equal(store.Id, @event.StoreId);
        Assert.Equal(_ownerId, @event.OwnerId);
        Assert.Equal(Name, @event.Name);
        Assert.Equal(Description, @event.Description);
        Assert.Equal(Address, @event.Address);
    }

    [Fact]
    public void Update_AllProperties_UpdatesPropertiesAndRaisesEvents()
    {
        // Arrange
        var store = Store.Create(_ownerId, Name, Description, Address);
        store.ClearEvents();

        var newName = "Updated Name";
        var newDescription = "Updated Description";
        var newAddress = "Updated Address";

        // Act
        store.Update(newName, newDescription, newAddress);

        // Assert
        Assert.Equal(newName, store.Name);
        Assert.Equal(newDescription, store.Description);
        Assert.Equal(newAddress, store.Address);

        var events = store.Events;
        Assert.Equal(3, events.Count);

        var nameEvent = Assert.IsType<V1StoreNameUpdatedDomainEvent>(events.ElementAt(0));
        Assert.Equal(newName, nameEvent.Name);

        var descEvent = Assert.IsType<V1StoreDescriptionUpdatedDomainEvent>(events.ElementAt(1));
        Assert.Equal(newDescription, descEvent.Description);

        var addressEvent = Assert.IsType<V1StoreAddressUpdatedDomainEvent>(events.ElementAt(2));
        Assert.Equal(newAddress, addressEvent.Address);
    }

    [Fact]
    public void Update_WithSameValues_DoesNotRaiseEvents()
    {
        // Arrange
        var store = Store.Create(_ownerId, Name, Description, Address);
        store.ClearEvents();

        // Act
        store.Update(Name, Description, Address);

        // Assert
        Assert.Empty(store.Events);
    }

    [Fact]
    public void AddProduct_WithValidProduct_AddsToCollectionAndRaisesEvent()
    {
        // Arrange
        var store = Store.Create(_ownerId, Name, Description, Address);
        store.ClearEvents();

        var product = Product.Create(store.Id, ProductTemplateId.New(), "P", "D", 1, 10m);

        // Act
        store.AddProduct(product);

        // Assert
        Assert.Single(store.Products);
        Assert.Contains(product, store.Products);

        var events = store.Events;
        Assert.Single(events);

        var @event = Assert.IsType<V1ProductAddedToStoreDomainEvent>(events.First());
        Assert.Equal(product, @event.Product);
    }

    [Fact]
    public void RemoveProduct_WithExistingProduct_RemovesFromCollectionAndRaisesEvent()
    {
        // Arrange
        var store = Store.Create(_ownerId, Name, Description, Address);
        var product = Product.Create(store.Id, ProductTemplateId.New(), "P", "D", 1, 10m);
        store.AddProduct(product);
        store.ClearEvents();

        // Act
        store.RemoveProduct(product);

        // Assert
        Assert.Empty(store.Products);

        var events = store.Events;
        Assert.Single(events);

        var @event = Assert.IsType<V1ProductRemovedFromStoreDomainEvent>(events.First());
        Assert.Equal(product, @event.Product);
    }
}
