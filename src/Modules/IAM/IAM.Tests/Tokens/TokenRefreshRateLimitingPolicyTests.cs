using System.Net;
using Common.Application.Options;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace IAM.Tests.Tokens;

// Regression coverage for the bug where /tokens/refresh used AddFixedWindowLimiter (ONE bucket shared
// by every caller): a single client could burn the whole limit and 429 every other user's refresh.
// The fix partitions per client IP, mirroring the already-correct Sms/Register policies.
public class TokenRefreshRateLimitingPolicyTests
{
    private static readonly Policies.TokenRefreshRateLimitingPolicy _sut = new(Options.Create(new CustomRateLimitingOptions
    {
        Global = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Sms = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Register = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CreateStore = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        TokenCreate = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CheckRegistration = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        TokenRefresh = new FixedWindow { Limit = 20, PeriodInMs = 60000, QueueLimit = 0 },

        Email = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },

        OtpVerify = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 }
    }));

    // RateLimitPartitions.FixedWindow resolves IConnectionMultiplexer from RequestServices to decide
    // Redis-backed vs in-process; an empty provider means "not registered", exercising the same
    // in-process fallback these tests exercised before Redis-backed limiters existed.
    private static readonly IServiceProvider EmptyServices = new ServiceCollection().BuildServiceProvider();

    private static DefaultHttpContext ContextWithIp(string? ip)
    {
        var context = new DefaultHttpContext { RequestServices = EmptyServices };
        if (ip is not null)
        {
            // GetIpAddress() only trusts Connection.RemoteIpAddress (as rewritten by
            // UseForwardedHeaders() for trusted proxies) — not the raw X-Forwarded-For header.
            context.Connection.RemoteIpAddress = IPAddress.Parse(ip);
        }

        return context;
    }

    [Fact]
    public void GetPartition_TwoDifferentIps_ReturnsDifferentPartitionKeys()
    {
        // This is the crux of the fix: two different callers must land in two different buckets,
        // so one exhausting its limit cannot affect the other.
        var partitionA = _sut.GetPartition(ContextWithIp("1.2.3.4"));
        var partitionB = _sut.GetPartition(ContextWithIp("5.6.7.8"));

        Assert.NotEqual(partitionA.PartitionKey, partitionB.PartitionKey);
    }

    [Fact]
    public void GetPartition_SameIpTwice_ReturnsSamePartitionKey()
    {
        var first = _sut.GetPartition(ContextWithIp("9.9.9.9"));
        var second = _sut.GetPartition(ContextWithIp("9.9.9.9"));

        Assert.Equal(first.PartitionKey, second.PartitionKey);
    }

    [Fact]
    public void GetPartition_NoIpAvailable_FallsBackToUnknownPartition()
    {
        var partition = _sut.GetPartition(ContextWithIp(null));

        Assert.Equal("unknown", partition.PartitionKey);
    }
}
