using Common.Application.Options;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using AspNetHealthCheckOptions = Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions;
using HealthCheckOptions = Common.Application.Options.HealthCheckOptions;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private const string LiveTag = "live";
    private const string ReadyTag = "ready";
    private const string StartupTag = "startup";

    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration
                          .GetSection(nameof(HealthCheckOptions))
                          .Get<HealthCheckOptions>()
                      ?? new HealthCheckOptions
                      {
                          LivenessTimeoutInSeconds = 3, ReadinessTimeoutInSeconds = 5, StartupTimeoutInSeconds = 10
                      };

        if (!options.EnableHealthChecks)
        {
            return services;
        }

        var builder = services.AddHealthChecks();

        // Liveness: self-check only — proves the process is alive and can handle HTTP.
        builder.AddCheck(
            "self",
            () => HealthCheckResult.Healthy(),
            [LiveTag]);

        // Readiness: PostgreSQL connectivity.
        builder.AddNpgSql(
            sp => sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString,
            name: "postgresql",
            tags: [ReadyTag],
            timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));

        // Readiness: Redis connectivity.
        var cachingOptions = configuration
            .GetSection(nameof(CachingOptions))
            .Get<CachingOptions>();

        if (cachingOptions is { UseRedis: true, Redis: not null })
        {
            builder.AddRedis(
                _ =>
                    $"{cachingOptions.Redis.Host}:{cachingOptions.Redis.Port},password={cachingOptions.Redis.Password}",
                "redis",
                tags: [ReadyTag],
                timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));
        }

        // Readiness: RabbitMQ connectivity.
        var rabbitMqOptions = configuration
            .GetSection(nameof(RabbitMqOptions))
            .Get<RabbitMqOptions>();

        if (rabbitMqOptions is not null)
        {
            builder.AddCheck<ConditionalRabbitMqHealthCheck>(
                "rabbitmq",
                HealthStatus.Unhealthy,
                [ReadyTag],
                TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));
        }

        // Startup: PostgreSQL reachable during boot — ensures migrations have been applied.
        builder.AddNpgSql(
            sp => sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString,
            name: "postgresql-startup",
            tags: [StartupTag],
            timeout: TimeSpan.FromSeconds(options.StartupTimeoutInSeconds));

        return services;
    }

    public static IApplicationBuilder MapCustomHealthChecks(this WebApplication app)
    {
        var options = app.Services
            .GetRequiredService<IOptions<HealthCheckOptions>>()
            .Value;

        if (!options.EnableHealthChecks)
        {
            return app;
        }

        app.MapHealthChecks("/health/live",
            new AspNetHealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(LiveTag),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).ExcludeFromDescription();

        app.MapHealthChecks("/health/ready",
            new AspNetHealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(ReadyTag),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).ExcludeFromDescription();

        app.MapHealthChecks("/health/startup",
            new AspNetHealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(StartupTag),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).ExcludeFromDescription();

        LogHealthChecksRegistered(app.Logger);

        return app;
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Health check endpoints registered: /health/live, /health/ready, /health/startup")]
    private static partial void LogHealthChecksRegistered(ILogger logger);

    private sealed class ConditionalRabbitMqHealthCheck(
        IOptions<HealthCheckOptions> healthCheckOptions,
        IOptions<RabbitMqOptions> rabbitMqOptions) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (healthCheckOptions.Value.SkipRabbitMqHealthCheck)
            {
                return HealthCheckResult.Healthy("RabbitMQ check skipped.");
            }

            try
            {
                var opts = rabbitMqOptions.Value;
                var factory = new RabbitMQ.Client.ConnectionFactory
                {
                    HostName = opts.Host,
                    VirtualHost = opts.VirtualHost,
                    UserName = opts.Username,
                    Password = opts.Password
                };
                await using var connection = await factory.CreateConnectionAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                return HealthCheckResult.Unhealthy("RabbitMQ unreachable.", ex);
            }
        }
    }
}
