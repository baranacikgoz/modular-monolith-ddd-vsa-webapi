using System.Net;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Domain.Stores.Errors;
using Xunit;

namespace Products.Tests.Stores;

/// <summary>Only an Active store changes: a pending application, a suspended store and a closed store refuse every edit.</summary>
public class StoreStatusGuardTests
{
    private readonly ApplicationUserId _ownerId = new(DefaultIdType.CreateVersion7());

    public static TheoryData<StoreStatus, string> NonActiveStatuses => new()
    {
        { StoreStatus.PendingApproval, nameof(StoreErrors.StoreIsPendingApproval) },
        { StoreStatus.Suspended, nameof(StoreErrors.StoreIsSuspended) },
        { StoreStatus.Inactive, nameof(StoreErrors.StoreIsInactive) }
    };

    /// <summary>The store in <paramref name="status" />, reached through the aggregate's own transitions.</summary>
    internal static Store StoreIn(StoreStatus status, ApplicationUserId ownerId)
    {
        var store = Store.Apply(ownerId, "Sample Store", "Description", "Address");

        if (status != StoreStatus.PendingApproval)
        {
            store.Approve();
        }

        if (status == StoreStatus.Suspended)
        {
            store.Suspend();
        }

        if (status == StoreStatus.Inactive)
        {
            store.Deactivate();
        }

        store.ClearEvents();
        return store;
    }

    private static Product NewProduct(Store store) =>Product.Create(store.Id, ProductTemplateId.New(), "P", "D", 1, 10m);

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void AddProduct_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);

        var result = store.AddProduct(NewProduct(store));

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Equal(HttpStatusCode.Conflict, result.Error.StatusCode);
        Assert.Empty(store.Products);
        Assert.Empty(store.Events);
    }

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void Update_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);

        var result = store.Update("Other name", "Other description", "Other address");

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Equal("Sample Store", store.Name);
        Assert.Equal("Description", store.Description);
        Assert.Equal("Address", store.Address);
        Assert.Empty(store.Events);
    }

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void RemoveProduct_StoreNotActive_FailsWithTheStatusErrorAndRaisesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);

        var result = store.RemoveProduct(NewProduct(store));

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Empty(store.Events);
    }

    [Fact]
    public void RemoveProduct_SuspendedStoreHoldingTheProduct_KeepsTheProduct()
    {
        var store = Store.Create(_ownerId, "Sample Store", "Description", "Address");
        var product = NewProduct(store);
        store.AddProduct(product);
        store.Suspend();
        store.ClearEvents();

        var result = store.RemoveProduct(product);

        Assert.True(result.IsFailure);
        Assert.Contains(product, store.Products);
        Assert.Empty(store.Events);
    }

    private static Product OwnedProduct(Store store)
    {
        var product = NewProduct(store);
        store.AddProduct(product);
        store.ClearEvents();
        product.ClearEvents();
        return product;
    }

    private static PackageDimensions Dimensions() => PackageDimensions.Create(10m, 5m, 4m, 1.5m).Value!;

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void UpdateProduct_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);
        var product = NewProduct(store);

        var result = store.UpdateProduct(product, "Renamed", "Other", 9, 99m);

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Equal(HttpStatusCode.Conflict, result.Error.StatusCode);
        Assert.Equal(("P", "D", 1, 10m), (product.Name, product.Description, product.Quantity, product.Price));
        Assert.Empty(product.Events.Skip(1));
    }

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void SetProductAttributes_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);
        var product = NewProduct(store);

        var result = store.SetProductAttributes(product, [new ProductAttribute("Color", "Red")], 10);

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Empty(product.Attributes);
        Assert.Empty(product.Events.Skip(1));
    }

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void SetProductPackageDimensions_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);
        var product = NewProduct(store);

        var result = store.SetProductPackageDimensions(product, Dimensions());

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Null(product.PackageDimensions);
        Assert.Empty(product.Events.Skip(1));
    }

    [Theory]
    [MemberData(nameof(NonActiveStatuses))]
    public void AddProductImage_StoreNotActive_FailsWithTheStatusErrorAndChangesNothing(StoreStatus status, string expectedKey)
    {
        var store = StoreIn(status, _ownerId);
        var product = NewProduct(store);

        var result = store.AddProductImage(product, "products/p/images/a.png", "image/png", 10, 8);

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Empty(product.Images);
        Assert.Empty(product.Events.Skip(1));
    }

    /// <summary>A pending application holds no products, so only the statuses an Active store can move to are reachable with an image.</summary>
    [Theory]
    [InlineData(StoreStatus.Suspended, nameof(StoreErrors.StoreIsSuspended))]
    [InlineData(StoreStatus.Inactive, nameof(StoreErrors.StoreIsInactive))]
    public void RemoveProductImage_StoreNotActive_FailsWithTheStatusErrorAndKeepsTheImage(StoreStatus status, string expectedKey)
    {
        var store = Store.Create(_ownerId, "Sample Store", "Description", "Address");
        var product = OwnedProduct(store);
        var image = store.AddProductImage(product, "products/p/images/a.png", "image/png", 10, 8).Value!;
        if (status == StoreStatus.Suspended)
        {
            store.Suspend();
        }
        else
        {
            store.Deactivate();
        }

        product.ClearEvents();

        var result = store.RemoveProductImage(product, image);

        Assert.True(result.IsFailure);
        Assert.Equal(expectedKey, result.Error!.Key);
        Assert.Contains(image, product.Images);
        Assert.Empty(product.Events);
    }

    [Fact]
    public void ProductWrites_ActiveStore_ReachTheProduct()
    {
        var store = Store.Create(_ownerId, "Sample Store", "Description", "Address");
        var product = OwnedProduct(store);

        var updated = store.UpdateProduct(product, "Renamed", null, 9, null);
        var attributes = store.SetProductAttributes(product, [new ProductAttribute("Color", "Red")], 10);
        var dimensions = store.SetProductPackageDimensions(product, Dimensions());
        var image = store.AddProductImage(product, "products/p/images/a.png", "image/png", 10, 8);
        var removed = store.RemoveProductImage(product, image.Value!);

        Assert.False(updated.IsFailure);
        Assert.False(attributes.IsFailure);
        Assert.False(dimensions.IsFailure);
        Assert.False(image.IsFailure);
        Assert.False(removed.IsFailure);
        Assert.Equal(("Renamed", 9), (product.Name, product.Quantity));
        Assert.Single(product.Attributes);
        Assert.NotNull(product.PackageDimensions);
        Assert.Empty(product.Images);
    }

    [Fact]
    public void SetProductAttributes_ActiveStoreAndTooManyAttributes_ReturnsTheProductsOwnError()
    {
        var store = Store.Create(_ownerId, "Sample Store", "Description", "Address");
        var product = OwnedProduct(store);

        var result = store.SetProductAttributes(product, [new ProductAttribute("A", "1"), new ProductAttribute("B", "2")], 1);

        Assert.True(result.IsFailure);
        Assert.Equal(nameof(ProductErrors.TooManyAttributes), result.Error!.Key);
    }

    [Fact]
    public void AddProduct_ReinstatedStore_Succeeds()
    {
        var store = StoreIn(StoreStatus.Suspended, _ownerId);
        store.Reinstate();

        var result = store.AddProduct(NewProduct(store));

        Assert.False(result.IsFailure);
        Assert.Single(store.Products);
    }
}
