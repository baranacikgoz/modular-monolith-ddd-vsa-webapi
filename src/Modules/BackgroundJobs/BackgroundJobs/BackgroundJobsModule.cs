using BackgroundJobs.Telemetry;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundJobs;

public sealed class BackgroundJobsModule : ICoreModule
{
    public string Name => "BackgroundJobs";
    public int StartupPriority => 0; // Probably the most core thing to run
    public IEnumerable<string> ActivitySourceNames => [BackgroundJobsTelemetry.ActivitySourceName];
    public IEnumerable<string> MeterNames => [BackgroundJobsTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IBackgroundJobs, BackgroundJobsService>()
            .AddSingleton<IRecurringBackgroundJobs, RecurringBackgroundJobsService>()
            .AddHangfire((sp, cfg) =>
            {
                // Hangfire keeps its OWN Postgres pool, separate from the API's shared NpgsqlDataSource.
                // Cap it independently (and bound WorkerCount below) so background jobs can't starve the
                // API pool or, together with it, exhaust Postgres max_connections across instances.
                var connectionString = new NpgsqlConnectionStringBuilder(
                    sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString)
                {
                    MaxPoolSize = sp.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.MaxPoolSize,
                    ApplicationName = "modular-monolith-hangfire"
                }.ConnectionString;

                cfg.UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(pgs => pgs.UseNpgsqlConnection(connectionString),
                        new PostgreSqlStorageOptions { SchemaName = nameof(BackgroundJobs) });
            });

        var isServer = configuration
            .GetSection(nameof(BackgroundJobsOptions))
            .Get<BackgroundJobsOptions>()?
            .IsServer ?? throw new InvalidOperationException("BackgroundJobsOptions is not configured.");

        if (isServer)
        {
            services.AddHangfireServer((sp, cfg) =>
            {
                var backgroundJobsOptions = sp.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value;
                cfg.SchedulePollingInterval = TimeSpan.FromSeconds(backgroundJobsOptions.PollingFrequencyInSeconds);

                // Validator enforces WorkerCount <= MaxPoolSize so jobs never block waiting on a connection.
                cfg.WorkerCount = backgroundJobsOptions.WorkerCount;
            });
        }
    }

    public void UseModule(IApplicationBuilder app)
    {
        var dashboardPath = app.ApplicationServices.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value
            .DashboardPath;

        app.UseHangfireDashboard(dashboardPath,
            new DashboardOptions { AsyncAuthorization = [new HangfireCustomAuthorizationFilter()] });
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }
}
