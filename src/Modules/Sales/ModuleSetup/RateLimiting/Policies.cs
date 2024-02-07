using Common.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace Sales.ModuleSetup.RateLimiting;

public static partial class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return CreateStorePolicy;
    }
}
