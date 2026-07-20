using Common.Application.Options;
using Common.Application.Persistence.Outbox;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using Outbox.Persistence;
using Respawn;
using Respawn.Graph;
using Xunit;

#pragma warning disable CA1707

namespace Outbox.Tests;

// Calls OutboxProcessor.ProcessBatchAsync directly (internal, via InternalsVisibleTo) instead of going
// through the BackgroundService's poll-interval timer — deterministic, no timing races. IsProcessor stays
// false (base IntegrationTestFactory default) so no auto-polling instance runs concurrently underneath.
public sealed class OutboxProcessorTests : IClassFixture<OutboxProcessorTestFactory>, IAsyncLifetime
{
    // This class doesn't inherit BaseIntegrationTest (it needs its own single-purpose factory, not the
    // shared IntegrationTestCollection one), so it doesn't get that base's per-test Respawn reset for free.
    // Without it, messages left behind by one test (e.g. released/retried rows) leak into a later test's
    // claim batch (ORDER BY CreatedOn LIMIT batchSize has no per-test filter) and skew its
    // RetryCount/consecutiveFailures assertions. Scoped to the Outbox schema only — this class never
    // touches any other module's tables.
    private static Respawner? _respawner;

    private readonly OutboxProcessorTestFactory _factory;
    private readonly FakePublishEndpoint _fakePublishEndpoint;

    public OutboxProcessorTests(OutboxProcessorTestFactory factory)
    {
        _factory = factory;
        _ = factory.CreateClient(); // eager — IClassFixture rule
        _fakePublishEndpoint = factory.Services.GetRequiredService<FakePublishEndpoint>();
        _fakePublishEndpoint.OnPublish = _ => Task.CompletedTask; // reset before every test
    }

    public async ValueTask InitializeAsync()
    {
        if (_respawner == null)
        {
            await using var conn = new NpgsqlConnection(_factory.ConnectionString);
            await conn.OpenAsync();

            _respawner = await Respawner.CreateAsync(conn,
                new RespawnerOptions
                {
                    DbAdapter = DbAdapter.Postgres,
                    SchemasToInclude = new[] { "Outbox" },
                    TablesToIgnore = new[] { new Table("__EFMigrationsHistory") }
                });
        }

        await using var connection = new NpgsqlConnection(_factory.ConnectionString);
        await connection.OpenAsync();

        await _respawner.ResetAsync(connection);
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

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
        MaxConsecutiveFailures = 3,
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

        // Capture "before" here, not a fresh UtcNow after the DB round trip below: ComputeBackoff uses
        // full jitter (uniform in [0, cap)), so the scheduled backoff can be near-zero. Asserting against
        // a later clock read races the SaveChangesAsync + GetMessageAsync round trip and flakes on a slow
        // CI runner (see OutboxProcessorTests CI failure 2026-07-20). Assert against the timestamp taken
        // before ProcessBatchAsync ran instead — NextRetryAt is always computed after this point.
        var before = DateTimeOffset.UtcNow;
        using var processor = CreateProcessor(BuildOptions(maxRetryCount: 3));
        await processor.ProcessBatchAsync(CancellationToken.None);
        var message = await GetMessageAsync(messageId);

        Assert.Equal(1, message.RetryCount);
        Assert.NotNull(message.NextRetryAt);
        Assert.True(message.NextRetryAt >= before);
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

        // See ProcessBatch_PublishFails_SchedulesRetryWithLease: assert against the timestamp taken
        // before ProcessBatchAsync ran, not a fresh UtcNow after the round trip below — full-jitter
        // backoff can be near-zero and a later clock read races the DB round trip.
        var before = DateTimeOffset.UtcNow;
        using var processor = CreateProcessor(BuildOptions(batchSize: 10, maxRetryCount: 5));
        await processor.ProcessBatchAsync(CancellationToken.None);

        for (var i = 0; i < 3; i++)
        {
            var attempted = await GetMessageAsync(ids[i]);
            Assert.Equal(1, attempted.RetryCount);
            Assert.NotNull(attempted.NextRetryAt);
            Assert.True(attempted.NextRetryAt >= before);
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
