using System.Net;
using Common.Application.Options;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace IAM.Tests.Tokens;

// Regression coverage for the bug where /users/check-registration used AddFixedWindowLimiter (ONE
// bucket shared by every caller): a single client could burn the whole limit and 429 every other user's
// check. The fix partitions per client IP, mirroring the already-correct Sms/Register/TokenRefresh policies.
public class CheckRegistrationRateLimitingPolicyTests
{
    private static readonly Policies.CheckRegistrationRateLimitingPolicy _sut = new(Options.Create(new CustomRateLimitingOptions
    {
        Global = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Sms = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Register = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CreateStore = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        TokenCreate = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CheckRegistration = new FixedWindow { Limit = 20, PeriodInMs = 60000, QueueLimit = 0 },
        TokenRefresh = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 }
    }));

    private static readonly IServiceProvider EmptyServices = new ServiceCollection().BuildServiceProvider();

    private static DefaultHttpContext ContextWithIp(string? ip)
    {
        var context = new DefaultHttpContext { RequestServices = EmptyServices };
        if (ip is not null)
        {
            context.Connection.RemoteIpAddress = IPAddress.Parse(ip);
        }

        return context;
    }

    [Fact]
    public void GetPartition_TwoDifferentIps_ReturnsDifferentPartitionKeys()
    {
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
