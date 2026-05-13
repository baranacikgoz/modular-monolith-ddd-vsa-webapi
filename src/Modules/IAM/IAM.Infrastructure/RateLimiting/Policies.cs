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
        yield return RegisterPolicy;
        yield return TokenCreatePolicy;
        yield return CheckRegistrationPolicy;
        yield return TokenRefreshPolicy;
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

    private static void RegisterPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddFixedWindowLimiter(Constants.Register, opt =>
            {
                var registerRateLimiting = options.Register ?? throw new InvalidOperationException("Register rate limiting is null.");

                opt.PermitLimit = registerRateLimiting.Limit;
                opt.Window = TimeSpan.FromMilliseconds(registerRateLimiting.PeriodInMs);
                opt.QueueLimit = registerRateLimiting.QueueLimit;
            });
    }

    private static void TokenCreatePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddFixedWindowLimiter(Constants.TokenCreate, opt =>
            {
                var tokenCreate = options.TokenCreate ?? throw new InvalidOperationException("TokenCreate rate limiting is null.");

                opt.PermitLimit = tokenCreate.Limit;
                opt.Window = TimeSpan.FromMilliseconds(tokenCreate.PeriodInMs);
                opt.QueueLimit = tokenCreate.QueueLimit;
            });
    }

    private static void CheckRegistrationPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddFixedWindowLimiter(Constants.CheckRegistration, opt =>
            {
                var checkRegistration = options.CheckRegistration ?? throw new InvalidOperationException("CheckRegistration rate limiting is null.");

                opt.PermitLimit = checkRegistration.Limit;
                opt.Window = TimeSpan.FromMilliseconds(checkRegistration.PeriodInMs);
                opt.QueueLimit = checkRegistration.QueueLimit;
            });
    }

    private static void TokenRefreshPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddFixedWindowLimiter(Constants.TokenRefresh, opt =>
            {
                var tokenRefresh = options.TokenRefresh ?? throw new InvalidOperationException("TokenRefresh rate limiting is null.");

                opt.PermitLimit = tokenRefresh.Limit;
                opt.Window = TimeSpan.FromMilliseconds(tokenRefresh.PeriodInMs);
                opt.QueueLimit = tokenRefresh.QueueLimit;
            });
    }
}
