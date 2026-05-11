using System.Diagnostics;
using Common.Application.Auth;
using Common.Application.FeatureManagement;
using Common.Application.Localization.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;
using NSubstitute;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515
#pragma warning disable CA1707
public sealed class RequireFeatureFilterTests : IDisposable
{
    private const string FeatureName = "Products.NewCheckout";
    private readonly IFeatureManagerSnapshot _featureManager = Substitute.For<IFeatureManagerSnapshot>();
    private readonly IResxLocalizer _localizer = Substitute.For<IResxLocalizer>();
    private readonly ActivityListener _listener;
    private readonly List<Activity> _recordedActivities = [];

    public RequireFeatureFilterTests()
    {
        _listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "ModularMonolith.FeatureManagement",
            Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStarted = activity => _recordedActivities.Add(activity)
        };
        ActivitySource.AddActivityListener(_listener);

        _localizer.FeatureNotAvailableTitle.Returns("Feature not available");
        _localizer.FeatureNotAvailable.Returns("Feature ({0}) is not available.");
    }

    public void Dispose()
    {
        _listener.Dispose();
    }

    [Fact]
    public async Task InvokeAsync_FeatureEnabled_ReturnsNextResult()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(true);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        var expected = new { Value = 42 };
        var result = await InvokeFilter(context, () => expected);

        Assert.Same(expected, result);
    }

    [Fact]
    public async Task InvokeAsync_FeatureDisabled_ReturnsProblemResult()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(false);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        var result = await InvokeFilter(context, () => "pass-through");

        var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_FeatureDisabled_EmitsActivityWithErrorStatus()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(false);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        await InvokeFilter(context, () => "pass-through");

        var activity = _recordedActivities.SingleOrDefault();
        Assert.NotNull(activity);
        Assert.Equal("FeatureEvaluation", activity.OperationName);
        Assert.Equal(ActivityStatusCode.Error, activity.Status);
        Assert.Equal("FeatureDisabled", activity.StatusDescription);
        Assert.Equal(FeatureName, activity.GetTagItem("feature.name"));
        Assert.False((bool)activity.GetTagItem("feature.enabled")!);
    }

    [Fact]
    public async Task InvokeAsync_FeatureEnabled_EmitsActivityWithOkStatus()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(true);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        await InvokeFilter(context, () => "pass-through");

        var activity = _recordedActivities.SingleOrDefault();
        Assert.NotNull(activity);
        Assert.Equal(ActivityStatusCode.Ok, activity.Status);
        Assert.True((bool)activity.GetTagItem("feature.enabled")!);
    }

    [Fact]
    public async Task InvokeAsync_WithCurrentUser_SetsUserIdTag()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(true);

        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.IdAsString.Returns("user-456");
        serviceProvider.GetService(typeof(ICurrentUser)).Returns(currentUser);

        await InvokeFilter(context, () => "pass-through");

        var activity = _recordedActivities.SingleOrDefault();
        Assert.NotNull(activity);
        Assert.Equal("user-456", activity.GetTagItem("user.id"));
    }

    [Fact]
    public async Task InvokeAsync_WithoutCurrentUser_DoesNotSetUserIdTag()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(true);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        await InvokeFilter(context, () => "pass-through");

        var activity = _recordedActivities.SingleOrDefault();
        Assert.NotNull(activity);
        Assert.Null(activity.GetTagItem("user.id"));
    }

    [Fact]
    public async Task InvokeAsync_FeatureDisabled_UsesLocalizedMessages()
    {
        var (context, serviceProvider) = CreateFilterContext();
        _featureManager.IsEnabledAsync(FeatureName, Arg.Any<CancellationToken>()).Returns(false);
        serviceProvider.GetService(typeof(ICurrentUser)).Returns((ICurrentUser?)null);

        var result = await InvokeFilter(context, () => "pass-through");

        _ = _localizer.Received(1).FeatureNotAvailableTitle;
        _ = _localizer.Received(1).FeatureNotAvailable;
        Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
    }

    private static async Task<object?> InvokeFilter(EndpointFilterInvocationContext context, Func<object?> getResult)
    {
        var filter = CreateFilter();
        EndpointFilterDelegate next = _ => ValueTask.FromResult(getResult());
        return await filter.InvokeAsync(context, next);
    }

    private static RequireFeatureFilter CreateFilter()
    {
        return new RequireFeatureFilter(FeatureName);
    }

    private (EndpointFilterInvocationContext context, IServiceProvider serviceProvider) CreateFilterContext()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(IFeatureManagerSnapshot)).Returns(_featureManager);
        serviceProvider.GetService(typeof(IResxLocalizer)).Returns(_localizer);

        var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
        httpContext.Request.Method = "GET";
        httpContext.Request.Path = "/test/products";

        var invocationContext = new TestEndpointFilterInvocationContext(httpContext);
        return (invocationContext, serviceProvider);
    }

    private sealed class TestEndpointFilterInvocationContext(HttpContext httpContext) : EndpointFilterInvocationContext
    {
        public override HttpContext HttpContext { get; } = httpContext;
        public override IList<object?> Arguments { get; } = [];
        public override T GetArgument<T>(int index) => throw new NotSupportedException();
    }
}
#pragma warning restore CA1707
