using System.Net;
using System.Text.Json;
using Common.Application.Options;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using EventBus = Common.Application.EventBus;

namespace Inventory.Tests.StockPushes;

/// <summary>
///     The push runner as the queue consumer calls it, in a DI scope of its own, against the factory's fake warehouse (the
///     real gateway, pipeline and encryption run, only the network is replaced).
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockPushRunnerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    private FakeWarehouseHandler Warehouse => factory.Warehouse;

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        Warehouse.Reset();
        Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch" });
    }

    private async Task RunAsync(WarehouseConnectionId connectionId, Action<InventoryOptions>? configure = null)
    {
        using var scope = Root.CreateScope();
        var services = scope.ServiceProvider;
        var runner = new StockPushRunner(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<IWarehouseGatewayFactory>(),
            services.GetRequiredService<EventBus.IIntegrationEventOutbox>(),
            TestInventoryOptions.Wrapped(configure),
            NullLogger<StockPushRunner>.Instance);

        await runner.RunAsync(connectionId, CancellationToken.None);
    }

    /// <summary>A store whose stock levels were all changed once, so each carries a mark.</summary>
    private async Task<SeededStore> SeedDirtyStoreAsync(int stockLevels = 1)
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, stockLevels);
        foreach (var stockLevel in store.StockLevels)
        {
            await StockPushTestData.ReceiveAsync(Root, stockLevel.Id, 5);
        }

        return store;
    }

    private static List<(string ProductReference, int Quantity)> SentItems(RecordedWarehouseRequest request)
    {
        using var body = JsonDocument.Parse(request.Body);
        return body.RootElement.GetProperty("items").EnumerateArray()
            .Select(i => (i.GetProperty("productReference").GetString()!, i.GetProperty("quantity").GetInt32()))
            .ToList();
    }

    private async Task<HashSet<DefaultIdType>> QueuedDueEventIdsAsync()
    {
        return (await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root)).Select(e => e.Id).ToHashSet();
    }

    [Fact]
    public async Task Run_DirtyStockLevels_PushesTheirCurrentQuantitiesInOneCallAndClearsTheMarks()
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 3);

        await RunAsync(store.Connection!.Id);

        var request = Assert.Single(Warehouse.Requests);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/stock/levels"), request.Uri);
        Assert.Equal(
            store.StockLevels.Select(s => (s.ProductId.ToString(), 15)).Order(),
            SentItems(request).Order());
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_NothingDirty_SendsNothing()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root);

        await RunAsync(store.Connection!.Id);

        Assert.Empty(Warehouse.Requests);
    }

    [Fact]
    public async Task Run_OnlyTheMarksOfTheConnectionsStoreAreSent()
    {
        var store = await SeedDirtyStoreAsync();
        var otherStore = await SeedDirtyStoreAsync();

        await RunAsync(store.Connection!.Id);

        var request = Assert.Single(Warehouse.Requests);
        Assert.Equal([(store.StockLevels.Single().ProductId.ToString(), 15)], SentItems(request));
        var left = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.Equal(otherStore.StoreId, left.StoreId);
    }

    [Fact]
    public async Task Run_MoreMarksThanOneBatch_SendsOneCallPerBatch()
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 5);

        await RunAsync(store.Connection!.Id, o => o.StockPushBatchSize = 2);

        Assert.Equal([2, 2, 1], Warehouse.Requests.Select(r => SentItems(r).Count));
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_MoreMarksThanOneMessageTakes_StopsAtTheCapAndQueuesAContinuationForItself()
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 3);
        var before = await QueuedDueEventIdsAsync();

        await RunAsync(store.Connection!.Id, o =>
        {
            o.StockPushBatchSize = 1;
            o.StockPushBatchesPerMessage = 2;
        });

        Assert.Equal(2, Warehouse.Requests.Count);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        var continuation = Assert.Single(
            await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root), e => !before.Contains(e.Id));
        Assert.Equal(store.Connection.Id.Value, continuation.ConnectionId);
    }

    [Fact]
    public async Task Run_NothingLeftAfterTheLastBatch_QueuesNoContinuation()
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 2);
        var before = await QueuedDueEventIdsAsync();

        await RunAsync(store.Connection!.Id);

        Assert.All(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root), e => Assert.Contains(e.Id, before));
    }

    [Fact]
    public async Task Run_StockChangesWhileTheBatchIsInFlight_TheNewMarkSurvivesAndTheNextRunPushesTheNewQuantity()
    {
        var store = await SeedDirtyStoreAsync();
        var stockLevel = store.StockLevels.Single();
        var markBefore = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        var before = await QueuedDueEventIdsAsync();
        Warehouse.Responder = _ =>
        {
            // Another request receives stock while the warehouse is still answering.
            Task.Run(() => StockPushTestData.ReceiveAsync(Root, stockLevel.Id, 100)).GetAwaiter().GetResult();
            return FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch" });
        };

        await RunAsync(store.Connection!.Id);

        Assert.Equal([(stockLevel.ProductId.ToString(), 15)], SentItems(Assert.Single(Warehouse.Requests)));
        var survivor = Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
        Assert.True(survivor.MarkVersion > markBefore.MarkVersion);
        // One due event is the mid-push change's own, the other is the run asking for another pass over what it left.
        var queuedSince = (await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root)).Where(e => !before.Contains(e.Id));
        Assert.Equal(2, queuedSince.Count());

        Warehouse.Reset();
        Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch-2" });
        await RunAsync(store.Connection.Id);

        Assert.Equal([(stockLevel.ProductId.ToString(), 115)], SentItems(Assert.Single(Warehouse.Requests)));
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_AnotherRunHoldsTheLeaseOfTheConnection_DoesNothing()
    {
        var store = await SeedDirtyStoreAsync();
        await using var holder = Root.CreateAsyncScope();
        var holderDb = holder.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        await holderDb.Database.OpenConnectionAsync();
        Assert.True(await StockPushLease.TryAcquireAsync(holderDb, store.Connection!.Id, CancellationToken.None));

        await RunAsync(store.Connection.Id);

        Assert.Empty(Warehouse.Requests);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));

        await StockPushLease.ReleaseAsync(holderDb, store.Connection.Id);
        await holderDb.Database.CloseConnectionAsync();
        await RunAsync(store.Connection.Id);

        Assert.Single(Warehouse.Requests);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_LeaseOfAnotherConnection_DoesNotBlockThisOne()
    {
        var store = await SeedDirtyStoreAsync();
        await using var holder = Root.CreateAsyncScope();
        var holderDb = holder.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        await holderDb.Database.OpenConnectionAsync();
        Assert.True(await StockPushLease.TryAcquireAsync(holderDb, WarehouseConnectionId.New(), CancellationToken.None));

        await RunAsync(store.Connection!.Id);

        Assert.Single(Warehouse.Requests);
        await holderDb.Database.CloseConnectionAsync();
    }

    [Fact]
    public async Task Run_ManyRunsOfOneConnectionAtOnce_SendTheMarksOnce()
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 3);
        var start = new TaskCompletionSource();

        var runs = Enumerable.Range(0, 8).Select(_ => Task.Run(async () =>
        {
            await start.Task;
            await RunAsync(store.Connection!.Id);
        })).ToList();
        start.SetResult();
        await Task.WhenAll(runs);

        Assert.Single(Warehouse.Requests);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task Run_WarehouseAnswersWithAFailure_KeepsTheMarksAndQueuesNothingAndSendsOnce(HttpStatusCode status)
    {
        var store = await SeedDirtyStoreAsync(stockLevels: 2);
        var before = await QueuedDueEventIdsAsync();
        Warehouse.Responder = _ => new HttpResponseMessage(status);

        await RunAsync(store.Connection!.Id, o => o.StockPushBatchSize = 1);

        Assert.Single(Warehouse.Requests);
        Assert.Equal(2, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
        Assert.All(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root), e => Assert.Contains(e.Id, before));
    }

    [Fact]
    public async Task Run_WarehouseDoesNotAnswer_KeepsTheMarksSoTheCurrentQuantitiesGoOutAgain()
    {
        var store = await SeedDirtyStoreAsync();
        Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        await RunAsync(store.Connection!.Id);

        Assert.Single(Warehouse.Requests);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));

        Warehouse.Reset();
        Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch" });
        await RunAsync(store.Connection.Id);

        Assert.Single(Warehouse.Requests);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_ConnectionWasDeactivated_PushesNothing()
    {
        var store = await SeedDirtyStoreAsync();
        await using (var scope = Root.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var connection = await db.WarehouseConnections.SingleAsync(c => c.Id == store.Connection!.Id);
            connection.Deactivate();
            await db.SaveChangesAsync();
        }

        await RunAsync(store.Connection!.Id);

        Assert.Empty(Warehouse.Requests);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Run_StoreOnTheOtherProvider_SendsThatProvidersWire()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, provider: WarehouseProvider.FormApi);
        await StockPushTestData.ReceiveAsync(Root, store.StockLevels.Single().Id, 5);
        Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "form-batch" } });

        await RunAsync(store.Connection!.Id);

        var request = Assert.Single(Warehouse.Requests);
        Assert.Equal("application/x-www-form-urlencoded", request.ContentType);
        Assert.Equal(WarehouseConnectionTestData.ApiCredential, request.ApiKey);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Root));
    }
}
