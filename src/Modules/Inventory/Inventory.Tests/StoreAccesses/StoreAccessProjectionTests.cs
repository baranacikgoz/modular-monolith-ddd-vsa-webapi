using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.IntegrationEventHandlers;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.StoreAccesses;

/// <summary>
///     Products' <c>StoreAccessChangedIntegrationEvent</c> feeds Inventory's local store access copy, driven through the
///     consumer's own <c>Consume</c> against the real Inventory schema.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreAccessProjectionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly ApplicationUserId Owner = new(DefaultIdType.CreateVersion7());

    private Task DeliverAsync(StoreAccessChangedIntegrationEvent @event)
    {
        return ConsumerHarness.DeliverAsync<UpdateStoreAccessOnStoreAccessChangedHandler, StoreAccessChangedIntegrationEvent>(
            Factory.Services, @event);
    }

    private async Task<StoreAccess> RowAsync(DefaultIdType storeId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StoreAccess.AsNoTracking().SingleAsync(a => a.StoreId == storeId);
    }

    [Fact]
    public async Task Consume_UnknownStore_CreatesTheRow()
    {
        var storeId = DefaultIdType.CreateVersion7();

        await DeliverAsync(new StoreAccessChangedIntegrationEvent(storeId, Owner, true, 1));

        var row = await RowAsync(storeId);
        Assert.Equal(Owner, row.OwnerId);
        Assert.True(row.IsActive);
        Assert.Equal(1, row.SourceVersion);
    }

    [Fact]
    public async Task Consume_NewerVersion_ClosesTheStore()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await DeliverAsync(new StoreAccessChangedIntegrationEvent(storeId, Owner, true, 1));

        await DeliverAsync(new StoreAccessChangedIntegrationEvent(storeId, Owner, false, 2));

        var row = await RowAsync(storeId);
        Assert.False(row.IsActive);
        Assert.Equal(2, row.SourceVersion);
    }

    /// <summary>Delivery order is never guaranteed: a late reinstatement of version 2 must not reopen a store version 3 closed.</summary>
    [Fact]
    public async Task Consume_StaleVersion_LeavesTheRowUntouched()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await DeliverAsync(new StoreAccessChangedIntegrationEvent(storeId, Owner, false, 3));

        await DeliverAsync(new StoreAccessChangedIntegrationEvent(storeId, Owner, true, 2));

        var row = await RowAsync(storeId);
        Assert.False(row.IsActive);
        Assert.Equal(3, row.SourceVersion);
    }
}
