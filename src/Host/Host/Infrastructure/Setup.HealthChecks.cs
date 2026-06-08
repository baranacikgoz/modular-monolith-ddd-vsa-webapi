using Common.Application.Options;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Npgsql;
using StackExchange.Redis;
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
                      ?? throw new InvalidOperationException($"Missing configuration: {nameof(HealthCheckOptions)}.");

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

        // Readiness: PostgreSQL connectivity. Probes the shared NpgsqlDataSource pool (the same one every
        // DbContext uses) rather than dialing a fresh data source per probe.
        builder.AddNpgSql(
            sp => sp.GetRequiredService<NpgsqlDataSource>(),
            name: "postgresql",
            tags: [ReadyTag],
            timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));

        // Readiness: Redis connectivity. Probes the shared IConnectionMultiplexer (registered in
        // AddCommonCaching) instead of spinning up a dedicated multiplexer just for the health check.
        var cachingOptions = configuration
            .GetSection(nameof(CachingOptions))
            .Get<CachingOptions>();

        if (cachingOptions is { UseRedis: true, Redis: not null })
        {
            builder.AddRedis(
                sp => sp.GetRequiredService<IConnectionMultiplexer>(),
                "redis",
                tags: [ReadyTag],
                timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));
        }

        // Readiness: RabbitMQ connectivity is owned by MassTransit's "masstransit-bus" health check
        // (tagged "ready" in AddCustomMassTransit). It probes the already-open bus connection instead
        // of dialing a brand-new AMQP connection on every probe, so there is no custom check here.

        // Startup: PostgreSQL reachable during boot — ensures migrations have been applied.
        builder.AddNpgSql(
            sp => sp.GetRequiredService<NpgsqlDataSource>(),
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
}
