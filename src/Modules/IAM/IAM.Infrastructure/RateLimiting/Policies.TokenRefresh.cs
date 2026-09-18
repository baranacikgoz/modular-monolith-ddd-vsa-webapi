using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.RateLimiting;

public static partial class Policies
{
    // internal (not private): unit-tested directly from IAM.Tests via InternalsVisibleTo, since
    // AddFixedWindowLimiter's single shared bucket previously rate-limited /tokens/refresh globally
    // across every caller: one client could exhaust it and lock out all other users' refreshes.
    internal sealed class TokenRefreshRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null → falls through to global OnRejected set in GlobalOnRejected()
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partitionKey = httpContext.GetIpAddress() ?? "unknown";

            return RateLimitPartitions.FixedWindow(
                httpContext, Constants.TokenRefresh, partitionKey, rateLimitingOptionsProvider.Value.TokenRefresh);
        }
    }
}
