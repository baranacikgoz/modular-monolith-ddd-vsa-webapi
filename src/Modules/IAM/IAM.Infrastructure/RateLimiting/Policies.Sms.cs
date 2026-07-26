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
    private sealed class SmsRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null → falls through to global OnRejected set in GlobalOnRejected()
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partitionKey = httpContext.GetIpAddress() ?? "unknown";

            return RateLimitPartitions.FixedWindow(
                httpContext, Constants.Sms, partitionKey, rateLimitingOptionsProvider.Value.Sms);
        }
    }
}
