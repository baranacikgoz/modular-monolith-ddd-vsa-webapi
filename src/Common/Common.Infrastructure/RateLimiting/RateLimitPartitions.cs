using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
    /// <summary>
    ///     Same limiter as <see cref="FixedWindow" />, partitioned by the route value <paramref name="routeKey" /> (a
    ///     webhook's account id, a per-tenant callback path) so each caller identified by the URL gets its own bucket.
    ///     Falls back to the client IP when the route value is missing, and the key is prefixed with the route key so a
    ///     route-value bucket can never collide with an IP bucket of the same policy.
    ///     <para>
    ///         The route value is caller-supplied, so this is fairness between legitimate callers, not abuse protection: a
    ///         client that sprays random values gets a fresh bucket per value. The ceiling for that is the global limiter,
    ///         which buckets anonymous traffic per IP, so never list a route using this policy in
    ///         <c>CustomRateLimitingOptions.ExemptPathPrefixes</c>.
    ///     </para>
    /// </summary>
    public static RateLimitPartition<string> FixedWindowByRouteValue(
        HttpContext httpContext, string policyName, string routeKey, FixedWindow options)
    {
        var routeValue = httpContext.GetRouteValue(routeKey)?.ToString();
        var partitionKey = string.IsNullOrEmpty(routeValue)
            ? $"ip:{httpContext.GetIpAddress() ?? "unknown"}"
            : $"{routeKey}:{routeValue}";

        return FixedWindow(httpContext, policyName, partitionKey, options);
    }

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
                QueueLimit = options.QueueLimit!.Value,
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
            options.FailOpen!.Value,
            logger));
    }
}
