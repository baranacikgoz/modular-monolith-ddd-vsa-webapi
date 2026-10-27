using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Products.Infrastructure.RateLimiting;

public static partial class Policies
{
    private static void StoreApplicationPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        rateLimiter.AddPolicy<string, StoreApplicationRateLimitingPolicy>(RateLimitingConstants.StoreApplication);
    }

    // internal (not private): unit-tested directly from Products.Tests via InternalsVisibleTo.
    internal sealed class StoreApplicationRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null: falls through to the global OnRejected handler.
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        // The caller is anonymous, so the client IP is the only key there is.
        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partitionKey = httpContext.GetIpAddress() ?? "unknown";

            return RateLimitPartitions.FixedWindow(
                httpContext, RateLimitingConstants.StoreApplication, partitionKey, rateLimitingOptionsProvider.Value.StoreApplication);
        }
    }
}
