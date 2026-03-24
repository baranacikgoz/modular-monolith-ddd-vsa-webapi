using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using NSubstitute;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace BackgroundJobs.Tests;

public class BackgroundJobsServiceTests
{
    private readonly IBackgroundJobClientV2 _hangfireClient;
    private readonly BackgroundJobsService _sut; // Subject under test

    public BackgroundJobsServiceTests()
    {
        _hangfireClient = Substitute.For<IBackgroundJobClientV2>();
        _sut = new BackgroundJobsService(_hangfireClient);
    }

    [Fact]
    public void Enqueue_WithAction_ShouldDelegateToHangfireClient()
    {
        // Arrange
        Expression<Action> expr = () => DummyMethod();
        _hangfireClient.Create(Arg.Any<Job>(), Arg.Any<EnqueuedState>()).Returns("job-123");

        // Act
        // Actually, Hangfire extension method Enqueue creates the job and sets state.
        // We can just verify the extension method wrapper works by mocking internal Create, but
        // since IBackgroundJobClientV2 is an interface, NSubstitute creates it fine.
        _sut.Enqueue(expr);

        // Assert
        // The extension method calls Create(job, new EnqueuedState("default")) under the hood usually,
        // or we can just verify the interface method if this were an adapter to methods, but Enqueue is an extension method for IBackgroundJobClient.
        // However, looking at the code, client.Enqueue is called. If client.Enqueue is an interface method (or extension), we just verify it didn't crash.
        // Wait, Hangfire's Enqueue is an extension method over IBackgroundJobClient. We can't easily assert extension methods with NSubstitute.
        // What we CAN assert is that Create is called, because the extension method calls Create.
        _hangfireClient.ReceivedWithAnyArgs().Create(default, default);
    }

    [Fact]
    public void Schedule_WithActionAndDelay_ShouldDelegateToHangfireClient()
    {
        // Arrange
        Expression<Action> expr = () => DummyMethod();
        var delay = TimeSpan.FromMinutes(5);

        // Act
        _sut.Schedule(expr, delay);

        // Assert
        _hangfireClient.ReceivedWithAnyArgs().Create(default, default);
    }

    [Fact]
    public void Delete_JobId_ShouldDelegateToHangfireClient()
    {
        // Arrange
        var jobId = "job-123";
        _hangfireClient.ChangeState(jobId, Arg.Any<DeletedState>(), Arg.Any<string>()).Returns(true);

        // Act
        // Delete is also an extension method that calls ChangeState(jobId, new DeletedState())
        _sut.Delete(jobId);

        // Assert
        _hangfireClient.ReceivedWithAnyArgs().ChangeState(default, default, default);
    }

    public static void DummyMethod()
    {
        // Dummy method for expression tree
    }
}
