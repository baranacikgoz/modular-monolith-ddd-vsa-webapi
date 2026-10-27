using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Application.Storage;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Products.Application.IntegrationEventHandlers;
using Products.Application.Persistence;
using Products.Tests.Support;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Tests.IntegrationEventHandlers;

public class DeleteOrphanedProductImageObjectsHandlerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly string[] Keys = ["products/lamp/images/a.png", "products/lamp/images/b.jpg"];

    private static ProductImageObjectsOrphanedIntegrationEvent NewEvent(params string[] keys)
    {
        return new ProductImageObjectsOrphanedIntegrationEvent(Guid.NewGuid(), keys);
    }

    private async Task StoreAsync(params string[] keys)
    {
        foreach (var key in keys)
        {
            using var content = new MemoryStream([1, 2, 3]);
            await PublicStore.UploadAsync(new UploadObjectRequest(key, content, "image/png"), CancellationToken.None);
        }
    }

    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(DeleteOrphanedProductImageObjectsHandler)}", StringComparison.Ordinal));
    }

    [Fact]
    public async Task Consume_Event_DeletesTheListedObjectsAndNothingElse()
    {
        await StoreAsync([.. Keys, "products/other/images/c.png"]);

        await ConsumerHarness.DeliverAsync<DeleteOrphanedProductImageObjectsHandler, ProductImageObjectsOrphanedIntegrationEvent>(
            Factory.Services, NewEvent(Keys));

        Assert.Equal(["products/other/images/c.png"], PublicStore.Keys);
    }

    [Fact]
    public async Task Consume_ObjectsAlreadyGone_Succeeds()
    {
        var exception = await Record.ExceptionAsync(() =>
            ConsumerHarness.DeliverAsync<DeleteOrphanedProductImageObjectsHandler, ProductImageObjectsOrphanedIntegrationEvent>(
                Factory.Services, NewEvent(Keys)));

        Assert.Null(exception);
    }

    [Fact]
    public async Task Consume_RedeliveredAfterTheIdempotencyKeyIsLost_DeletesNothingMore()
    {
        var @event = NewEvent(Keys);
        await StoreAsync(Keys);
        await ConsumerHarness.DeliverAsync<DeleteOrphanedProductImageObjectsHandler, ProductImageObjectsOrphanedIntegrationEvent>(
            Factory.Services, @event);
        await StoreAsync("products/lamp/images/new.png");
        await ConsumerHarness.ForgetAsync<DeleteOrphanedProductImageObjectsHandler>(Factory.Services, @event);

        var exception = await Record.ExceptionAsync(() =>
            ConsumerHarness.DeliverAsync<DeleteOrphanedProductImageObjectsHandler, ProductImageObjectsOrphanedIntegrationEvent>(
                Factory.Services, @event));

        Assert.Null(exception);
        Assert.Equal(["products/lamp/images/new.png"], PublicStore.Keys);
    }

    [Fact]
    public async Task Consume_WhenTheStoreFails_Throws_SoTheBusRetries()
    {
        var failingStore = Substitute.For<IPublicObjectStore>();
        failingStore.DeleteManyAsync(Arg.Any<IReadOnlyCollection<string>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException(new IOException("storage is down")));
        await using var scope = Factory.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var handler = new DeleteOrphanedProductImageObjectsHandler(
            failingStore,
            services.GetRequiredService<IFusionCache>(),
            services.GetRequiredService<IOptions<CachingOptions>>(),
            services.GetRequiredService<IInboxStore<IProductsDbContext>>(),
            NullLogger<DeleteOrphanedProductImageObjectsHandler>.Instance);

        var thrown = await Assert.ThrowsAsync<IOException>(() => ConsumerHarness.DeliverToAsync(handler, NewEvent(Keys)));

        Assert.Equal("storage is down", thrown.Message);
    }
}
