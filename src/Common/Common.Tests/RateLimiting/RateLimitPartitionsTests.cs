using System.Net;
using Common.Application.Options;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Common.Tests.RateLimiting;

public class RateLimitPartitionsTests
{
    private static readonly FixedWindow Window = new() { Limit = 5, PeriodInMs = 60_000, QueueLimit = 0, FailOpen = true };

    [Fact]
    public void FixedWindowByRouteValue_RouteValuePresent_PartitionsByIt()
    {
        var httpContext = CreateHttpContext();
        httpContext.Request.RouteValues["accountId"] = "acct-42";

        var partition = RateLimitPartitions.FixedWindowByRouteValue(httpContext, "Webhook", "accountId", Window);

        Assert.Equal("accountId:acct-42", partition.PartitionKey);
    }

    [Fact]
    public void FixedWindowByRouteValue_RouteValueMissing_FallsBackToClientIp()
    {
        var httpContext = CreateHttpContext();
        httpContext.Connection.RemoteIpAddress = IPAddress.Parse("203.0.113.9");

        var partition = RateLimitPartitions.FixedWindowByRouteValue(httpContext, "Webhook", "accountId", Window);

        Assert.Equal("ip:203.0.113.9", partition.PartitionKey);
    }

    [Fact]
    public void FixedWindowByRouteValue_NoRouteValueAndNoIp_UsesUnknownBucket()
    {
        var httpContext = CreateHttpContext();

        var partition = RateLimitPartitions.FixedWindowByRouteValue(httpContext, "Webhook", "accountId", Window);

        Assert.Equal("ip:unknown", partition.PartitionKey);
    }

    [Fact]
    public void FixedWindowByRouteValue_WithoutRedis_BuildsInProcessLimiterHonoringTheLimit()
    {
        var httpContext = CreateHttpContext();
        httpContext.Request.RouteValues["accountId"] = "acct-1";

        var partition = RateLimitPartitions.FixedWindowByRouteValue(httpContext, "Webhook", "accountId",
            new FixedWindow { Limit = 1, PeriodInMs = 60_000, QueueLimit = 0, FailOpen = true });
        using var limiter = partition.Factory(partition.PartitionKey);

        using var first = limiter.AttemptAcquire();
        using var second = limiter.AttemptAcquire();
        Assert.True(first.IsAcquired);
        Assert.False(second.IsAcquired);
    }

    /// <summary>No IConnectionMultiplexer registered: the in-process limiter path (Redis off, as in dev/test).</summary>
    private static DefaultHttpContext CreateHttpContext()
    {
        return new DefaultHttpContext { RequestServices = new ServiceCollection().BuildServiceProvider() };
    }
}
