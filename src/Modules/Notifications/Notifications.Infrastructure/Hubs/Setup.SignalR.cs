using Common.Application.Extensions;
using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications.Application.Hubs;
using StackExchange.Redis;

namespace Notifications.Infrastructure.Hubs;

internal static class Setup
{
    internal static IServiceCollection AddNotificationsSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(SignalROptions)).Get<SignalROptions>();

        if (services.IsProductionEnvironment() && options?.UseRedisBackplane != true)
        {
            throw new InvalidOperationException(
                "SignalROptions.UseRedisBackplane is false in Production. " +
                "Multi-instance deployments require the Redis backplane for SignalR fan-out.");
        }

        var builder = services.AddSignalR();

        if (options?.UseRedisBackplane == true)
        {
            builder.AddStackExchangeRedis(_ => { }); // options configured below with DI access

            services.AddSingleton<IConfigureOptions<Microsoft.AspNetCore.SignalR.StackExchangeRedis.RedisOptions>>(sp =>
                new ConfigureOptions<Microsoft.AspNetCore.SignalR.StackExchangeRedis.RedisOptions>(redisOptions =>
                {
                    // Reuse the app-wide multiplexer registered by AddCommonCaching when Redis caching
                    // is on; otherwise dial the backplane's own connection string.
                    var shared = sp.GetService<IConnectionMultiplexer>();
                    redisOptions.ConnectionFactory = shared is not null
                        ? _ => Task.FromResult<IConnectionMultiplexer>(shared)
                        : async writer => await ConnectionMultiplexer.ConnectAsync(
                            options.RedisConnectionString, writer);
                }));
        }

        services.AddSingleton<INotificationDispatcher, SignalRNotificationDispatcher>();

        return services;
    }
}
