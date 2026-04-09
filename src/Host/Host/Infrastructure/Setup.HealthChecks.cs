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
                      ?? new HealthCheckOptions();

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

        // Liveness probe: Is the process alive?
        app.MapHealthChecks("/health/live", new AspNetHealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(LiveTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).ExcludeFromDescription();

        // Readiness probe: Can it serve traffic? (checks all dependencies)
        app.MapHealthChecks("/health/ready", new AspNetHealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(ReadyTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).ExcludeFromDescription();

        // Startup probe: Has it finished booting?
        app.MapHealthChecks("/health/startup", new AspNetHealthCheckOptions
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
