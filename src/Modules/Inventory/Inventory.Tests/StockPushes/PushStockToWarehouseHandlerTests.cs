using System.Net;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.StockPushes;

[Collection("IntegrationTestCollection")]
public class PushStockToWarehouseHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch" });
    }

    [Fact]
    public async Task Consume_DueEvent_PushesTheStoresMarksOnceEvenWhenDeliveredAgain()
    {
        var store = await StockPushTestData.SeedStoreAsync(Factory.Services);
        await StockPushTestData.ReceiveAsync(Factory.Services, store.StockLevels.Single().Id, 5);
        var @event = new StockPushDueIntegrationEvent(store.Connection!.Id.Value);

        await ConsumerHarness.DeliverAsync<PushStockToWarehouseHandler, StockPushDueIntegrationEvent>(Factory.Services, @event);
        await ConsumerHarness.DeliverAsync<PushStockToWarehouseHandler, StockPushDueIntegrationEvent>(Factory.Services, @event);

        Assert.Single(factory.Warehouse.Requests);
        Assert.Empty(await StockPushTestData.DirtyItemsAsync(Factory.Services));
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        Assert.True(await db.Set<ProcessedMessage>().AsNoTracking()
            .AnyAsync(m => m.ConsumerName == nameof(PushStockToWarehouseHandler) && m.MessageId == @event.Id));
    }

    [Fact]
    public void Definition_PrefetchCountAndConcurrentMessageLimitAreTheOptionsValue()
    {
        var definition = new PushStockToWarehouseHandlerDefinition(TestInventoryOptions.Wrapped(o => o.StockPushConcurrentMessageLimit = 3));
        var endpoint = Substitute.For<IReceiveEndpointConfigurator>();

        ((IConsumerDefinition<PushStockToWarehouseHandler>)definition).Configure(
            endpoint, Substitute.For<IConsumerConfigurator<PushStockToWarehouseHandler>>(), Substitute.For<IRegistrationContext>());

        Assert.Equal(3, endpoint.PrefetchCount);
        Assert.Equal(3, endpoint.ConcurrentMessageLimit);
    }

    /// <summary>The consumer is picked up by the Host's assembly scan; this is the check that a rename or a moved namespace does not silently drop its queue.</summary>
    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(PushStockToWarehouseHandler)}", StringComparison.Ordinal));
    }
}
