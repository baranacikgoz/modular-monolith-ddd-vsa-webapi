using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Hubs;

namespace Notifications.Infrastructure.Hubs;

internal static class Setup
{
    internal static IServiceCollection AddNotificationsSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(SignalROptions)).Get<SignalROptions>();

        var builder = services.AddSignalR();

        if (options?.UseRedisBackplane == true)
        {
            builder.AddStackExchangeRedis(options.RedisConnectionString);
        }

        services.AddSingleton<INotificationDispatcher, SignalRNotificationDispatcher>();

        return services;
    }
}
