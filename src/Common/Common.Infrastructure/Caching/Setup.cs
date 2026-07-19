using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isProduction = string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase);

        if (isProduction && !cachingOptions.UseRedis && !cachingOptions.AllowInMemoryOnlyInProduction)
        {
            throw new InvalidOperationException(
                $"{nameof(CachingOptions)}.{nameof(CachingOptions.UseRedis)} is false in Production. " +
                "Multi-instance deployments require Redis for OTP storage, consumer idempotency, and the FusionCache backplane. " +
                $"Set {nameof(CachingOptions.AllowInMemoryOnlyInProduction)} = true only for single-instance deployments.");
        }

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

            var redis = cachingOptions.Redis;
            var configurationOptions = ConfigurationOptions.Parse($"{redis.Host}:{redis.Port}");
            configurationOptions.Password = redis.Password;
            // Boot even if Redis is briefly unreachable: the multiplexer reconnects in the background and
            // FusionCache's fail-safe serves from L1 in the meantime, so a Redis blip never takes the app down.
            configurationOptions.AbortOnConnectFail = false;

            // One shared multiplexer drives the L2 distributed cache, the FusionCache backplane, and the
            // Redis health check (resolved from DI in Setup.HealthChecks). Previously each spun up its own
            // multiplexer (~2 TCP connections apiece); now they share a single connection.
            var multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddStackExchangeRedisCache(o =>
            {
                o.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(multiplexer);
                o.InstanceName = redis.AppName;
            });

            // AddStackExchangeRedisCache registers IDistributedCache; FusionCache auto-discovers it as L2.
            builder.WithStackExchangeRedisBackplane(
                o => o.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(multiplexer));
        }

        return services;
    }
}
