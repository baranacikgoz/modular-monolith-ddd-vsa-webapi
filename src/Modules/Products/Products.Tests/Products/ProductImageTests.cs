using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Products;

public class ProductImageTests
{
    private const int MaxImages = 2;

    private static Product NewProduct()
    {
        var product = Product.Create(StoreId.New(), ProductTemplateId.New(), "Sample", "Description", 5, 10m);
        product.ClearEvents();
        return product;
    }

    [Fact]
    public void AddImage_WithNewKey_ReturnsImageAndRaisesEvent()
    {
        var product = NewProduct();

        var result = product.AddImage("products/a.png", "image/png", 128, MaxImages);

        Assert.False(result.IsFailure);
        var image = result.Value!;
        Assert.Equal(product.Id, image.ProductId);
        Assert.Equal("products/a.png", image.ObjectKey);
        Assert.Equal("image/png", image.ContentType);
        Assert.Equal(128, image.SizeBytes);
        Assert.Same(image, Assert.Single(product.Images));
        var @event = Assert.IsType<V1ProductImageAddedDomainEvent>(Assert.Single(product.Events));
        Assert.Equal(product.Id, @event.ProductId);
        Assert.Equal(image.Id, @event.ImageId);
        Assert.Equal("products/a.png", @event.ObjectKey);
    }

    [Fact]
    public void AddImage_SameKeyTwice_FailsWithImageAlreadyAdded()
    {
        var product = NewProduct();
        product.AddImage("products/a.png", "image/png", 128, MaxImages);
        product.ClearEvents();

        var result = product.AddImage("products/a.png", "image/png", 128, MaxImages);

        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.ImageAlreadyAdded.Key, result.Error!.Key);
        Assert.Single(product.Images);
        Assert.Empty(product.Events);
    }

    [Fact]
    public void AddImage_BeyondTheLimit_FailsWithImageLimitReached()
    {
        var product = NewProduct();
        product.AddImage("products/a.png", "image/png", 1, MaxImages);
        product.AddImage("products/b.png", "image/png", 1, MaxImages);
        product.ClearEvents();

        var result = product.AddImage("products/c.png", "image/png", 1, MaxImages);

        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.ImageLimitReached.Key, result.Error!.Key);
        Assert.Equal(2, product.Images.Count);
        Assert.Empty(product.Events);
    }

    [Fact]
    public void RemoveImage_ExistingImage_RemovesItAndRaisesEvent()
    {
        var product = NewProduct();
        var image = product.AddImage("products/a.png", "image/png", 1, MaxImages).Value!;
        product.ClearEvents();

        product.RemoveImage(image);

        Assert.Empty(product.Images);
        var @event = Assert.IsType<V1ProductImageRemovedDomainEvent>(Assert.Single(product.Events));
        Assert.Equal(image.Id, @event.ImageId);
        Assert.Equal("products/a.png", @event.ObjectKey);
    }
}
