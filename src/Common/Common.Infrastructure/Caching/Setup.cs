using Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Caching;

public static class Setup
{
    public static IServiceCollection AddCommonCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache(); // will be replaced with Redis in production
        services.AddSingleton<ICacheService, CacheService>(sp =>
        {
            var microsotDistributedCache = sp.GetRequiredService<IDistributedCache>();
            return new CacheService(microsotDistributedCache);
        });

        return services;
    }
}
