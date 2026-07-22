using Common.Application.EndpointFilters;
using Common.Application.Localization.Resources;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using NSubstitute;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515
#pragma warning disable CA1707
public sealed class ResultToResponseTransformerTests
{
    // Reproduces: an inner filter (e.g. RequireFeatureFilter, or minimal-API's own
    // antiforgery/short-circuit path) returns an ASP.NET IResult directly instead of
    // calling its own `next`. That IResult bubbles up through `next(context)` here.
    // Before the fix this hit the "resultObj is not Result<T>" guard and threw,
    // turning an already-correct framework response (e.g. 404 from a disabled
    // feature flag) into an unhandled 500.
    [Fact]
    public async Task ResultToResponseTransformer_UpstreamShortCircuit_ReturnsAspNetResultUnchanged()
    {
        var filter = new ResultToResponseTransformer(Substitute.For<IServiceProvider>(), Substitute.For<IWebHostEnvironment>());
        var shortCircuit = Results.NotFound();
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>(shortCircuit);

        var result = await filter.InvokeAsync(CreateContext(), next);

        Assert.Same(shortCircuit, result);
    }

    [Fact]
    public async Task ResultToResponseTransformerOfT_UpstreamShortCircuit_ReturnsAspNetResultUnchanged()
    {
        var filter = new ResultToResponseTransformer<string>(Substitute.For<IServiceProvider>(), Substitute.For<IWebHostEnvironment>());
        var shortCircuit = Results.NotFound();
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>(shortCircuit);

        var result = await filter.InvokeAsync(CreateContext(), next);

        Assert.Same(shortCircuit, result);
    }

    [Fact]
    public async Task ResultToCreatedResponseTransformerOfT_UpstreamShortCircuit_ReturnsAspNetResultUnchanged()
    {
        var filter = new ResultToCreatedResponseTransformer<string>(Substitute.For<IServiceProvider>(), Substitute.For<IWebHostEnvironment>());
        var shortCircuit = Results.NotFound();
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>(shortCircuit);

        var result = await filter.InvokeAsync(CreateContext(), next);

        Assert.Same(shortCircuit, result);
    }

    // A handler wired to the wrong transformer (e.g. returning a plain string instead
    // of Result<T>) is genuine developer misuse, not an upstream short-circuit, and
    // must keep failing loudly.
    [Fact]
    public async Task ResultToResponseTransformerOfT_NonResultNonAspNetResultObject_Throws()
    {
        var filter = new ResultToResponseTransformer<string>(Substitute.For<IServiceProvider>(), Substitute.For<IWebHostEnvironment>());
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>("not a Result");

        await Assert.ThrowsAsync<InvalidOperationException>(() => filter.InvokeAsync(CreateContext(), next).AsTask());
    }

    [Fact]
    public async Task ResultToResponseTransformerOfT_SuccessResult_ReturnsOkWithValue()
    {
        var filter = new ResultToResponseTransformer<string>(Substitute.For<IServiceProvider>(), Substitute.For<IWebHostEnvironment>());
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>(Result<string>.Success("value"));

        var result = await filter.InvokeAsync(CreateContext(), next);

        var okResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task ResultToResponseTransformerOfT_FailureResult_UsesLocalizerAndReturnsProblem()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        var localizer = Substitute.For<IStringLocalizer<ResxLocalizer>>();
        localizer[Arg.Any<string>()].Returns(new LocalizedString("key", "Not Found"));
        serviceProvider.GetService(typeof(IStringLocalizer<ResxLocalizer>)).Returns(localizer);

        var filter = new ResultToResponseTransformer<string>(serviceProvider, Substitute.For<IWebHostEnvironment>());
        var error = Error.Validation([]);
        EndpointFilterDelegate next = _ => ValueTask.FromResult<object?>(Result<string>.Failure(error));

        var result = await filter.InvokeAsync(CreateContext(), next);

        var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
    }

    private static TestEndpointFilterInvocationContext CreateContext()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = "GET";
        httpContext.Request.Path = "/test/resource";
        return new TestEndpointFilterInvocationContext(httpContext);
    }

    private sealed class TestEndpointFilterInvocationContext(HttpContext httpContext) : EndpointFilterInvocationContext
    {
        public override HttpContext HttpContext { get; } = httpContext;
        public override IList<object?> Arguments { get; } = [];
        public override T GetArgument<T>(int index) => throw new NotSupportedException();
    }
}
#pragma warning restore CA1707
