using System.Net;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
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
