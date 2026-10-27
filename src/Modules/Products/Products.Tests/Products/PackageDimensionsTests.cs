using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Products;

public class PackageDimensionsTests
{
    private static PackageDimensions Valid(decimal length = 30m, decimal width = 20m, decimal height = 10m, decimal weight = 1.5m)
    {
        return PackageDimensions.Create(length, width, height, weight).Value!;
    }

    [Fact]
    public void Create_WithPositiveValues_KeepsThem()
    {
        var result = PackageDimensions.Create(30m, 20m, 10m, 1.5m);

        Assert.False(result.IsFailure);
        Assert.Equal(30m, result.Value!.LengthCm);
        Assert.Equal(20m, result.Value.WidthCm);
        Assert.Equal(10m, result.Value.HeightCm);
        Assert.Equal(1.5m, result.Value.WeightKg);
    }

    [Theory]
    [InlineData(0, 20, 10, 1.5)]
    [InlineData(30, -1, 10, 1.5)]
    [InlineData(30, 20, 0, 1.5)]
    [InlineData(30, 20, 10, 0)]
    [InlineData(1000.01, 20, 10, 1.5)]
    [InlineData(30, 20, 10, 1000.01)]
    public void Create_WithAValueOutsideTheLimits_ReturnsInvalidPackageDimensions(double length, double width, double height, double weight)
    {
        var result = PackageDimensions.Create((decimal)length, (decimal)width, (decimal)height, (decimal)weight);

        Assert.Equal(ProductErrors.InvalidPackageDimensions, result.Error);
    }

    [Fact]
    public void Create_RoundsToTheStoredPrecision()
    {
        var dimensions = PackageDimensions.Create(30.126m, 20m, 10m, 1.23456m).Value!;

        Assert.Equal(30.13m, dimensions.LengthCm);
        Assert.Equal(1.235m, dimensions.WeightKg);
    }

    [Fact]
    public void Equals_ComparesByValueNotByReference()
    {
        Assert.Equal(Valid(), Valid());
        Assert.True(Valid() == Valid());
        Assert.NotEqual(Valid(), Valid(weight: 2m));
        Assert.Equal(Valid().GetHashCode(), Valid().GetHashCode());
    }

    [Fact]
    public void CompareTo_OrdersByVolumeThenWeight()
    {
        Assert.True(Valid(length: 10m) < Valid(length: 40m));
        Assert.True(Valid(weight: 1m) < Valid(weight: 2m));
        Assert.Equal(0, Valid().CompareTo(Valid()));
    }

    [Fact]
    public void SetPackageDimensions_WithNewValues_StoresThemAndRaisesOneEventWithASnapshot()
    {
        var product = NewProduct();

        product.SetPackageDimensions(Valid());

        Assert.Equal(Valid(), product.PackageDimensions);
        var @event = Assert.IsType<V1ProductPackageDimensionsChangedDomainEvent>(Assert.Single(product.Events));
        Assert.Equal(product.Id, @event.ProductId);
        Assert.Equal(new V1ProductPackageDimensionsChangedDomainEvent.DimensionsSnapshot(30m, 20m, 10m, 1.5m), @event.Dimensions);
    }

    [Fact]
    public void SetPackageDimensions_WithTheSameValues_RaisesNothing()
    {
        var product = NewProduct();
        product.SetPackageDimensions(Valid());
        product.ClearEvents();

        product.SetPackageDimensions(Valid());

        Assert.Empty(product.Events);
    }

    private static Product NewProduct()
    {
        var product = Product.Create(StoreId.New(), ProductTemplateId.New(), "Sample", "Description", 5, 10m);
        product.ClearEvents();
        return product;
    }
}
