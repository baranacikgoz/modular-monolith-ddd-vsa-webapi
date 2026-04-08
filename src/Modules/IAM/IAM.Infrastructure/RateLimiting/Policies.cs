using System.Globalization;
using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace IAM.Infrastructure.RateLimiting;

public static class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return SmsPolicy;
    }

    private static void SmsPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddFixedWindowLimiter(Constants.Sms, opt =>
            {
                var smsRateLimiting = options.Sms ?? throw new InvalidOperationException("Sms rate limiting is null.");

                opt.PermitLimit = smsRateLimiting.Limit;
                opt.Window = TimeSpan.FromMilliseconds(smsRateLimiting.PeriodInMs);
                opt.QueueLimit = smsRateLimiting.QueueLimit;
            });

        // Expose Retry-After so mobile clients know exactly when to retry.
        rateLimiter.OnRejected = static (context, _) =>
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                context.HttpContext.Response.Headers.RetryAfter =
                    ((int)retryAfter.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            }

            return ValueTask.CompletedTask;
        };
    }
}
