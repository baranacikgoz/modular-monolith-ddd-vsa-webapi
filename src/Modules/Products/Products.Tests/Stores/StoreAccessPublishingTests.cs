using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Stores;

/// <summary>
///     Every store event that opens or closes the store for its owner queues a <c>StoreAccessChangedIntegrationEvent</c> carrying
///     the version of the domain event that caused it, so a consumer can drop a late one.
/// </summary>
public class StoreAccessPublishingTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly ApplicationUserId Owner = new(DefaultIdType.CreateVersion7());

    private async Task<Store> SaveAsync(Store store)
    {
        Db.Stores.Add(store);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return store;
    }

    /// <summary>Loads the store the way an endpoint does (tracked, with its products) and runs <paramref name="change" /> on it in one save.</summary>
    private async Task ChangeAsync(StoreId id, Action<Store> change)
    {
        var store = await Db.Stores.Include(s => s.Products).SingleAsync(s => s.Id == id);
        change(store);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
    }

    private async Task<List<StoreAccessChangedIntegrationEvent>> AccessChangesAsync(StoreId id)
    {
        return (await OutboxEventsAsync())
            .OfType<StoreAccessChangedIntegrationEvent>()
            .Where(e => e.StoreId == id.Value)
            .OrderBy(e => e.Version)
            .ToList();
    }

    [Fact]
    public async Task CreatingAStore_QueuesAnActiveAccessChangeWithTheCreationVersion()
    {
        var store = await SaveAsync(Store.Create(Owner, "Store", "Description", "Address"));

        var change = Assert.Single(await AccessChangesAsync(store.Id));

        Assert.True(change.IsActive);
        Assert.Equal(Owner, change.OwnerId);
        Assert.Equal(1, change.Version);
    }

    [Fact]
    public async Task SubmittingAnApplication_QueuesNoAccessChangeBecauseThePendingStoreIsNotActive()
    {
        var store = await SaveAsync(Store.Apply(Owner, "Store", "Description", "Address"));

        Assert.Empty(await AccessChangesAsync(store.Id));
    }

    [Fact]
    public async Task ApprovingAnApplication_QueuesAnActiveAccessChangeWithTheApprovalVersion()
    {
        var store = await SaveAsync(Store.Apply(Owner, "Store", "Description", "Address"));

        await ChangeAsync(store.Id, s => s.Approve());

        var change = Assert.Single(await AccessChangesAsync(store.Id));
        Assert.True(change.IsActive);
        Assert.Equal(Owner, change.OwnerId);
        Assert.Equal(2, change.Version);
    }

    [Fact]
    public async Task SuspendingAndReinstatingAStore_QueueAnInactiveThenAnActiveAccessChangeEachWithItsOwnVersion()
    {
        var store = await SaveAsync(Store.Create(Owner, "Store", "Description", "Address"));

        await ChangeAsync(store.Id, s => s.Suspend());
        await ChangeAsync(store.Id, s => s.Reinstate());

        var changes = await AccessChangesAsync(store.Id);
        Assert.Equal([1L, 2L, 3L], changes.Select(c => c.Version));
        Assert.Equal([true, false, true], changes.Select(c => c.IsActive));
        Assert.All(changes, c => Assert.Equal(Owner, c.OwnerId));
    }

    [Fact]
    public async Task DeactivatingAStoreWithoutStock_QueuesAnInactiveAccessChange()
    {
        var store = await SaveAsync(Store.Create(Owner, "Store", "Description", "Address"));

        await ChangeAsync(store.Id, s => s.Deactivate());

        var change = (await AccessChangesAsync(store.Id))[^1];
        Assert.False(change.IsActive);
        Assert.Equal(Owner, change.OwnerId);
        Assert.Equal(2, change.Version);
    }

    [Fact]
    public async Task DeactivatingAStoreThatStillHoldsStock_QueuesAnInactiveAccessChangeToo()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");
        Db.ChangeTracker.Clear();

        await ChangeAsync(seeded.Store.Id, s => s.Deactivate());

        var change = (await AccessChangesAsync(seeded.Store.Id))[^1];
        Assert.False(change.IsActive);
        Assert.Equal(Owner, change.OwnerId);
    }

    [Fact]
    public async Task RenamingAStore_QueuesNoAccessChangeButTheNextOneCarriesTheLaterVersion()
    {
        var store = await SaveAsync(Store.Create(Owner, "Store", "Description", "Address"));

        await ChangeAsync(store.Id, s => s.Update("Renamed", null, null));
        await ChangeAsync(store.Id, s => s.Suspend());

        var changes = await AccessChangesAsync(store.Id);
        Assert.Equal([1L, 3L], changes.Select(c => c.Version));
        Assert.False(changes[1].IsActive);
    }
}
