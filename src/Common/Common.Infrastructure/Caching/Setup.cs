using Common.Application.Caching;
using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Caching;

public static class Setup
{
    public static IServiceCollection AddCommonCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var cachingOptions = configuration
                                 .GetSection(nameof(CachingOptions))
                                 .Get<CachingOptions>()
                             ?? throw new InvalidOperationException(
                                 $"Configuration for {nameof(CachingOptions)} is null.");

        if (cachingOptions.UseRedis)
        {
            if (cachingOptions.Redis is null)
            {
                throw new InvalidOperationException($"Configuration for {nameof(CachingOptions.Redis)} is null.");
            }

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =
                    $"{cachingOptions.Redis.Host}:{cachingOptions.Redis.Port},password={cachingOptions.Redis.Password}";
            });
        }

        // Microsoft made an interesting decision; this HybridCache automatically uses Redis when it has been registered like above.
        // So, do not change the order, StackExchangeRedisCache must be registered before HybridCache.
        services.AddHybridCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
