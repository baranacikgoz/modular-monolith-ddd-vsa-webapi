using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using EventBus = Common.Application.EventBus;

namespace Inventory.Tests.StockPushes;

/// <summary>
///     A warehouse connection that is created for a store (a store connects again by creating a new one, so this covers a
///     reactivation too) knows none of the store's stock yet, so every stock level of the store is marked dirty for a first
///     full push. The connection's save only queues the request; a consumer marks the store in pages, one set-based
///     statement each.
/// </summary>
[Collection("IntegrationTestCollection")]
public class InitialStockPushTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    /// <summary>A store whose stock levels exist before it has a connection.</summary>
    private async Task<SeededStore> SeedStoreWithoutConnectionAsync(int stockLevels)
    {
        return await StockPushTestData.SeedStoreAsync(Root, stockLevels, withConnection: false);
    }

    /// <summary>Delivers the request to a consumer built with its own options, so a test can shrink the page.</summary>
    private async Task DeliverAsync(StockPushInitialMarkRequestedIntegrationEvent @event, Action<InventoryOptions>? configure = null)
    {
        await using var scope = Root.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var handler = new MarkStoreStockForPushHandler(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<EventBus.IIntegrationEventOutbox>(),
            TimeProvider.System,
            TestInventoryOptions.Wrapped(configure),
            services.GetRequiredService<IFusionCache>(),
            services.GetRequiredService<IOptions<CachingOptions>>(),
            services.GetRequiredService<IInboxStore<IInventoryDbContext>>(),
            NullLogger<MarkStoreStockForPushHandler>.Instance);
        var context = Substitute.For<ConsumeContext<StockPushInitialMarkRequestedIntegrationEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);

        await handler.Consume(context);
    }

    [Fact]
    public async Task CreatingAConnection_ForAStoreWithStock_QueuesTheRequestForAFirstFullPush()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 3);

        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        var requested = Assert.Single(await StockLevelTestData.QueuedEventsAsync<StockPushInitialMarkRequestedIntegrationEvent>(Root));
        Assert.Equal(connection.Id.Value, requested.ConnectionId);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Consume_RequestOfAnActiveConnection_MarksEveryStockLevelOfItsStoreAndQueuesOnePush()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 5);
        var other = await StockPushTestData.SeedStoreAsync(Root, stockLevels: 2, withConnection: false);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value));

        var marks = await StockPushTestData.DirtyItemsAsync(Root);
        Assert.Equal(store.StockLevels.Select(s => s.Id.Value).Order(), marks.Select(m => m.StockLevelId.Value).Order());
        Assert.All(marks, mark => Assert.Equal(store.StoreId, mark.StoreId));
        Assert.DoesNotContain(marks, mark => other.StockLevels.Any(s => s.Id == mark.StockLevelId));
        var due = Assert.Single(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
        Assert.Equal(connection.Id.Value, due.ConnectionId);
    }

    [Fact]
    public async Task Consume_StoreLargerThanOnePage_MarksEveryStockLevelPageByPage()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 7);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value), o => o.StockPushInitialMarkPageSize = 3);

        Assert.Equal(7, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
    }

    [Fact]
    public async Task Consume_StoreWhoseStockLevelsFillExactlyAFinalPage_StopsAfterTheEmptyPageWithoutLoopingForever()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 4);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value), o => o.StockPushInitialMarkPageSize = 2);

        Assert.Equal(4, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
    }

    [Fact]
    public async Task Consume_StockLevelThatIsAlreadyMarked_KeepsOneRowWithANewVersionAndTheFirstMarkTime()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 1);
        var stockLevel = store.StockLevels.Single();
        await StockPushTestData.MarkAsync(Root, stockLevel, store.StoreId, DateTimeOffset.UtcNow.AddHours(-1));
        var before = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value));

        var after = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.True(after.MarkVersion > before.MarkVersion);
        Assert.Equal(before.MarkedOn, after.MarkedOn, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Consume_ConnectionDeactivatedBeforeTheRequestRan_MarksNothing()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 2);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId, deactivated: true);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value));

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Empty(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
    }

    [Fact]
    public async Task Consume_UnknownConnection_MarksNothingAndDoesNotThrow()
    {
        await SeedStoreWithoutConnectionAsync(stockLevels: 1);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(DefaultIdType.CreateVersion7()));

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Consume_StoreWithoutStockLevels_QueuesNoPush()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 0);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);

        await DeliverAsync(new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value));

        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Empty(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
    }

    /// <summary>The consumer is picked up by the Host's assembly scan; this is the check that a rename or a moved namespace does not silently drop its queue.</summary>
    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(MarkStoreStockForPushHandler)}", StringComparison.Ordinal));
    }

    [Fact]
    public async Task Consume_RequestDeliveredTwice_LeavesOneMarkPerStockLevel()
    {
        var store = await SeedStoreWithoutConnectionAsync(stockLevels: 3);
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Root, store.StoreId);
        var request = new StockPushInitialMarkRequestedIntegrationEvent(connection.Id.Value);

        await ConsumerHarness.DeliverAsync<MarkStoreStockForPushHandler, StockPushInitialMarkRequestedIntegrationEvent>(Root, request);
        await ConsumerHarness.DeliverAsync<MarkStoreStockForPushHandler, StockPushInitialMarkRequestedIntegrationEvent>(Root, request);

        Assert.Equal(3, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
    }
}
