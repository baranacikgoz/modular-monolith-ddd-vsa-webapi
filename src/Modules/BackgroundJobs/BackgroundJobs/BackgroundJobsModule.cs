using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundJobs;

public sealed class BackgroundJobsModule : IModule
{
    public string Name => "BackgroundJobs";
    public int StartupPriority => 0; // Probably the most core thing to run

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IBackgroundJobs, BackgroundJobsService>()
            .AddSingleton<IRecurringBackgroundJobs, RecurringBackgroundJobsService>()
            .AddHangfire((sp, cfg) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

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
                var pollingFrequencyInSeconds = sp.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value
                    .PollingFrequencyInSeconds;
                cfg.SchedulePollingInterval = TimeSpan.FromSeconds(pollingFrequencyInSeconds);
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
