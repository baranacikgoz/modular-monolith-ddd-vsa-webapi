using System.Threading.RateLimiting;
using Common.Application.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Common.Infrastructure.RateLimiting;

/// <summary>
///     Single chokepoint every fixed-window rate limiting policy goes through. Falls back to the
///     in-process framework limiter when Redis isn't configured (dev/test, CachingOptions.UseRedis =
///     false); otherwise every replica shares one counter via <see cref="RedisFixedWindowRateLimiter"/>.
/// </summary>
public static class RateLimitPartitions
{
    public static RateLimitPartition<string> FixedWindow(
        HttpContext httpContext, string policyName, string partitionKey, FixedWindow options)
    {
        var redis = httpContext.RequestServices.GetService<IConnectionMultiplexer>();

        if (redis is null)
        {
            return RateLimitPartition.GetFixedWindowLimiter(partitionKey, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = options.Limit,
                Window = TimeSpan.FromMilliseconds(options.PeriodInMs),
                QueueLimit = options.QueueLimit,
            });
        }

        var logger = httpContext.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("Common.Infrastructure.RateLimiting.RedisFixedWindowRateLimiter");

        // Prefixed with the policy name: distinct policies must not share a bucket for the same IP,
        // unlike the in-process path where each policy already owns a separate PartitionedRateLimiter.
        var redisKey = $"ratelimit:{policyName}:{partitionKey}";

        return RateLimitPartition.Get(partitionKey, _ => new RedisFixedWindowRateLimiter(
            redis,
            redisKey,
            options.Limit,
            TimeSpan.FromMilliseconds(options.PeriodInMs),
            options.FailOpen,
            logger));
    }
}
