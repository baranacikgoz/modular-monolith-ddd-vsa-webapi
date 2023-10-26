using Microsoft.Extensions.DependencyInjection;

namespace Common.Caching;

public static class Setup
{
    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache(); // will be replaced with Redis in production
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
