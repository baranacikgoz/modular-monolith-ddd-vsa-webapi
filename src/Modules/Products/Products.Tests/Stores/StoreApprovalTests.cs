using System.Net;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Products.Domain.Stores.Errors;
using Xunit;

namespace Products.Tests.Stores;

public class StoreApprovalTests
{
    private readonly ApplicationUserId _ownerId = new(DefaultIdType.CreateVersion7());

    private Store PendingStore()
    {
        var store = Store.Apply(_ownerId, "Applied Store", "Description", "Address");
        store.ClearEvents();
        return store;
    }

    private Store ActiveStore()
    {
        var store = PendingStore();
        store.Approve();
        store.ClearEvents();
        return store;
    }

    [Fact]
    public void Apply_WithValidParameters_CreatesPendingStoreAndRaisesApplicationSubmittedEvent()
    {
        var store = Store.Apply(_ownerId, "Applied Store", "Description", "Address");

        Assert.Equal(StoreStatus.PendingApproval, store.Status);
        Assert.Equal(_ownerId, store.OwnerId);
        var @event = Assert.IsType<V1StoreApplicationSubmittedDomainEvent>(Assert.Single(store.Events));
        Assert.Equal(store.Id, @event.StoreId);
        Assert.Equal(_ownerId, @event.OwnerId);
        Assert.Equal("Applied Store", @event.Name);
    }

    [Fact]
    public void Approve_PendingStore_ActivatesItAndRaisesApprovedEvent()
    {
        var store = PendingStore();

        var result = store.Approve();

        Assert.False(result.IsFailure);
        Assert.Equal(StoreStatus.Active, store.Status);
        var @event = Assert.IsType<V1StoreApprovedDomainEvent>(Assert.Single(store.Events));
        Assert.Equal(store.Id, @event.StoreId);
        Assert.Equal(_ownerId, @event.OwnerId);
    }

    [Fact]
    public void Approve_ActiveStore_SucceedsWithoutRaisingAnEvent()
    {
        var store = ActiveStore();

        var result = store.Approve();

        Assert.False(result.IsFailure);
        Assert.Empty(store.Events);
    }

    [Fact]
    public void Approve_InactiveStore_ReturnsNotPendingApproval()
    {
        var store = PendingStore();
        store.Deactivate();
        store.ClearEvents();

        var result = store.Approve();

        Assert.Equal(StoreErrors.NotPendingApproval, result.Error);
        Assert.Equal(HttpStatusCode.Conflict, result.Error!.StatusCode);
        Assert.Equal(StoreStatus.Inactive, store.Status);
        Assert.Empty(store.Events);
    }

    [Fact]
    public void Suspend_ActiveStore_SuspendsItAndRaisesSuspendedEvent()
    {
        var store = ActiveStore();

        var result = store.Suspend();

        Assert.False(result.IsFailure);
        Assert.Equal(StoreStatus.Suspended, store.Status);
        var @event = Assert.IsType<V1StoreSuspendedDomainEvent>(Assert.Single(store.Events));
        Assert.Equal(store.Id, @event.StoreId);
    }

    [Fact]
    public void Suspend_PendingStore_ReturnsNotActive()
    {
        var store = PendingStore();

        var result = store.Suspend();

        Assert.Equal(StoreErrors.NotActive, result.Error);
        Assert.Equal(StoreStatus.PendingApproval, store.Status);
        Assert.Empty(store.Events);
    }

    [Fact]
    public void Reinstate_SuspendedStore_ActivatesItAndRaisesReinstatedEvent()
    {
        var store = ActiveStore();
        store.Suspend();
        store.ClearEvents();

        var result = store.Reinstate();

        Assert.False(result.IsFailure);
        Assert.Equal(StoreStatus.Active, store.Status);
        var @event = Assert.IsType<V1StoreReinstatedDomainEvent>(Assert.Single(store.Events));
        Assert.Equal(store.Id, @event.StoreId);
    }

    [Fact]
    public void Reinstate_PendingStore_ReturnsNotSuspended()
    {
        var store = PendingStore();

        var result = store.Reinstate();

        Assert.Equal(StoreErrors.NotSuspended, result.Error);
        Assert.Equal(StoreStatus.PendingApproval, store.Status);
    }

    [Fact]
    public void Deactivate_SuspendedStore_ClosesItForGoodSoReinstateIsRefused()
    {
        var store = ActiveStore();
        store.Suspend();
        store.ClearEvents();

        var deactivated = store.Deactivate();
        var reinstated = store.Reinstate();

        Assert.False(deactivated.IsFailure);
        Assert.Equal(StoreStatus.Inactive, store.Status);
        Assert.IsType<V1StoreDeactivatedDomainEvent>(Assert.Single(store.Events));
        Assert.Equal(StoreErrors.NotSuspended, reinstated.Error);
    }
}
