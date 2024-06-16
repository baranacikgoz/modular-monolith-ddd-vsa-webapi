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
using Inventory.Domain.Stores.DomainEvents.v1;
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
            .Then<V1StoreCreatedDomainEvent>(
                @event => @event.OwnerId.Should().Be(_ownerId),
                @event => @event.Name.Should().Be(Name),
                @event => @event.Description.Should().Be(Description),
                @event => @event.LogoUrl.Should().Be(_logoUrl));
    }

    [Fact]
    public void UpdateStoreShouldUpdateNameAndRaiseStoreNameUpdatedDomainEventWhenDifferentNameIsGiven()
    {
        const string newName = "New Store Name";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: newName, description: null))
            .Then(store => store.Name.Should().Be(newName))
            .Then<V1StoreNameUpdatedDomainEvent>(
                @event => @event.OldName.Should().Be(Name),
                @event => @event.NewName.Should().Be(newName));
    }

    [Fact]
    public void UpdateStoreShouldUpdateDescriptionAndRaiseStoreDescriptionUpdatedDomainEventWhenDifferentDescriptionIsGiven()
    {
        const string newDescription = "New Store Description";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: null, description: newDescription))
            .Then(store => store.Description.Should().Be(newDescription))
            .Then<V1StoreDescriptionUpdatedDomainEvent>(
                @event => @event.OldDescription.Should().Be(Description),
                @event => @event.NewDescription.Should().Be(newDescription));
    }

    [Fact]
    public void AddProductShouldAddProductAndRaiseProductAddedToStoreDomainEvent()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .Then(store => store.Products.Should().ContainSingle(product => product.ProductId == productId))
            .Then<V1ProductAddedToStoreDomainEvent>(
                @event => @event.StoreId.Should().Be(Aggregate.Id),
                @event => @event.Product.ProductId.Should().Be(productId),
                @event => @event.Product.Quantity.Should().Be(quantity),
                @event => @event.Product.Price.Should().Be(price));
    }

    [Fact]
    public void UpdateProductShouldUpdateQuantityAndRaiseProductQuantityIncreasedDomainEventWhenNewQuantityIsGreaterThanOldQuantity()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const int newQuantity = 20;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: newQuantity, newPrice: null))
            .Then(store => store.Products.Single().Quantity.Should().Be(newQuantity))
            .Then<V1ProductQuantityIncreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void UpdateProductShouldUpdateQuantityAndRaiseProductQuantityDecreasedDomainEventWhenNewQuantityIsLessThanOldQuantity()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const int newQuantity = 5;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: newQuantity, newPrice: null))
            .Then(store => store.Products.Single().Quantity.Should().Be(newQuantity))
            .Then<V1ProductQuantityDecreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void UpdateProductShouldNotRaiseEventWhenNewQuantityIsEqualToOldQuantity()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const int newQuantity = 10;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: newQuantity, newPrice: null))
            .ThenNoEventsOfType<V1ProductPriceIncreasedDomainEvent>()
            .ThenNoEventsOfType<V1ProductPriceDecreasedDomainEvent>();
    }

    [Fact]
    public void UpdateProductShouldUpdatePriceAndRaiseProductPriceIncreasedDomainEventWhenNewPriceIsGreaterThanOldPrice()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const decimal newPrice = 200;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: null, newPrice: newPrice))
            .Then(store => store.Products.Single().Price.Should().Be(newPrice))
            .Then<V1ProductPriceIncreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewPrice.Should().Be(newPrice));
    }

    [Fact]
    public void UpdateProductShouldUpdatePriceAndRaiseProductPriceDecreasedDomainEventWhenNewPriceIsLessThanOldPrice()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const decimal newPrice = 50;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: null, newPrice: newPrice))
            .Then(store => store.Products.Single().Price.Should().Be(newPrice))
            .Then<V1ProductPriceDecreasedDomainEvent>(
                (store, product, @event) => @event.Product.Should().Be((StoreProduct)product),
                (_, _, @event) => @event.NewPrice.Should().Be(newPrice));
    }

    [Fact]
    public void UpdateProductShouldNotRaiseEventWhenNewPriceIsEqualToOldPrice()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        const decimal newPrice = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.UpdateProduct(((StoreProduct)product).Id, newQuantity: null, newPrice: newPrice))
            .ThenNoEventsOfType<V1ProductPriceIncreasedDomainEvent>()
            .ThenNoEventsOfType<V1ProductPriceDecreasedDomainEvent>();
    }

    [Fact]
    public void RemoveProductFromStoreShouldRemoveAndRaiseProductRemovedFromStoreDomainEvent()
    {
        var productId = ProductId.New();
        const int quantity = 10;
        const decimal price = 100;

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.AddProduct(productId, quantity, price))
            .When((store, product) => store.RemoveProductFromStore(((StoreProduct)product).Id))
            .Then(store => store.Products.Should().BeEmpty())
            .Then<V1ProductRemovedFromStoreDomainEvent>(
                (_, product, @event) => @event.Product.Should().Be((StoreProduct)product));
    }
}
