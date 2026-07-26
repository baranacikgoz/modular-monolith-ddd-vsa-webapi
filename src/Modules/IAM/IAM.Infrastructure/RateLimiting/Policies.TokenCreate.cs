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
    // internal (not private): unit-tested directly from IAM.Tests via InternalsVisibleTo, mirroring the
    // TokenRefresh fix — AddFixedWindowLimiter is a single bucket shared by every caller, so one client
    // exhausting it 429s login for every user.
    internal sealed class TokenCreateRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null → falls through to global OnRejected set in GlobalOnRejected()
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partitionKey = httpContext.GetIpAddress() ?? "unknown";

            return RateLimitPartitions.FixedWindow(
                httpContext, Constants.TokenCreate, partitionKey, rateLimitingOptionsProvider.Value.TokenCreate);
        }
    }
}
