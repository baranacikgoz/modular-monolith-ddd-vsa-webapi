using Common.Infrastructure.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace Inventory.Infrastructure.RateLimiting;

public static partial class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return CreateStorePolicy;
    }
}
