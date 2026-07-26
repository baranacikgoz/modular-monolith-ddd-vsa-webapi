using Common.Infrastructure.RateLimiting;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using StackExchange.Redis;
using Xunit;

namespace Common.Tests.RateLimiting;

// Only the Redis-free logic paths — constructor validation and the synchronous AttemptAcquireCore, which
// never touches Redis by design (see the comment on that method). The Lua-backed AcquireAsyncCore path
// needs a real Redis instance and, consistent with RedisOtpService (Notifications.Infrastructure), is
// verified manually rather than via a container in CI.
public class RedisFixedWindowRateLimiterTests
{
    private static RedisFixedWindowRateLimiter CreateSut(int permitLimit = 5, TimeSpan? window = null, bool failOpen = true) =>
        new(Substitute.For<IConnectionMultiplexer>(), "test-key", permitLimit, window ?? TimeSpan.FromMinutes(1),
            failOpen, NullLogger.Instance);

    [Fact]
    public void Constructor_ZeroPermitLimit_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateSut(permitLimit: 0));
    }

    [Fact]
    public void Constructor_NegativePermitLimit_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateSut(permitLimit: -1));
    }

    [Fact]
    public void Constructor_ZeroWindow_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateSut(window: TimeSpan.Zero));
    }

    [Fact]
    public void Constructor_NegativeWindow_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateSut(window: TimeSpan.FromSeconds(-1)));
    }

    [Fact]
    public void AttemptAcquire_AlwaysFails_NeverTouchesRedis()
    {
        // The redis mock has no configured behavior — if AttemptAcquire touched it, NSubstitute
        // would return a default/null RedisResult and this would throw or hang, not just fail cleanly.
        using var sut = CreateSut();

        var lease = sut.AttemptAcquire(1);

        Assert.False(lease.IsAcquired);
    }
}
