using Common.Application.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace Inventory.Infrastructure.RateLimiting;

public static partial class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return WarehouseWebhookPolicy;
    }

    private static void WarehouseWebhookPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions _)
    {
        rateLimiter.AddPolicy<string, WarehouseWebhookRateLimitingPolicy>(Constants.WarehouseWebhook);
    }
}
