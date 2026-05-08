using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Infrastructure.Caching;

public static class Setup
{
    public static IServiceCollection AddCommonCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CachingOptions>(configuration.GetSection(nameof(CachingOptions)));

        var cachingOptions = configuration
                                 .GetSection(nameof(CachingOptions))
                                 .Get<CachingOptions>()
                             ?? throw new InvalidOperationException(
                                 $"Configuration for {nameof(CachingOptions)} is null.");

        var defaults = cachingOptions.EntryDefaults;
        var builder = services
            .AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = defaults.Duration,
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = defaults.FailSafeMaxDuration,
                FailSafeThrottleDuration = defaults.FailSafeThrottleDuration,
                FactorySoftTimeout = defaults.FactorySoftTimeout,
                FactoryHardTimeout = defaults.FactoryHardTimeout,
            })
            .WithSystemTextJsonSerializer();

        if (cachingOptions.UseRedis)
        {
            if (cachingOptions.Redis is null)
            {
                throw new InvalidOperationException($"Configuration for {nameof(CachingOptions.Redis)} is null.");
            }

            var connectionString =
                $"{cachingOptions.Redis.Host}:{cachingOptions.Redis.Port},password={cachingOptions.Redis.Password}";

            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = connectionString;
                o.InstanceName = cachingOptions.Redis.AppName;
            });

            // AddStackExchangeRedisCache registers IDistributedCache; FusionCache auto-discovers it as L2.
            builder.WithStackExchangeRedisBackplane(o => o.Configuration = connectionString);
        }

        return services;
    }
}
