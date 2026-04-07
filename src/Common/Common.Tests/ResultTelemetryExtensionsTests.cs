using System.Diagnostics;
using System.Net;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515 // Consider making public types internal
#pragma warning disable CA1707 // Remove the underscores from member name
public sealed class ResultTelemetryExtensionsTests : IDisposable
{
    private const string TestSourceName = "Test.Telemetry.ResultExtensions";
    private readonly ActivitySource _testSource;
    private readonly ActivityListener _listener;

    public ResultTelemetryExtensionsTests()
    {
        // Listener must be registered before the source is created
        _listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == TestSourceName,
            Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded
        };
        ActivitySource.AddActivityListener(_listener);
        _testSource = new ActivitySource(TestSourceName);
    }

    public void Dispose()
    {
        _testSource.Dispose();
        _listener.Dispose();
    }

    [Fact]
    public void TapActivity_OnSuccess_SetsActivityStatusOk()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var result = Result<int>.Success(42);

        // Act
        var returned = result.TapActivity(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Ok, activity.Status);
        Assert.False(returned.IsFailure);
        Assert.Equal(42, returned.Value);
    }

    [Fact]
    public void TapActivity_OnFailure_SetsActivityStatusError()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var error = new Error { Key = nameof(Error.NotFound), StatusCode = HttpStatusCode.NotFound };
        var result = Result<int>.Failure(error);

        // Act
        var returned = result.TapActivity(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Error, activity.Status);
        Assert.Equal(nameof(Error.NotFound), activity.StatusDescription);
        Assert.True(returned.IsFailure);
    }

    [Fact]
    public void TapActivity_WithNullActivity_ReturnsResultUnchanged()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var returned = result.TapActivity(null);

        // Assert
        Assert.False(returned.IsFailure);
        Assert.Equal(42, returned.Value);
    }

    [Fact]
    public void TapActivity_OnFailure_SetsErrorTypeTag()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var error = new Error { Key = nameof(Error.ViolatesUniqueConstraint), StatusCode = HttpStatusCode.Conflict };
        var result = Result<string>.Failure(error);

        // Act
        result.TapActivity(activity);

        // Assert
        var errorTag = activity.GetTagItem("error.type");
        Assert.Equal(nameof(Error.ViolatesUniqueConstraint), errorTag);
    }

    [Fact]
    public void NonGenericTapActivity_OnSuccess_SetsActivityStatusOk()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var result = Result.Success;

        // Act
        var returned = result.TapActivity(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Ok, activity.Status);
        Assert.False(returned.IsFailure);
    }

    [Fact]
    public void NonGenericTapActivity_OnFailure_SetsActivityStatusError()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var error = new Error { Key = nameof(Error.NotFound), StatusCode = HttpStatusCode.NotFound };
        var result = Result.Failure(error);

        // Act
        var returned = result.TapActivity(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Error, activity.Status);
        Assert.True(returned.IsFailure);
    }

    [Fact]
    public async Task TapActivityAsync_OnSuccess_SetsActivityStatusOk()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var resultTask = Task.FromResult(Result<int>.Success(42));

        // Act
        var returned = await resultTask.TapActivityAsync(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Ok, activity.Status);
        Assert.Equal(42, returned.Value);
    }

    [Fact]
    public async Task TapActivityAsync_NonGeneric_OnSuccess_SetsActivityStatusOk()
    {
        // Arrange
        using var activity = _testSource.StartActivity("TestOperation");
        Assert.NotNull(activity);
        var resultTask = Task.FromResult(Result.Success);

        // Act
        var returned = await resultTask.TapActivityAsync(activity);

        // Assert
        Assert.Equal(ActivityStatusCode.Ok, activity.Status);
        Assert.False(returned.IsFailure);
    }

    [Fact]
    public void StartActivityForCaller_UsesCallerMemberName()
    {
        // Act
        using var activity = _testSource.StartActivityForCaller();

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(nameof(StartActivityForCaller_UsesCallerMemberName), activity.OperationName);
    }
}
#pragma warning restore CA1707
