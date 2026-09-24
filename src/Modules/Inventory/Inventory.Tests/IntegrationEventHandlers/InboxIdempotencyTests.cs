using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.IntegrationEventHandlers;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Tests.IntegrationEventHandlers;

// Drives IntegrationEventHandlerBase.Consume directly, one DI scope per delivery exactly as MassTransit does,
// against the real Inventory schema. The ConsumeContext substitute only carries the message and the token:
// the transport is not what is under test, the inbox row and the transaction around it are.
public sealed class InboxIdempotencyTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static ConsumeContext<ProductCreatedIntegrationEvent> DeliveryOf(ProductCreatedIntegrationEvent @event)
    {
        var context = Substitute.For<ConsumeContext<ProductCreatedIntegrationEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        return context;
    }

    private static ProductCreatedIntegrationEvent NewEvent()
        => new(DefaultIdType.CreateVersion7(), "Widget", "A widget", 5);

    private static string CacheKeyOf<THandler>(ProductCreatedIntegrationEvent @event)
        => $"processed_event:{typeof(THandler).Name}:{@event.Id}";

    private async Task<int> InboxRowsAsync(string consumerName, DefaultIdType messageId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.Set<ProcessedMessage>().AsNoTracking()
            .CountAsync(m => m.ConsumerName == consumerName && m.MessageId == messageId);
    }

    private async Task<int> StockLevelRowsAsync(DefaultIdType productId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockLevels.AsNoTracking().CountAsync(s => s.ProductId == productId);
    }

    // Inserts a StockLevel with no domain-level existence check, so only the inbox stands between a
    // redelivery and a second row.
    private sealed class InsertStockLevelHandler(
        IFusionCache cache,
        IOptions<CachingOptions> cachingOptions,
        IInboxStore<IInventoryDbContext> inbox,
        IInventoryDbContext db,
        ILogger<InsertStockLevelHandler> logger,
        Action onProcess
    ) : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, inbox, logger)
    {
        protected override async Task ProcessAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            onProcess();
            db.StockLevels.Add(StockLevel.Create(@event.ProductId, @event.Quantity));
            await db.SaveChangesAsync(cancellationToken);
        }
    }

    private sealed class ThrowingHandler(
        IFusionCache cache,
        IOptions<CachingOptions> cachingOptions,
        IInboxStore<IInventoryDbContext> inbox,
        ILogger<ThrowingHandler> logger
    ) : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, inbox, logger)
    {
        protected override Task ProcessAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken)
            => throw new InvalidOperationException("simulated handler failure");
    }

    private async Task ConsumeWithInsertHandlerAsync(ProductCreatedIntegrationEvent @event, Action onProcess)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var sp = scope.ServiceProvider;
        var handler = new InsertStockLevelHandler(
            sp.GetRequiredService<IFusionCache>(),
            sp.GetRequiredService<IOptions<CachingOptions>>(),
            sp.GetRequiredService<IInboxStore<IInventoryDbContext>>(),
            sp.GetRequiredService<IInventoryDbContext>(),
            sp.GetRequiredService<ILogger<InsertStockLevelHandler>>(),
            onProcess);

        await handler.Consume(DeliveryOf(@event));
    }

    [Fact]
    public async Task Consume_SameMessageTwiceWithCacheEvicted_OneInboxRowAndOneSideEffect()
    {
        var @event = NewEvent();
        var processed = 0;

        await ConsumeWithInsertHandlerAsync(@event, () => processed++);

        // Simulate the cache losing the key (Redis blip, eviction, other instance): the inbox must hold alone.
        await Factory.Services.GetRequiredService<IFusionCache>()
            .RemoveAsync(CacheKeyOf<InsertStockLevelHandler>(@event));

        await ConsumeWithInsertHandlerAsync(@event, () => processed++);

        Assert.Equal(2, processed); // second delivery ran ProcessAsync, its insert rolled back with the inbox key
        Assert.Equal(1, await StockLevelRowsAsync(@event.ProductId));
        Assert.Equal(1, await InboxRowsAsync(nameof(InsertStockLevelHandler), @event.Id));
    }

    [Fact]
    public async Task Consume_SameMessageTwiceWithCacheWarm_SecondSkippedBeforeProcessAsync()
    {
        var @event = NewEvent();
        var processed = 0;

        await ConsumeWithInsertHandlerAsync(@event, () => processed++);
        await ConsumeWithInsertHandlerAsync(@event, () => processed++);

        Assert.Equal(1, processed);
        Assert.Equal(1, await StockLevelRowsAsync(@event.ProductId));
        Assert.Equal(1, await InboxRowsAsync(nameof(InsertStockLevelHandler), @event.Id));
    }

    [Fact]
    public async Task Consume_ProcessAsyncThrows_LeavesNoInboxRowAndNoCacheKey()
    {
        var @event = NewEvent();

        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var sp = scope.ServiceProvider;
            var handler = new ThrowingHandler(
                sp.GetRequiredService<IFusionCache>(),
                sp.GetRequiredService<IOptions<CachingOptions>>(),
                sp.GetRequiredService<IInboxStore<IInventoryDbContext>>(),
                sp.GetRequiredService<ILogger<ThrowingHandler>>());

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Consume(DeliveryOf(@event)));
        }

        Assert.Equal(0, await InboxRowsAsync(nameof(ThrowingHandler), @event.Id));

        var cached = await Factory.Services.GetRequiredService<IFusionCache>()
            .TryGetAsync<bool>(CacheKeyOf<ThrowingHandler>(@event));
        Assert.False(cached.HasValue);
    }

    [Fact]
    public async Task Consume_HandlerThatSavesNothing_StillWritesInboxRow()
    {
        var @event = NewEvent();

        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            // Registered in DI by MassTransit's consumer scan; LogProductCatalogChangeHandler only logs.
            var handler = scope.ServiceProvider.GetRequiredService<LogProductCatalogChangeHandler>();
            await handler.Consume(DeliveryOf(@event));
        }

        Assert.Equal(1, await InboxRowsAsync(nameof(LogProductCatalogChangeHandler), @event.Id));
    }

    [Fact]
    public async Task CleanupAsync_DeletesOnlyRowsOlderThanTheCutoff_InBatches()
    {
        var now = DateTimeOffset.UtcNow;
        var consumer = $"CleanupProbe{Guid.NewGuid():N}";
        await using (var seed = Factory.Services.CreateAsyncScope())
        {
            var db = (DbContext)seed.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            for (var i = 0; i < 5; i++)
            {
                await InsertProcessedMessageAsync(db, consumer, now.AddDays(-2));
            }

            await InsertProcessedMessageAsync(db, consumer, now);
        }

        // Batch size 2 over 5 old rows: 2 + 2 + 1, then 0 ends the loop. Exercises Take + ExecuteDeleteAsync on the
        // composite (ConsumerName, MessageId) key.
        var batches = new List<int>();
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var inbox = scope.ServiceProvider.GetRequiredService<IInboxStore<IInventoryDbContext>>();
            int deleted;
            do
            {
                deleted = await inbox.CleanupAsync(now.AddDays(-1), batchSize: 2, CancellationToken.None);
                batches.Add(deleted);
            } while (deleted > 0);
        }

        Assert.Equal([2, 2, 1, 0], batches);
        await using var verify = Factory.Services.CreateAsyncScope();
        var remaining = await verify.ServiceProvider.GetRequiredService<IInventoryDbContext>()
            .Set<ProcessedMessage>().AsNoTracking()
            .Where(m => m.ConsumerName == consumer)
            .Select(m => m.ProcessedOn)
            .ToListAsync();
        var fresh = Assert.Single(remaining);
        Assert.Equal(now, fresh, TimeSpan.FromSeconds(1));
    }

    private static Task<int> InsertProcessedMessageAsync(DbContext db, string consumer, DateTimeOffset processedOn)
        => db.Database.ExecuteSqlAsync($"""
            INSERT INTO "Inventory"."ProcessedMessages" ("ConsumerName", "MessageId", "ProcessedOn", "CreatedOn")
            VALUES ({consumer}, {DefaultIdType.CreateVersion7()}, {processedOn}, {processedOn})
            """);
}
