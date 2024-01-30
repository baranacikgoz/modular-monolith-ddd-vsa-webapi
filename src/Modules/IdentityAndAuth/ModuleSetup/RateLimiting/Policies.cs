using Common.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace IdentityAndAuth.ModuleSetup.RateLimiting;

public static class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return SmsPolicy;
    }

    private static void SmsPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
        => rateLimiter
            .AddFixedWindowLimiter(RateLimiting.Constants.Sms, opt =>
            {
                var smsRateLimiting = options.Sms ?? throw new InvalidOperationException("Sms rate limiting is null.");
                var permitLimit = smsRateLimiting.Limit;
                var periodInMs = smsRateLimiting.PeriodInMs;

                opt.PermitLimit = permitLimit;
                opt.Window = TimeSpan.FromMilliseconds(periodInMs);
                opt.QueueLimit = smsRateLimiting.QueueLimit;
            });
}
