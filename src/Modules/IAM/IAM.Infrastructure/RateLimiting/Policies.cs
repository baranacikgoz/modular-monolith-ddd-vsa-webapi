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

    private static void TokenCreatePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        // Per-IP partitioned (not AddFixedWindowLimiter): that would be a single bucket shared by every
        // caller, so one client exhausting it would 429 login for every user — see TokenCreateRateLimitingPolicy.
        rateLimiter.AddPolicy<string, TokenCreateRateLimitingPolicy>(Constants.TokenCreate);
    }

    private static void CheckRegistrationPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        // Per-IP partitioned (not AddFixedWindowLimiter): that would be a single bucket shared by every
        // caller — see CheckRegistrationRateLimitingPolicy.
        rateLimiter.AddPolicy<string, CheckRegistrationRateLimitingPolicy>(Constants.CheckRegistration);
    }

    private static void TokenRefreshPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        // Per-IP partitioned (not AddFixedWindowLimiter): that would be a single bucket shared by every
        // caller, so one client exhausting it would 429 every user's refresh — see TokenRefreshRateLimitingPolicy.
        rateLimiter.AddPolicy<string, TokenRefreshRateLimitingPolicy>(Constants.TokenRefresh);
    }
}
