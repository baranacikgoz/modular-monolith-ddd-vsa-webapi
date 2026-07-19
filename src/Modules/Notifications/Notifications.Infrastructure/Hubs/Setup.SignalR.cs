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

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isProduction = string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase);

        if (isProduction && options?.UseRedisBackplane != true)
        {
            throw new InvalidOperationException(
                "SignalROptions.UseRedisBackplane is false in Production. " +
                "Multi-instance deployments require the Redis backplane for SignalR fan-out.");
        }

        var builder = services.AddSignalR();

        if (options?.UseRedisBackplane == true)
        {
            builder.AddStackExchangeRedis(options.RedisConnectionString);
        }

        services.AddSingleton<INotificationDispatcher, SignalRNotificationDispatcher>();

        return services;
    }
}
