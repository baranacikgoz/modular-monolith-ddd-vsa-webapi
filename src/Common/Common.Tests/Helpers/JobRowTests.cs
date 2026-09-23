using Common.Application.Jobs;
using Common.Application.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Common.Tests.Helpers;

public class JobRowTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DateTimeOffset Now = new(2026, 9, 24, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task TryClaimAsync_SecondClaimOfTheSameJob_ReturnsFalse()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var job = new SampleJob(null, Now);
        db.Jobs.Add(job);
        await db.SaveChangesAsync();

        var first = await db.Jobs.TryClaimAsync(job.Id, Now, CancellationToken.None);
        var second = await db.Jobs.TryClaimAsync(job.Id, Now.AddSeconds(1), CancellationToken.None);

        Assert.True(first);
        Assert.False(second);
        var row = await db.Jobs.AsNoTracking().SingleAsync(j => j.Id == job.Id);
        Assert.Equal(JobStatus.Running, row.Status);
        Assert.Equal(Now, row.StartedOn);
    }

    [Fact]
    public async Task RequeueAsync_RunningJob_BackToQueuedAndClaimableAgain()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var job = new SampleJob(null, Now);
        db.Jobs.Add(job);
        await db.SaveChangesAsync();
        Assert.True(await db.Jobs.TryClaimAsync(job.Id, Now, CancellationToken.None));

        var requeued = await db.Jobs.RequeueAsync(job.Id, CancellationToken.None);

        Assert.True(requeued);
        var row = await db.Jobs.AsNoTracking().SingleAsync(j => j.Id == job.Id);
        Assert.Equal(JobStatus.Queued, row.Status);
        Assert.Null(row.StartedOn);
        Assert.True(await db.Jobs.TryClaimAsync(job.Id, Now, CancellationToken.None));
    }

    [Fact]
    public async Task Housekeeping_FailsStaleRunningRowsAndDeletesExpiredFinishedRowsInPages()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var staleRunning = new SampleJob(null, Now.AddHours(-3));
        staleRunning.MarkRunning(Now.AddHours(-2));
        var freshRunning = new SampleJob(null, Now.AddMinutes(-10));
        freshRunning.MarkRunning(Now.AddMinutes(-5));
        var expired = Enumerable.Range(0, 5).Select(_ =>
        {
            var j = new SampleJob(null, Now.AddDays(-9));
            j.MarkSucceeded(Now.AddDays(-8));
            return j;
        }).ToList();
        var recentFinished = new SampleJob(null, Now.AddHours(-2));
        recentFinished.MarkFailed(Now.AddHours(-1), "SomethingWentWrong");
        List<SampleJob> rows = [staleRunning, freshRunning, recentFinished, .. expired];
        db.Jobs.AddRange(rows);
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();

        var timeProvider = Substitute.For<TimeProvider>();
        timeProvider.GetUtcNow().Returns(Now);
        var housekeeping = new SampleHousekeepingJob(db,
            Options.Create(new JobHousekeepingOptions { StaleAfterMinutes = 60, RetentionHours = 24 * 7, PageSize = 2 }),
            timeProvider);
        var (staleFailed, deleted) = await housekeeping.RunAsync();

        Assert.Equal(1, staleFailed);
        Assert.Equal(5, deleted);
        var stale = await db.Jobs.AsNoTracking().SingleAsync(j => j.Id == staleRunning.Id);
        Assert.Equal(JobStatus.Failed, stale.Status);
        Assert.Equal(JobClaimExtensions.StaleFailureKey, stale.FailureKey);
        Assert.Equal(Now, stale.FinishedOn);
        Assert.Equal(JobStatus.Running, (await db.Jobs.AsNoTracking().SingleAsync(j => j.Id == freshRunning.Id)).Status);
        Assert.NotNull(await db.Jobs.AsNoTracking().SingleOrDefaultAsync(j => j.Id == recentFinished.Id));
        var expiredIds = expired.Select(j => j.Id).ToList();
        Assert.Equal(0, await db.Jobs.AsNoTracking().CountAsync(j => expiredIds.Contains(j.Id)));
    }

    [Fact]
    public void MarkFailed_LongFailureKey_TruncatesToColumnLength()
    {
        var job = new SampleJob(null, Now);

        job.MarkFailed(Now, new string('x', 300));

        Assert.Equal(JobRow<SampleJobId>.FailureKeyMaxLength, job.FailureKey!.Length);
        Assert.Equal(JobStatus.Failed, job.Status);
    }

    private sealed class SampleHousekeepingJob(
        HelpersTestDbContext db, IOptions<JobHousekeepingOptions> options, TimeProvider timeProvider)
        : JobHousekeepingJob<SampleJob, SampleJobId>(db, options, timeProvider, NullLogger.Instance);
}
