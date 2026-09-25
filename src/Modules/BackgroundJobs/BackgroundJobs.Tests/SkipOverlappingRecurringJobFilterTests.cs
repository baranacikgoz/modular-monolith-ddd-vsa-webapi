using System.Diagnostics.Metrics;
using BackgroundJobs.Telemetry;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.Storage;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace BackgroundJobs.Tests;

public class SkipOverlappingRecurringJobFilterTests
{
    private readonly IStorageConnection _connection = Substitute.For<IStorageConnection>();
    private readonly SkipOverlappingRecurringJobFilter _sut = new(NullLogger<SkipOverlappingRecurringJobFilter>.Instance);

    private PerformContext ContextFor(string? recurringJobId)
    {
        _connection.GetJobParameter("job-1", "RecurringJobId")
            .Returns(recurringJobId is null ? null : SerializationHelper.Serialize(recurringJobId, SerializationOption.User));

        var backgroundJob = new BackgroundJob("job-1", Job.FromExpression(() => JobTargets.DummyMethod()), DateTime.UtcNow);
        return new PerformContext(Substitute.For<JobStorage>(), _connection, backgroundJob, Substitute.For<IJobCancellationToken>());
    }

    [Fact]
    public void OnPerforming_JobWithoutRecurringJobId_TakesNoLockAndDoesNotCancel()
    {
        var performing = new PerformingContext(ContextFor(recurringJobId: null));

        _sut.OnPerforming(performing);

        Assert.False(performing.Canceled);
        _connection.DidNotReceive().AcquireDistributedLock(Arg.Any<string>(), Arg.Any<TimeSpan>());
    }

    [Fact]
    public void OnPerforming_RecurringJobWithFreeLock_TakesTheLockWithoutWaitingAndReleasesItWhenDone()
    {
        var heldLock = Substitute.For<IDisposable>();
        _connection.AcquireDistributedLock("recurring-job:sweep", TimeSpan.Zero).Returns(heldLock);
        var context = ContextFor("sweep");
        var performing = new PerformingContext(context);

        _sut.OnPerforming(performing);

        Assert.False(performing.Canceled);
        heldLock.DidNotReceive().Dispose();

        _sut.OnPerformed(new PerformedContext(context, null, false, null));

        heldLock.Received(1).Dispose();
    }

    [Fact]
    public void OnPerforming_RecurringJobWhosePreviousRunHoldsTheLock_CancelsTheRunAndMarksItSkipped()
    {
        _connection.AcquireDistributedLock("recurring-job:sweep", TimeSpan.Zero)
            .Returns(_ => throw new DistributedLockTimeoutException("recurring-job:sweep"));
        var context = ContextFor("sweep");
        var performing = new PerformingContext(context);

        _sut.OnPerforming(performing);

        Assert.True(performing.Canceled);
        Assert.True(context.Items.ContainsKey(SkipOverlappingRecurringJobFilter.SkippedItemKey));

        // Nothing was acquired, so finishing the cancelled run releases nothing and must not throw.
        _sut.OnPerformed(new PerformedContext(context, null, true, null));
    }

    [Fact]
    public void MetricsFilter_RunSkippedByTheOverlapFilter_RecordsSkippedNotFailure()
    {
        var outcomes = new List<string>();
        using var listener = new MeterListener();
        listener.InstrumentPublished = (instrument, meterListener) =>
        {
            if (instrument.Meter.Name == BackgroundJobsTelemetry.MeterName && instrument.Name == "backgroundjobs.executions.total")
            {
                meterListener.EnableMeasurementEvents(instrument);
            }
        };
        listener.SetMeasurementEventCallback<long>((_, _, tags, _) =>
        {
            var tagList = tags.ToArray();
            if (tagList.Any(t => t.Key == "job.name" && (string?)t.Value == nameof(JobTargets)))
            {
                outcomes.AddRange(tagList.Where(t => t.Key == "job.outcome").Select(t => (string)t.Value!));
            }
        });
        listener.Start();

        var context = ContextFor("sweep");
        context.Items[SkipOverlappingRecurringJobFilter.SkippedItemKey] = true;

        new JobMetricsFilter().OnPerformed(new PerformedContext(context, null, true, null));

        Assert.Equal(["skipped"], outcomes);
    }
}
