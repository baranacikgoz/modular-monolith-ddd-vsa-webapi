using Common.Application.BackgroundJobs;
using Common.Infrastructure.Options;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundJobs;

public static class ModuleInstaller
{
    public static IServiceCollection AddBackgroundJobsModule(this IServiceCollection services)
        => services
            .AddSingleton<IBackgroundJobs, BackgroundJobsService>()
            .AddSingleton<IRecurringBackgroundJobs, RecurringBackgroundJobsService>()
            .AddHangfire((sp, cfg) =>
                {
                    var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

                    cfg.UseSimpleAssemblyNameTypeSerializer()
                       .UseRecommendedSerializerSettings()
                       .UsePostgreSqlStorage(pgs => pgs.UseNpgsqlConnection(connectionString), new PostgreSqlStorageOptions()
                       {
                           SchemaName = nameof(BackgroundJobs)
                       });
                })
            .AddHangfireServer((sp, cfg) =>
                {
                    var pollingFrequencyInSeconds = sp.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.PollingFrequencyInSeconds;

                    cfg.SchedulePollingInterval = TimeSpan.FromSeconds(pollingFrequencyInSeconds);
                });

    public static WebApplication UseBackgroundJobsModule(this WebApplication app)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        app.UseHangfireServer();
#pragma warning restore CS0618 // Type or member is obsolete

        var dashboardPath = app.Services.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.DashboardPath;

        app.UseHangfireDashboard(dashboardPath, new DashboardOptions()
        {
            AsyncAuthorization = [new HangfireCustomAuthorizationFilter()]
        });

        return app;
    }
}
