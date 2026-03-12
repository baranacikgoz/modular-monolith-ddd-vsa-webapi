
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Common.Tests;
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
                @event => @event.OwnerId.Should().Be(_ownerId),
                @event => @event.Name.Should().Be(Name),
                @event => @event.Description.Should().Be(Description),
                @event => @event.Address.Should().Be(Address));
    }

    [Fact]
    public void UpdateStoreShouldUpdateNameAndRaiseStoreNameUpdatedDomainEventWhenDifferentNameIsGiven()
    {
        const string newName = "New Store Name";

        Given(() => Store.Create(_ownerId, Name, Description, Address))
            .When(store => store.Update(name: newName, description: null, address: null))
            .Then(store => store.Name.Should().Be(newName))
            .Then<V1StoreNameUpdatedDomainEvent>(
                @event => @event.Name.Should().Be(newName));
    }

    [Fact]
    public void UpdateStoreShouldUpdateDescriptionAndRaiseStoreDescriptionUpdatedDomainEventWhenDifferentDescriptionIsGiven()
    {
        const string newDescription = "New Store Description";

        Given(() => Store.Create(_ownerId, Name, Description, Address))
            .When(store => store.Update(name: null, description: newDescription, address: null))
            .Then(store => store.Description.Should().Be(newDescription))
            .Then<V1StoreDescriptionUpdatedDomainEvent>(
                @event => @event.Description.Should().Be(newDescription));
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
            .Then(store => store.Products.Should().ContainSingle(p => p.Id == product.Id))
            .Then<V1ProductAddedToStoreDomainEvent>(
                @event => @event.StoreId.Should().Be(Aggregate.Id),
                @event => @event.Product.Id.Should().Be(product.Id),
                @event => @event.Product.Quantity.Should().Be(quantity),
                @event => @event.Product.Price.Should().Be(price));
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
            .Then(store => store.Products.Should().BeEmpty())
            .Then<V1ProductRemovedFromStoreDomainEvent>(
                @event => @event.Product.Should().Be(product));
    }
}
