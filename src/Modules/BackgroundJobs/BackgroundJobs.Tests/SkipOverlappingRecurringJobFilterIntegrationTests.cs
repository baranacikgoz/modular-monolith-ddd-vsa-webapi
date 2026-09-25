using Hangfire;
using Hangfire.Common;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.Logging.Abstractions;
using Testcontainers.PostgreSql;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace BackgroundJobs.Tests;

/// <summary>Public because Hangfire's <c>Job.FromExpression</c> only accepts public types and methods.</summary>
public static class OverlapProbeJob
{
    private static int _started;
    private static TaskCompletionSource _entered = NewSignal();
    private static TaskCompletionSource _release = NewSignal();

    public static int Started => Volatile.Read(ref _started);

    public static void Reset()
    {
        Interlocked.Exchange(ref _started, 0);
        _entered = NewSignal();
        _release = NewSignal();
    }

    public static Task Entered => _entered.Task;

    public static void Release()
    {
        _release.TrySetResult();
    }

    public static void ArmNextRun()
    {
        _entered = NewSignal();
    }

    public static void Run()
    {
        Interlocked.Increment(ref _started);
        _entered.TrySetResult();
        _release.Task.Wait(TimeSpan.FromSeconds(30));
    }

    private static TaskCompletionSource NewSignal() => new(TaskCreationOptions.RunContinuationsAsynchronously);
}

/// <summary>
///     The filter against real Hangfire and a real Postgres storage: proves the scheduler puts the RecurringJobId
///     parameter on a triggered run and that Hangfire.PostgreSql's distributed lock refuses a second concurrent run
///     immediately, which the mocked tests cannot show.
/// </summary>
public sealed class SkipOverlappingRecurringJobFilterIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:15-alpine").Build();

    public async ValueTask InitializeAsync()
    {
        await _postgres.StartAsync(TestContext.Current.CancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task RecurringJobTriggeredWhileItsPreviousRunIsStillGoing_IsSkippedAndRunsAgainAfterwards()
    {
        OverlapProbeJob.Reset();
        var storageOptions = new PostgreSqlStorageOptions { QueuePollInterval = TimeSpan.FromMilliseconds(100) };
        var storage = new PostgreSqlStorage(new NpgsqlConnectionFactory(_postgres.GetConnectionString(), storageOptions, null), storageOptions);
        var filters = new JobFilterCollection { new SkipOverlappingRecurringJobFilter(NullLogger<SkipOverlappingRecurringJobFilter>.Instance) };
        using var server = new BackgroundJobServer(
            new BackgroundJobServerOptions { WorkerCount = 4, FilterProvider = filters, ServerName = "overlap-test" }, storage);
        var recurring = new RecurringJobManager(storage, filters);
        recurring.AddOrUpdate("overlap-probe", Job.FromExpression(() => OverlapProbeJob.Run()), Cron.Yearly(), new RecurringJobOptions());

        recurring.TriggerJob("overlap-probe");
        await OverlapProbeJob.Entered.WaitAsync(TimeSpan.FromSeconds(30), TestContext.Current.CancellationToken);

        // The first run is inside the job and holds the lock: a second tick must be cancelled, not queued behind it.
        recurring.TriggerJob("overlap-probe");
        await Task.Delay(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken);
        Assert.Equal(1, OverlapProbeJob.Started);

        // Once the first run is over the lock is free again and the next tick runs.
        OverlapProbeJob.ArmNextRun();
        OverlapProbeJob.Release();
        await Task.Delay(TimeSpan.FromSeconds(1), TestContext.Current.CancellationToken);
        recurring.TriggerJob("overlap-probe");
        await OverlapProbeJob.Entered.WaitAsync(TimeSpan.FromSeconds(30), TestContext.Current.CancellationToken);

        Assert.Equal(2, OverlapProbeJob.Started);
    }
}
