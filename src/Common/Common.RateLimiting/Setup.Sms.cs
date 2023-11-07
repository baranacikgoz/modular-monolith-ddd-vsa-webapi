using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.RateLimiting;

public static partial class Setup
{
    private static RateLimiterOptions ApplySmsPolicy(this RateLimiterOptions rateLimiter, RateLimitingOptions options)
        => rateLimiter
            .AddFixedWindowLimiter(RateLimitingPolicies.Sms, opt =>
            {
                var smsRateLimiting = options.Sms ?? throw new InvalidOperationException("Sms rate limiting is null.");
                var permitLimit = smsRateLimiting.Limit;
                var periodInMs = smsRateLimiting.PeriodInMs;

                opt.PermitLimit = permitLimit;
                opt.Window = TimeSpan.FromMilliseconds(periodInMs);

                if (smsRateLimiting.HasQueueLimit)
                {
                    opt.QueueLimit = smsRateLimiting.QueueLimit!.Value;
                }
            });
}
