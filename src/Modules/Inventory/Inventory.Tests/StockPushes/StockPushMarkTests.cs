using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.StockPushes;

/// <summary>
///     A quantity change marks its stock level dirty in the same transaction as the change, and queues one due event per
///     connection however many stock levels the save touched.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockPushMarkTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    [Fact]
    public async Task QuantityChange_StoreWithAnActiveConnection_MarksTheStockLevelAndQueuesOneDueEvent()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root);
        var stockLevel = store.StockLevels.Single();

        await StockPushTestData.ReceiveAsync(Root, stockLevel.Id, 5);

        var mark = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Equal(stockLevel.Id, mark.StockLevelId);
        Assert.Equal(store.StoreId, mark.StoreId);
        Assert.Equal(stockLevel.ProductId, mark.ProductId);
        Assert.True(mark.MarkVersion > 0);
        Assert.NotEqual(default, mark.MarkedOn);

        var due = Assert.Single(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
        Assert.Equal(store.Connection!.Id.Value, due.ConnectionId);
    }

    [Fact]
    public async Task QuantityChange_StoreWithoutAConnection_MarksNothingAndQueuesNothing()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, withConnection: false);

        await StockPushTestData.ReceiveAsync(Root, store.StockLevels.Single().Id, 5);

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Empty(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
    }

    [Fact]
    public async Task QuantityChange_StoreWithADeactivatedConnection_MarksNothingAndQueuesNothing()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, deactivated: true);

        await StockPushTestData.ReceiveAsync(Root, store.StockLevels.Single().Id, 5);

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Empty(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
    }

    [Fact]
    public async Task QuantityChange_AgainBeforeThePush_KeepsOneRowWithANewerVersionAndTheFirstMarkTime()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root);
        var id = store.StockLevels.Single().Id;
        await StockPushTestData.ReceiveAsync(Root, id, 5);
        var first = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));

        await StockPushTestData.ReceiveAsync(Root, id, 3);

        var second = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.True(second.MarkVersion > first.MarkVersion);
        Assert.Equal(first.MarkedOn, second.MarkedOn, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task QuantityChange_ManyStockLevelsOfOneStoreInOneSave_QueuesOneDueEventForTheConnection()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, stockLevels: 5);
        var ids = store.StockLevels.Select(s => s.Id).ToList();

        await using (var scope = Root.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            foreach (var stockLevel in await db.StockLevels.Where(s => ids.Contains(s.Id)).ToListAsync())
            {
                stockLevel.Receive(1, "Delivery");
            }

            await db.SaveChangesAsync();
        }

        Assert.Equal(5, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
        var due = Assert.Single(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
        Assert.Equal(store.Connection!.Id.Value, due.ConnectionId);
    }

    [Fact]
    public async Task QuantityChange_StockLevelsOfTwoStoresInOneSave_QueuesOneDueEventPerConnection()
    {
        var first = await StockPushTestData.SeedStoreAsync(Root);
        var second = await StockPushTestData.SeedStoreAsync(Root);
        var ids = first.StockLevels.Concat(second.StockLevels).Select(s => s.Id).ToList();

        await using (var scope = Root.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            foreach (var stockLevel in await db.StockLevels.Where(s => ids.Contains(s.Id)).ToListAsync())
            {
                stockLevel.Receive(1, "Delivery");
            }

            await db.SaveChangesAsync();
        }

        var due = await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root);
        Assert.Equal(
            new[] { first.Connection!.Id.Value, second.Connection!.Id.Value }.Order(),
            due.Select(e => e.ConnectionId).Order());
    }

    [Fact]
    public async Task QuantityChange_SaveThatFails_LeavesNoMarkBehind()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root);
        var stockLevel = store.StockLevels.Single();

        await using (var scope = Root.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
            tracked.Receive(5, "Delivery");

            // A second stock level for the same product violates the unique index after the mark was written.
            db.StockLevels.Add(StockLevel.Create(stockLevel.ProductId, 1));

            await Assert.ThrowsAnyAsync<Exception>(() => db.SaveChangesAsync());
        }

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Empty(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
        Assert.Equal(10, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
    }
}
