#pragma warning disable S125 // Sections of code should not be commented out
//using Common.Domain.StronglyTypedIds;
#pragma warning restore S125 // Sections of code should not be commented out
//using FluentAssertions;
//using Products.Domain.Products;
//using Products.Domain.ProductTemplates;
//using Products.Domain.Stores;
//using Products.Domain.Stores.DomainEvents.v1;
//using UnitTests.Common;
//using Xunit;

//namespace Products.UnitTests;

//internal class StoreTests : AggregateTests<Store, StoreId>
//{
//    private static readonly ApplicationUserId _ownerId = ApplicationUserId.New();
//    private const string Name = "Store Name";
//    private const string Description = "Store Description";
//    private const string Address = "Store Address";
//    private static readonly Uri _logoUrl = new("https://store.com/logo.png");
//    private static readonly ProductTemplate _productTemplate = ProductTemplate.Create("Brand", "Model", "Color");

//    [Fact]
//    public void CreateStoreShouldRaiseStoreCreatedDomainEvent()
//    {
//        Given(() => Store.Create(_ownerId, Name, Description, Address, _logoUrl))
//            .Then<V1StoreCreatedDomainEvent>(
//                @event => @event.OwnerId.Should().Be(_ownerId),
//                @event => @event.Name.Should().Be(Name),
//                @event => @event.Description.Should().Be(Description),
//                @event => @event.LogoUrl.Should().Be(_logoUrl));
//    }

//    [Fact]
//    public void UpdateStoreShouldUpdateNameAndRaiseStoreNameUpdatedDomainEventWhenDifferentNameIsGiven()
//    {
//        const string newName = "New Store Name";

//        Given(() => Store.Create(_ownerId, Name, Description, Address, _logoUrl))
//            .When(store => store.Update(name: newName, description: null, address: null))
//            .Then(store => store.Name.Should().Be(newName))
//            .Then<V1StoreNameUpdatedDomainEvent>(
//                @event => @event.Name.Should().Be(newName));
//    }

//    [Fact]
//    public void UpdateStoreShouldUpdateDescriptionAndRaiseStoreDescriptionUpdatedDomainEventWhenDifferentDescriptionIsGiven()
//    {
//        const string newDescription = "New Store Description";

//        Given(() => Store.Create(_ownerId, Name, Description, Address, _logoUrl))
//            .When(store => store.Update(name: null, description: newDescription, address: null))
//            .Then(store => store.Description.Should().Be(newDescription))
//            .Then<V1StoreDescriptionUpdatedDomainEvent>(
//                @event => @event.Description.Should().Be(newDescription));
//    }

//    [Fact]
//    public void AddProductShouldAddProductAndRaiseProductAddedToStoreDomainEvent()
//    {
//        const int quantity = 10;
//        const decimal price = 100;

//        var store = Store.Create(_ownerId, Name, Description, Address, _logoUrl);
//        var product = Product.Create(store.Id, _productTemplate.Id, quantity, price);

//        Given(() => store)
//            .When(store => store.AddProduct(product))
//            .Then(store => store.Products.Should().ContainSingle(p => p.Id == product.Id))
//            .Then<V1ProductAddedToStoreDomainEvent>(
//                @event => @event.StoreId.Should().Be(Aggregate.Id),
//                @event => @event.Product.Id.Should().Be(product.Id),
//                @event => @event.Product.Quantity.Should().Be(quantity),
//                @event => @event.Product.Price.Should().Be(price));
//    }

//    [Fact]
//    public void UpdateProductShouldUpdateQuantityAndRaiseProductQuantityIncreasedDomainEventWhenNewQuantityIsGreaterThanOldQuantity()
//    {
//        const int quantity = 10;
//        const decimal price = 100;

//        const int newQuantity = 20;

//        var store = Store.Create(_ownerId, Name, Description, Address, _logoUrl);
//        var product = Product.Create(store.Id, _productTemplate.Id, quantity, price);

//        Given(() => store)
//            .When(store => store.AddProduct(product))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: newQuantity, newPrice: null))
//            .Then(store => store.Products.Single().Quantity.Should().Be(newQuantity))
//            .Then<V1ProductQuantityIncreasedDomainEvent>(
//                (store, product, @event) => @event.Product.Should().Be((Product)product),
//                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
//    }

