using System.Threading.RateLimiting;
using Common.Application.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace IAM.Infrastructure.RateLimiting;

public static partial class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return SmsPolicy;
        yield return RegisterPolicy;
        yield return TokenCreatePolicy;
        yield return CheckRegistrationPolicy;
        yield return TokenRefreshPolicy;
    }

    private static void SmsPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        rateLimiter.AddPolicy<string, SmsRateLimitingPolicy>(Constants.Sms);
    }

    private static void RegisterPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        rateLimiter.AddPolicy<string, RegisterRateLimitingPolicy>(Constants.Register);
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
