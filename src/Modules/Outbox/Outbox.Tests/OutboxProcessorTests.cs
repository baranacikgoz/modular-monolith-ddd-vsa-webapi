using Common.Application.Options;
using Common.Application.Persistence.Outbox;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;
using Xunit;

#pragma warning disable CA1707

namespace Outbox.Tests;

// Calls OutboxProcessor.ProcessBatchAsync directly (internal, via InternalsVisibleTo) instead of going
// through the BackgroundService's poll-interval timer — deterministic, no timing races. IsProcessor stays
// false (base IntegrationTestFactory default) so no auto-polling instance runs concurrently underneath.
public sealed class OutboxProcessorTests : IClassFixture<OutboxProcessorTestFactory>
{
    private readonly OutboxProcessorTestFactory _factory;
    private readonly FakePublishEndpoint _fakePublishEndpoint;

    public OutboxProcessorTests(OutboxProcessorTestFactory factory)
    {
        _factory = factory;
        _ = factory.CreateClient(); // eager — IClassFixture rule
        _fakePublishEndpoint = factory.Services.GetRequiredService<FakePublishEndpoint>();
        _fakePublishEndpoint.OnPublish = _ => Task.CompletedTask; // reset before every test
    }

    private static OutboxOptions BuildOptions(int batchSize = 10, int maxRetryCount = 3) => new()
    {
        PollIntervalMs = 100_000, // never consulted — ProcessBatchAsync is called directly, not via the loop
        BatchSize = batchSize,
        MaxRetryCount = maxRetryCount,
        IsProcessor = false,
        BaseBackoffSeconds = 1,
        MaxBackoffSeconds = 2,
        PublishTimeoutMs = 2000,
        ClaimLeaseSeconds = 120,
        LagThresholdMinutes = 5,
        MetricsCronSchedule = "*/5 * * * *"
    };

    private OutboxProcessor CreateProcessor(OutboxOptions options) => new(
        _factory.Services.GetRequiredService<IServiceScopeFactory>(),
        Options.Create(options),
        _factory.Services.GetRequiredService<TimeProvider>(),
        _factory.Services.GetRequiredService<ILogger<OutboxProcessor>>());

    private async Task<int> SeedMessageAsync(DateTimeOffset createdOn, string phoneSuffix, Action<OutboxMessage>? configure = null)
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var message = OutboxMessage.Create(createdOn, new UserRegisteredIntegrationEvent(
            ApplicationUserId.New(), "Test User", $"+90555{phoneSuffix}"));
        configure?.Invoke(message);
        db.OutboxMessages.Add(message);
        await db.SaveChangesAsync();
        return message.Id;
    }

    private async Task<OutboxMessage> GetMessageAsync(int id)
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        return await db.OutboxMessages.AsNoTracking().SingleAsync(m => m.Id == id);
    }

    [Fact]
    public async Task ProcessBatch_PublishSucceeds_MarksProcessed()
    {
        var messageId = await SeedMessageAsync(DateTimeOffset.UtcNow, "0001001");

        using var processor = CreateProcessor(BuildOptions());
        var processed = await processor.ProcessBatchAsync(CancellationToken.None);
        var message = await GetMessageAsync(messageId);

        Assert.True(processed >= 1);
        Assert.True(message.IsProcessed);
        Assert.NotNull(message.ProcessedOn);
        Assert.Null(message.FailedOn);
    }

    [Fact]
    public async Task ProcessBatch_PublishFails_SchedulesRetryWithLease()
    {
        _fakePublishEndpoint.OnPublish = _ => throw new InvalidOperationException("simulated broker failure");

        var messageId = await SeedMessageAsync(DateTimeOffset.UtcNow, "0001002");

        using var processor = CreateProcessor(BuildOptions(maxRetryCount: 3));
        await processor.ProcessBatchAsync(CancellationToken.None);
        var message = await GetMessageAsync(messageId);

        Assert.Equal(1, message.RetryCount);
        Assert.NotNull(message.NextRetryAt);
        Assert.True(message.NextRetryAt > DateTimeOffset.UtcNow);
        Assert.False(message.IsProcessed);
        Assert.Null(message.FailedOn);
    }

    [Fact]
    public async Task ProcessBatch_RetriesExhausted_MarksFailed()
    {
        _fakePublishEndpoint.OnPublish = _ => throw new InvalidOperationException("simulated broker failure");

        const int maxRetryCount = 3;
        var messageId = await SeedMessageAsync(DateTimeOffset.UtcNow, "0001003", message =>
        {
            for (var i = 0; i < maxRetryCount - 1; i++)
            {
                message.IncrementRetryCount(DateTimeOffset.UtcNow, TimeSpan.Zero);
            }

            message.ReleaseClaim(); // make immediately eligible — IncrementRetryCount left NextRetryAt set
        });

        using var processor = CreateProcessor(BuildOptions(maxRetryCount: maxRetryCount));
        await processor.ProcessBatchAsync(CancellationToken.None);
        var message = await GetMessageAsync(messageId);

        Assert.Equal(maxRetryCount, message.RetryCount);
        Assert.NotNull(message.FailedOn);
        Assert.False(message.IsProcessed);
    }

    [Fact]
    public async Task ProcessBatch_ThreeConsecutiveFailures_ReleasesUnattemptedClaims()
    {
        _fakePublishEndpoint.OnPublish = _ => throw new InvalidOperationException("simulated broker failure");

        var now = DateTimeOffset.UtcNow;
        var ids = new List<int>();
        for (var i = 0; i < 5; i++)
        {
            ids.Add(await SeedMessageAsync(now.AddMilliseconds(i), $"000200{i}"));
        }

        using var processor = CreateProcessor(BuildOptions(batchSize: 10, maxRetryCount: 5));
        await processor.ProcessBatchAsync(CancellationToken.None);

        for (var i = 0; i < 3; i++)
        {
            var attempted = await GetMessageAsync(ids[i]);
            Assert.Equal(1, attempted.RetryCount);
            Assert.NotNull(attempted.NextRetryAt);
            Assert.True(attempted.NextRetryAt > DateTimeOffset.UtcNow);
        }

        for (var i = 3; i < 5; i++)
        {
            var released = await GetMessageAsync(ids[i]);
            Assert.Equal(0, released.RetryCount);
            Assert.Null(released.NextRetryAt);
        }
    }

    [Fact]
    public async Task ProcessBatch_ConcurrentRuns_EachMessagePublishedOnce()
    {
        var publishCount = 0;
        _fakePublishEndpoint.OnPublish = _ =>
        {
            Interlocked.Increment(ref publishCount);
            return Task.CompletedTask;
        };

        var now = DateTimeOffset.UtcNow;
        var ids = new List<int>();
        for (var i = 0; i < 20; i++)
        {
            ids.Add(await SeedMessageAsync(now.AddMilliseconds(i), $"00030{i:D2}"));
        }

        // BatchSize (12) < total messages (20): neither single ProcessBatchAsync call can claim everything
        // alone, so the two concurrent calls are forced to split the 20 rows between them. FOR UPDATE
        // SKIP LOCKED guarantees the split has no overlap — this is what's under test.
        using var processor = CreateProcessor(BuildOptions(batchSize: 12));
        await Task.WhenAll(
            processor.ProcessBatchAsync(CancellationToken.None),
            processor.ProcessBatchAsync(CancellationToken.None));

        Assert.Equal(20, publishCount);

        await using var scope = _factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var processedCount = await db.OutboxMessages.AsNoTracking().CountAsync(m => ids.Contains(m.Id) && m.IsProcessed);
        Assert.Equal(20, processedCount);
    }
}
