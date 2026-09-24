using Common.Endpoints.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Xunit;

namespace Common.Tests.Webhooks;

public class RequestBodyLimitFilterTests
{
    private sealed class BodySizeFeature : IHttpMaxRequestBodySizeFeature
    {
        public bool IsReadOnly { get; init; }
        public long? MaxRequestBodySize { get; set; }
    }

    private static async Task<(object? Result, bool NextCalled, BodySizeFeature Feature)> RunAsync(long? contentLength, long limit)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.ContentLength = contentLength;
        var feature = new BodySizeFeature();
        httpContext.Features.Set<IHttpMaxRequestBodySizeFeature>(feature);

        var nextCalled = false;
        var filter = new RequestBodyLimitFilter(_ => limit);
        var result = await filter.InvokeAsync(
            new DefaultEndpointFilterInvocationContext(httpContext),
            _ =>
            {
                nextCalled = true;
                return ValueTask.FromResult<object?>(Results.NoContent());
            });

        return (result, nextCalled, feature);
    }

    [Fact]
    public async Task InvokeAsync_DeclaredLengthOverLimit_Returns413WithoutInvokingEndpoint()
    {
        var (result, nextCalled, _) = await RunAsync(contentLength: 2_000, limit: 1_000);

        var statusResult = Assert.IsType<IStatusCodeHttpResult>(result, exactMatch: false);
        Assert.Equal(StatusCodes.Status413PayloadTooLarge, statusResult.StatusCode);
        Assert.False(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_WithinLimit_SetsServerCapAndContinues()
    {
        var (_, nextCalled, feature) = await RunAsync(contentLength: 500, limit: 1_000);

        Assert.True(nextCalled);
        Assert.Equal(1_000, feature.MaxRequestBodySize);
    }

    [Fact]
    public async Task InvokeAsync_ChunkedBody_StillCapsTheServerLimit()
    {
        var (_, nextCalled, feature) = await RunAsync(contentLength: null, limit: 1_000);

        Assert.True(nextCalled);
        Assert.Equal(1_000, feature.MaxRequestBodySize);
    }
}
