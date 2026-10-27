using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.BulkAdjustments;

/// <summary>The sweep finds jobs a crashed consumer left Running and hands them back to the queue; it only publishes, it never
/// runs a job. The lease is 15 minutes in the host's options and in these tests.</summary>
public class StockJobRequeueServiceTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    private static readonly TimeSpan PastTheLease = TimeSpan.FromMinutes(30);

    private async Task SweepAsync(Action<InventoryOptions>? configure = null)
    {
        using var scope = Factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var sweep = new StockJobRequeueService(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<IIntegrationEventOutbox>(),
            TimeProvider.System,
            TestInventoryOptions.Wrapped(configure),
            NullLogger<StockJobRequeueService>.Instance);

        await sweep.SweepAsync();
    }

    /// <summary>One user per job: a user may have only one active job.</summary>
    private async Task<StockBulkAdjustJob> AddRunningJobAsync(TimeSpan runningFor, int requeueCount = 0)
    {
        var job = StockBulkAdjustJob.Queue(
            StockBulkAdjustJobId.New(), new ApplicationUserId(Guid.NewGuid()), DateTimeOffset.UtcNow.AddHours(-2),
            Guid.NewGuid().ToString("N"), "adjustments.xlsx", "key", "Cycle count");
        job.MarkRunning(DateTimeOffset.UtcNow - runningFor);
        Db.StockBulkAdjustJobs.Add(job);
        await Db.SaveChangesAsync();
        if (requeueCount > 0)
        {
            await Db.StockBulkAdjustJobs.Where(j => j.Id == job.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.RequeueCount, requeueCount));
        }

        return job;
    }

    private async Task<StockBulkAdjustJob> ReloadAsync(StockBulkAdjustJobId id)
    {
        using var scope = Factory.Services.CreateScope();
        return await scope.ServiceProvider.GetRequiredService<IInventoryDbContext>().StockBulkAdjustJobs
            .AsNoTracking().SingleAsync(j => j.Id == id);
    }

    private async Task<List<DefaultIdType>> RequestedJobIdsAsync()
    {
        return (await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>().Select(e => e.JobId).ToList();
    }

    [Fact]
    public async Task Sweep_JobRunningPastTheLease_GoesBackToQueuedWithAFreshMessage()
    {
        var stuck = await AddRunningJobAsync(PastTheLease);

        await SweepAsync();

        var job = await ReloadAsync(stuck.Id);
        Assert.Equal(JobStatus.Queued, job.Status);
        Assert.Null(job.StartedOn);
        Assert.Equal(1, job.RequeueCount);
        Assert.Equal([stuck.Id.Value], await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_JobRunningWithinTheLease_IsLeftAlone()
    {
        var fresh = await AddRunningJobAsync(TimeSpan.FromMinutes(1));

        await SweepAsync();

        var job = await ReloadAsync(fresh.Id);
        Assert.Equal(JobStatus.Running, job.Status);
        Assert.NotNull(job.StartedOn);
        Assert.Equal(0, job.RequeueCount);
        Assert.Empty(await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_OldAndFreshRunningJobsTogether_RequeuesOnlyTheOldOne()
    {
        var stuck = await AddRunningJobAsync(PastTheLease);
        var fresh = await AddRunningJobAsync(TimeSpan.FromMinutes(1));

        await SweepAsync();

        Assert.Equal(JobStatus.Queued, (await ReloadAsync(stuck.Id)).Status);
        Assert.Equal(JobStatus.Running, (await ReloadAsync(fresh.Id)).Status);
        Assert.Equal([stuck.Id.Value], await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_QueuedAndFinishedJobs_AreLeftAlone()
    {
        var queued = StockBulkAdjustJob.Queue(
            StockBulkAdjustJobId.New(), new ApplicationUserId(Guid.NewGuid()), DateTimeOffset.UtcNow.AddHours(-2),
            Guid.NewGuid().ToString("N"), "adjustments.xlsx", "key", "Cycle count");
        var finished = StockBulkAdjustJob.Queue(
            StockBulkAdjustJobId.New(), new ApplicationUserId(Guid.NewGuid()), DateTimeOffset.UtcNow.AddHours(-2),
            Guid.NewGuid().ToString("N"), "adjustments.xlsx", "key", "Cycle count");
        finished.MarkRunning(DateTimeOffset.UtcNow - PastTheLease);
        finished.MarkSucceeded(DateTimeOffset.UtcNow.AddMinutes(-20));
        Db.StockBulkAdjustJobs.AddRange(queued, finished);
        await Db.SaveChangesAsync();

        await SweepAsync();

        Assert.Equal(JobStatus.Queued, (await ReloadAsync(queued.Id)).Status);
        Assert.Equal(JobStatus.Succeeded, (await ReloadAsync(finished.Id)).Status);
        Assert.Empty(await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_JobAlreadyRequeuedAsOftenAsAllowed_IsFailedAsAbandonedInsteadOfQueuedAgain()
    {
        var poisoned = await AddRunningJobAsync(PastTheLease, requeueCount: 3);

        await SweepAsync(o => o.StockJobMaxRequeues = 3);

        var job = await ReloadAsync(poisoned.Id);
        Assert.Equal(JobStatus.Failed, job.Status);
        Assert.Equal(StockBulkAdjustErrors.JobAbandoned, job.FailureKey);
        Assert.NotNull(job.FinishedOn);
        Assert.Empty(await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_MoreStuckJobsThanOnePage_RequeuesEveryOne()
    {
        var stuck = new List<StockBulkAdjustJob>();
        for (var i = 0; i < 5; i++)
        {
            stuck.Add(await AddRunningJobAsync(PastTheLease));
        }

        await SweepAsync(o => o.SweepPageSize = 2);

        Assert.Equal(stuck.Select(j => j.Id.Value).Order(), (await RequestedJobIdsAsync()).Order());
        foreach (var job in stuck)
        {
            Assert.Equal(JobStatus.Queued, (await ReloadAsync(job.Id)).Status);
        }
    }

    /// <summary>The sweep reads the stuck jobs first and changes them afterwards, so a consumer may claim one again in between
    /// (another sweep requeued it, a redelivery finally arrived). The change itself carries the lease test.</summary>
    [Fact]
    public async Task RequeueAndAbandon_JobClaimedAgainAfterItWasSeenStuck_AreLeftToItsConsumer()
    {
        var reclaimed = await AddRunningJobAsync(TimeSpan.FromSeconds(10));
        using var scope = Factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var sweep = new StockJobRequeueService(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<IIntegrationEventOutbox>(),
            TimeProvider.System,
            TestInventoryOptions.Wrapped(),
            NullLogger<StockJobRequeueService>.Instance);
        var leaseExpiredBefore = DateTimeOffset.UtcNow.AddMinutes(-15);

        Assert.False(await sweep.RequeueAsync(reclaimed.Id, leaseExpiredBefore, CancellationToken.None));
        Assert.False(await sweep.AbandonAsync(reclaimed.Id, leaseExpiredBefore, DateTimeOffset.UtcNow, CancellationToken.None));

        Assert.Equal(JobStatus.Running, (await ReloadAsync(reclaimed.Id)).Status);
        Assert.Empty(await RequestedJobIdsAsync());
    }

    [Fact]
    public async Task Sweep_RequeuedJobCanBeClaimedAgain()
    {
        var stuck = await AddRunningJobAsync(PastTheLease);

        await SweepAsync();

        using var scope = Factory.Services.CreateScope();
        var jobs = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>().StockBulkAdjustJobs;
        Assert.True(await jobs.TryClaimAsync(stuck.Id, DateTimeOffset.UtcNow, CancellationToken.None));
    }

    [Fact]
    public async Task StartAsync_RegistersTheSweepOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var services = new ServiceCollection().AddSingleton(recurringJobs).BuildServiceProvider();
        var registrar = new StockJobRequeueJobRegistrar(
            services,
            Options.Create(TestInventoryOptions.Create(configure: o => o.StockJobRequeueCron = "15 */5 * * * *")),
            NullLogger<StockJobRequeueJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "inventory-stock-job-requeue",
            Arg.Any<Expression<Func<StockJobRequeueService, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "15 */5 * * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var registrar = new StockJobRequeueJobRegistrar(
            new ServiceCollection().BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create()),
            NullLogger<StockJobRequeueJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);
    }
}
