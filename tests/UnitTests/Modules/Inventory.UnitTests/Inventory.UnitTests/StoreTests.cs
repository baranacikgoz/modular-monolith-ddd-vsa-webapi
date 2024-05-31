using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Inventory.Domain.Stores.DomainEvents;
using UnitTests.Common;
using Xunit;

namespace Inventory.UnitTests;

public class StoreTests : AggregateTests<Store, StoreId>
{
    private static readonly ApplicationUserId _ownerId = ApplicationUserId.New();
    private const string Name = "Store Name";
    private const string Description = "Store Description";
    private static readonly Uri _logoUrl = new("https://store.com/logo.png");

    [Fact]
    public void CreateStoreShouldRaiseStoreCreatedDomainEvent()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .Then<StoreCreatedDomainEvent>(
                @event => @event.OwnerId.Should().Be(_ownerId),
                @event => @event.Name.Should().Be(Name),
                @event => @event.Description.Should().Be(Description),
                @event => @event.LogoUrl.Should().Be(_logoUrl));
    }

    [Fact]
    public void UpdateStoreNameShouldRaiseStoreNameUpdatedDomainEvent()
    {
        const string newName = "New Store Name";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: newName, description: null))
            .Then<StoreNameUpdatedDomainEvent>(
                @event => @event.OldName.Should().Be(Name),
                @event => @event.NewName.Should().Be(newName));
    }

    [Fact]
    public void UpdateStoreDescriptionShouldRaiseStoreDescriptionUpdatedDomainEvent()
    {
        const string newDescription = "New Store Description";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: null, description: newDescription))
            .Then<StoreDescriptionUpdatedDomainEvent>(
                @event => @event.OldDescription.Should().Be(Description),
                @event => @event.NewDescription.Should().Be(newDescription));
    }

    [Fact]
    public void UpdateStoreNameShouldReturnSameValueErrorWhenSameValueIsGiven()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: Name, description: null))
            .ThenError(
                error => error.ShouldBe(Error.SameValue(nameof(Store.Name), Name)));
    }

    [Fact]
    public void UpdateStoreDescriptionShouldReturnSameValueErrorWhenSameValueIsGiven()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: null, description: Description))
            .ThenError(
                error => error.ShouldBe(Error.SameValue(nameof(Store.Description), Description)));
    }

    [Fact]
    public void AddProductShouldRaiseProductAddedToStoreDomainEvent()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .Then<ProductAddedToStoreDomainEvent>(
                @event => @event.StoreId.Should().Be(Aggregate.Id),
                @event => @event.Product.ProductId.Should().Be(productId),
                @event => @event.Product.Quantity.Should().Be(quantity),
                @event => @event.Product.Price.Should().Be(price));
    }

    [Fact]
    public void UpdateProductQuantityShouldRaiseProductQuantityIncreasedDomainEventWhenNewQuantityIsGreaterThanOldQuantity()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;
        const int newQuantity = 20;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProductQuantity(((StoreProduct)product).Id, newQuantity))
            .Then<ProductQuantityIncreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void UpdateProductQuantityShouldRaiseProductQuantityDecreasedDomainEventWhenNewQuantityIsLessThanOldQuantity()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;
        const int newQuantity = 5;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProductQuantity(((StoreProduct)product).Id, newQuantity))
            .Then<ProductQuantityDecreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void UpdateProductQuantityShouldReturnSameValueErrorWhenSameValueIsGiven()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProductQuantity(((StoreProduct)product).Id, quantity))
            .ThenError(
                error => error.ShouldBe(Error.SameValue(nameof(StoreProduct.Quantity), quantity)));
    }

    [Fact]
    public void RemoveProductFromStoreShouldRaiseProductRemovedFromStoreDomainEvent()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.RemoveProductFromStore(((StoreProduct)product).Id))
            .Then<ProductRemovedFromStoreDomainEvent>(
                (_, product, @event) => @event.Product.Should().Be((StoreProduct)product));
    }
}
