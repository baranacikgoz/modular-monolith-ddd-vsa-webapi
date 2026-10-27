using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Xunit;

namespace Products.Tests.Stores;

/// <summary>Removing a product deletes its image rows with it, so the store announces the object keys that are left behind.</summary>
public class StoreRemoveProductImagesTests
{
    private const int MaxImages = 8;
    private readonly ApplicationUserId _ownerId = new(DefaultIdType.CreateVersion7());

    private (Store Store, Product Product) StoreWithProduct()
    {
        var store = Store.Create(_ownerId, "Sample Store", "Description", "Address");
        var product = Product.Create(store.Id, ProductTemplateId.New(), "Lamp", "Description", 1, 10m);
        store.AddProduct(product);
        return (store, product);
    }

    [Fact]
    public void RemoveProduct_ProductWithImages_RaisesAnEventListingEveryObjectKey()
    {
        var (store, product) = StoreWithProduct();
        product.AddImage("products/p/images/a.png", "image/png", 10, MaxImages);
        product.AddImage("products/p/images/b.jpg", "image/jpeg", 20, MaxImages);
        store.ClearEvents();

        var result = store.RemoveProduct(product);

        Assert.False(result.IsFailure);
        var @event = Assert.Single(store.Events.OfType<V1ProductImagesRemovedWithProductDomainEvent>());
        Assert.Equal(store.Id, @event.StoreId);
        Assert.Equal(product.Id, @event.ProductId);
        Assert.Equal(["products/p/images/a.png", "products/p/images/b.jpg"], @event.ObjectKeys);
    }

    [Fact]
    public void RemoveProduct_ProductWithoutImages_RaisesNoImageEvent()
    {
        var (store, product) = StoreWithProduct();
        store.ClearEvents();

        store.RemoveProduct(product);

        Assert.Empty(store.Events.OfType<V1ProductImagesRemovedWithProductDomainEvent>());
    }
}
