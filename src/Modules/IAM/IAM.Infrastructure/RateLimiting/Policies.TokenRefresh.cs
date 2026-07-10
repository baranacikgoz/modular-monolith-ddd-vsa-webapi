using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.RateLimiting;

public static partial class Policies
{
    // internal (not private): unit-tested directly from IAM.Tests via InternalsVisibleTo, since
    // AddFixedWindowLimiter's single shared bucket previously rate-limited /tokens/refresh globally
    // across every caller — one client could exhaust it and lock out all other users' refreshes.
    internal sealed class TokenRefreshRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null → falls through to global OnRejected set in GlobalOnRejected()
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partition = httpContext.GetIpAddress() ?? "unknown";
            var opts = rateLimitingOptionsProvider.Value.TokenRefresh;

            return RateLimitPartition.GetFixedWindowLimiter(partition,
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = opts.Limit,
                    Window = TimeSpan.FromMilliseconds(opts.PeriodInMs),
                    QueueLimit = opts.QueueLimit,
                });
        }
    }
}
