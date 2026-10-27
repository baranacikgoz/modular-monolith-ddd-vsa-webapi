using System.Linq.Expressions;
using System.Net;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using EventBus = Common.Application.EventBus;

namespace Inventory.Tests.StockPushes;

/// <summary>The sweep is the safety net under the event-driven push: it only publishes, it never calls a warehouse.</summary>
[Collection("IntegrationTestCollection")]
public class StockPushSweepServiceTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    private async Task SweepAsync(Action<InventoryOptions>? configure = null)
    {
        using var scope = Root.CreateScope();
        var services = scope.ServiceProvider;
        var sweep = new StockPushSweepService(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<EventBus.IIntegrationEventOutbox>(),
            TimeProvider.System,
            TestInventoryOptions.Wrapped(configure),
            NullLogger<StockPushSweepService>.Instance);

        await sweep.SweepAsync();
    }

    /// <summary>A store with one marked stock level whose mark was made <paramref name="age" /> ago.</summary>
    private async Task<SeededStore> SeedMarkedStoreAsync(TimeSpan age, bool withConnection = true)
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, withConnection: withConnection);
        await StockPushTestData.MarkAsync(Root, store.StockLevels.Single(), store.StoreId, DateTimeOffset.UtcNow - age);
        return store;
    }

    private async Task<List<StockPushDueIntegrationEvent>> DueEventsAsync()
    {
        return await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root);
    }

    [Fact]
    public async Task Sweep_MarksOlderThanTheAge_QueuesOneDueEventForTheirConnectionAndPushesNothing()
    {
        var old = await SeedMarkedStoreAsync(TimeSpan.FromMinutes(10));

        await SweepAsync();

        var due = Assert.Single(await DueEventsAsync());
        Assert.Equal(old.Connection!.Id.Value, due.ConnectionId);
        Assert.Empty(factory.Warehouse.Requests);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Root));
    }

    [Fact]
    public async Task Sweep_ManyOldMarksOfOneStore_QueueOneDueEvent()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, stockLevels: 3);
        foreach (var stockLevel in store.StockLevels)
        {
            await StockPushTestData.MarkAsync(Root, stockLevel, store.StoreId, DateTimeOffset.UtcNow.AddMinutes(-10));
        }

        await SweepAsync();

        Assert.Single(await DueEventsAsync());
    }

    [Fact]
    public async Task Sweep_MarkYoungerThanTheAge_IsLeftToThePushItsOwnChangeQueued()
    {
        await SeedMarkedStoreAsync(TimeSpan.FromSeconds(5));

        await SweepAsync();

        Assert.Empty(await DueEventsAsync());
    }

    [Fact]
    public async Task Sweep_OldMarkOfAStoreWithoutAnActiveConnection_QueuesNothing()
    {
        await SeedMarkedStoreAsync(TimeSpan.FromMinutes(10), withConnection: false);

        await SweepAsync();

        Assert.Empty(await DueEventsAsync());
    }

    [Fact]
    public async Task Sweep_MoreConnectionsThanOnePage_QueuesEveryConnectionOnce()
    {
        var stores = new List<SeededStore>();
        for (var i = 0; i < 3; i++)
        {
            stores.Add(await SeedMarkedStoreAsync(TimeSpan.FromMinutes(10)));
        }

        await SweepAsync(o => o.SweepPageSize = 2);

        Assert.Equal(
            stores.Select(s => s.Connection!.Id.Value).Order(),
            (await DueEventsAsync()).Select(e => e.ConnectionId).Order());
    }

    [Fact]
    public async Task StartAsync_RegistersTheSweepOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var services = new ServiceCollection().AddSingleton(recurringJobs).BuildServiceProvider();
        var registrar = new StockPushSweepJobRegistrar(
            services,
            Options.Create(TestInventoryOptions.Create(configure: o => o.StockPushDirtySweepCron = "30 */2 * * * *")),
            NullLogger<StockPushSweepJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "inventory-stock-push-sweep",
            Arg.Any<Expression<Func<StockPushSweepService, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "30 */2 * * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var registrar = new StockPushSweepJobRegistrar(
            new ServiceCollection().BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create()),
            NullLogger<StockPushSweepJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);
    }
}
