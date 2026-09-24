using Common.Endpoints.Webhooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Common.Tests.Webhooks;

public class RequestBodyLimitMiddlewareTests
{
    private sealed class BodySizeFeature : IHttpMaxRequestBodySizeFeature
    {
        public bool IsReadOnly { get; init; }
        public long? MaxRequestBodySize { get; set; }
    }

    private static async Task<(HttpContext Context, bool NextCalled, BodySizeFeature Feature)> RunAsync(
        long? contentLength, Endpoint? endpoint)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.ContentLength = contentLength;
        var feature = new BodySizeFeature { MaxRequestBodySize = 30_000_000 };
        httpContext.Features.Set<IHttpMaxRequestBodySizeFeature>(feature);
        httpContext.SetEndpoint(endpoint);

        var nextCalled = false;
        var middleware = new RequestBodyLimitMiddleware(_ =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(httpContext);
        return (httpContext, nextCalled, feature);
    }

    private static Endpoint LimitedEndpoint(long limit) =>
        new(_ => Task.CompletedTask, new EndpointMetadataCollection(new RequestBodyLimitMetadata(_ => limit)), "limited");

    [Fact]
    public async Task InvokeAsync_DeclaredLengthOverLimit_Returns413WithoutReachingTheEndpoint()
    {
        var (context, nextCalled, _) = await RunAsync(contentLength: 2_000, LimitedEndpoint(1_000));

        Assert.Equal(StatusCodes.Status413PayloadTooLarge, context.Response.StatusCode);
        Assert.False(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_WithinLimit_CapsTheServerBeforeTheEndpointBinds()
    {
        var (_, nextCalled, feature) = await RunAsync(contentLength: 500, LimitedEndpoint(1_000));

        Assert.True(nextCalled);
        Assert.Equal(1_000, feature.MaxRequestBodySize);
    }

    [Fact]
    public async Task InvokeAsync_ChunkedBody_StillCapsTheServer()
    {
        var (_, nextCalled, feature) = await RunAsync(contentLength: null, LimitedEndpoint(1_000));

        Assert.True(nextCalled);
        Assert.Equal(1_000, feature.MaxRequestBodySize);
    }

    [Fact]
    public async Task InvokeAsync_EndpointWithoutMetadata_LeavesTheServerLimitAlone()
    {
        var plain = new Endpoint(_ => Task.CompletedTask, EndpointMetadataCollection.Empty, "plain");

        var (_, nextCalled, feature) = await RunAsync(contentLength: 2_000, plain);

        Assert.True(nextCalled);
        Assert.Equal(30_000_000, feature.MaxRequestBodySize);
    }

    [Fact]
    public void LimitRequestBody_AddsMetadataTheMiddlewareReads()
    {
        var builder = WebApplication.CreateSlimBuilder();
        builder.Services.AddRouting();
        using var app = builder.Build();

        app.MapPost("/hook", () => Results.NoContent()).LimitRequestBody(_ => 1_000);

        var endpoint = ((IEndpointRouteBuilder)app).DataSources.SelectMany(s => s.Endpoints).Single();
        Assert.NotNull(endpoint.Metadata.GetMetadata<RequestBodyLimitMetadata>());
    }
}
