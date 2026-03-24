using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;
using NSubstitute;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace BackgroundJobs.Tests;

public class RecurringBackgroundJobsServiceTests
{
    private readonly IRecurringJobManagerV2 _recurringJobManager;
    private readonly TimeProvider _timeProvider;
    private readonly RecurringBackgroundJobsService _sut;

    public RecurringBackgroundJobsServiceTests()
    {
        _recurringJobManager = Substitute.For<IRecurringJobManagerV2>();
        _timeProvider = Substitute.For<TimeProvider>();
        _timeProvider.LocalTimeZone.Returns(TimeZoneInfo.Utc);
        _sut = new RecurringBackgroundJobsService(_recurringJobManager, _timeProvider);
    }

    [Fact]
    public void AddOrUpdate_WithAction_ShouldDelegateToHangfireManager()
    {
        // Arrange
        var jobId = "recurring-123";
        Expression<Action> expr = () => DummyMethod();
        Func<string> cronExpr = () => "0 0 * * *";

        // Act
        _sut.AddOrUpdate(jobId, expr, cronExpr);

        // Assert
        _recurringJobManager.Received(1).AddOrUpdate(
            jobId,
            Arg.Any<Job>(),
            Arg.Is<string>(x => x == "0 0 * * *"),
            Arg.Is<RecurringJobOptions>(opts => opts.MisfireHandling == MisfireHandlingMode.Relaxed && opts.TimeZone == _timeProvider.LocalTimeZone));
    }

    [Fact]
    public void AddOrUpdate_WithFuncTask_ShouldDelegateToHangfireManager()
    {
        // Arrange
        var jobId = "recurring-task-123";
        Expression<Func<Task>> expr = () => DummyTaskMethod();
        Func<string> cronExpr = () => "*/5 * * * *";

        // Act
        _sut.AddOrUpdate(jobId, expr, cronExpr);

        // Assert
        _recurringJobManager.Received(1).AddOrUpdate(
            jobId,
            Arg.Any<Job>(),
            Arg.Is<string>(x => x == "*/5 * * * *"),
            Arg.Is<RecurringJobOptions>(opts => opts.MisfireHandling == MisfireHandlingMode.Relaxed && opts.TimeZone == _timeProvider.LocalTimeZone));
    }

    public static void DummyMethod()
    {
        // Dummy method for expression tree
    }

    public static Task DummyTaskMethod()
    {
        // Dummy method for expression tree
        return Task.CompletedTask;
    }
}