//    [Fact]
//    public void UpdateProductShouldUpdateQuantityAndRaiseProductQuantityDecreasedDomainEventWhenNewQuantityIsLessThanOldQuantity()
//    {
//        var productId = ProductTemplateId.New();
//        const int quantity = 10;
//        const decimal price = 100;

//        const int newQuantity = 5;

//        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
//            .When(store => store.AddProduct(productId, quantity, price))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: newQuantity, newPrice: null))
//            .Then(store => store.Products.Single().Quantity.Should().Be(newQuantity))
//            .Then<V1ProductQuantityDecreasedDomainEvent>(
//                (store, product, @event) => @event.Product.Should().Be((Product)product),
//                (_, _, @event) => @event.NewQuantity.Should().Be(newQuantity));
//    }

//    [Fact]
//    public void UpdateProductShouldNotRaiseEventWhenNewQuantityIsEqualToOldQuantity()
//    {
//        var productId = ProductTemplateId.New();
//        const int quantity = 10;
//        const decimal price = 100;

//        const int newQuantity = 10;

//        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
//            .When(store => store.AddProduct(productId, quantity, price))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: newQuantity, newPrice: null))
//            .ThenNoEventsOfType<V1ProductPriceIncreasedDomainEvent>()
//            .ThenNoEventsOfType<V1ProductPriceDecreasedDomainEvent>();
//    }

//    [Fact]
//    public void UpdateProductShouldUpdatePriceAndRaiseProductPriceIncreasedDomainEventWhenNewPriceIsGreaterThanOldPrice()
//    {
//        var productId = ProductTemplateId.New();
//        const int quantity = 10;
//        const decimal price = 100;

//        const decimal newPrice = 200;

//        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
//            .When(store => store.AddProduct(productId, quantity, price))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: null, newPrice: newPrice))
//            .Then(store => store.Products.Single().Price.Should().Be(newPrice))
//            .Then<V1ProductPriceIncreasedDomainEvent>(
//                (store, product, @event) => @event.Product.Should().Be((Product)product),
//                (_, _, @event) => @event.NewPrice.Should().Be(newPrice));
//    }

//    [Fact]
//    public void UpdateProductShouldUpdatePriceAndRaiseProductPriceDecreasedDomainEventWhenNewPriceIsLessThanOldPrice()
//    {
//        var productId = ProductTemplateId.New();
//        const int quantity = 10;
//        const decimal price = 100;

//        const decimal newPrice = 50;

//        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
//            .When(store => store.AddProduct(productId, quantity, price))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: null, newPrice: newPrice))
//            .Then(store => store.Products.Single().Price.Should().Be(newPrice))
//            .Then<V1ProductPriceDecreasedDomainEvent>(
//                (store, product, @event) => @event.Product.Should().Be((Product)product),
//                (_, _, @event) => @event.NewPrice.Should().Be(newPrice));
//    }

//    [Fact]
//    public void UpdateProductShouldNotRaiseEventWhenNewPriceIsEqualToOldPrice()
//    {
//        var productId = ProductTemplateId.New();
//        const int quantity = 10;
//        const decimal price = 100;

//        const decimal newPrice = 100;

//        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
//            .When(store => store.AddProduct(productId, quantity, price))
//            .When((store, product) => store.UpdateProduct(((Product)product).Id, newQuantity: null, newPrice: newPrice))
//            .ThenNoEventsOfType<V1ProductPriceIncreasedDomainEvent>()
//            .ThenNoEventsOfType<V1ProductPriceDecreasedDomainEvent>();
//    }

//    [Fact]
//    public void RemoveProductFromStoreShouldRemoveAndRaiseProductRemovedFromStoreDomainEvent()
//    {
//        const int quantity = 10;
//        const decimal price = 100;

//        var store = Store.Create(_ownerId, Name, Description, Address, _logoUrl);
//        var product = Product.Create(store.Id, _productTemplate.Id, quantity, price);

//        Given(() => store)
//            .When(store => store.AddProduct(product))
//            .When(store => store.RemoveProduct(product))
//            .Then(store => store.Products.Should().BeEmpty())
//            .Then<V1ProductRemovedFromStoreDomainEvent>(
//                (_, product, @event) => @event.Product.Should().Be(product));
//    }
//}
