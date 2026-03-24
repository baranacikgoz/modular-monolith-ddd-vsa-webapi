
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Xunit;

namespace Products.UnitTests;

public class StoreTests : AggregateTests<Store, StoreId>
{
    private static readonly ApplicationUserId _ownerId = ApplicationUserId.New();
    private const string Name = "Store Name";
    private const string Description = "Store Description";
    private const string Address = "Store Address";
    // LogoUrl removed from Domain
    private static readonly ProductTemplate _productTemplate = ProductTemplate.Create("Brand", "Model", "Color");

    [Fact]
    public void CreateStoreShouldRaiseStoreCreatedDomainEvent()
    {
        Given(() => Store.Create(_ownerId, Name, Description, Address))
            .Then<V1StoreCreatedDomainEvent>(
                @event => Assert.Equal(_ownerId, @event.OwnerId),
                @event => Assert.Equal(Name, @event.Name),
                @event => Assert.Equal(Description, @event.Description),
                @event => Assert.Equal(Address, @event.Address));
    }

    [Fact]
    public void UpdateStoreShouldUpdateNameAndRaiseStoreNameUpdatedDomainEventWhenDifferentNameIsGiven()
    {
        const string newName = "New Store Name";

        Given(() => Store.Create(_ownerId, Name, Description, Address))
            .When(store => store.Update(name: newName, description: null, address: null))
            .Then(store => Assert.Equal(newName, store.Name))
            .Then<V1StoreNameUpdatedDomainEvent>(
                @event => Assert.Equal(newName, @event.Name));
    }

    [Fact]
    public void UpdateStoreShouldUpdateDescriptionAndRaiseStoreDescriptionUpdatedDomainEventWhenDifferentDescriptionIsGiven()
    {
        const string newDescription = "New Store Description";

        Given(() => Store.Create(_ownerId, Name, Description, Address))
            .When(store => store.Update(name: null, description: newDescription, address: null))
            .Then(store => Assert.Equal(newDescription, store.Description))
            .Then<V1StoreDescriptionUpdatedDomainEvent>(
                @event => Assert.Equal(newDescription, @event.Description));
    }

    [Fact]
    public void AddProductShouldAddProductAndRaiseProductAddedToStoreDomainEvent()
    {
        const int quantity = 10;
        const decimal price = 100;
        const string prodName = "Product Name";
        const string prodDesc = "Product Desc";

        var store = Store.Create(_ownerId, Name, Description, Address);
        var product = Product.Create(store.Id, _productTemplate.Id, prodName, prodDesc, quantity, price);

        Given(() => store)
            .When(store => store.AddProduct(product))
            .Then(store => Assert.Single(store.Products, p => p.Id == product.Id))
            .Then<V1ProductAddedToStoreDomainEvent>(
                @event => Assert.Equal(Aggregate.Id, @event.StoreId),
                @event => Assert.Equal(product.Id, @event.Product.Id),
                @event => Assert.Equal(quantity, @event.Product.Quantity),
                @event => Assert.Equal(price, @event.Product.Price));
    }

    [Fact]
    public void RemoveProductFromStoreShouldRemoveAndRaiseProductRemovedFromStoreDomainEvent()
    {
        const int quantity = 10;
        const decimal price = 100;
        const string prodName = "Product Name";
        const string prodDesc = "Product Desc";

        var store = Store.Create(_ownerId, Name, Description, Address);
        var product = Product.Create(store.Id, _productTemplate.Id, prodName, prodDesc, quantity, price);

        Given(() => store)
            .When(store => store.AddProduct(product))
            .When(store => store.RemoveProduct(product))
            .Then(store => Assert.Empty(store.Products))
            .Then<V1ProductRemovedFromStoreDomainEvent>(
                @event => Assert.Equal(product, @event.Product));
    }
}
