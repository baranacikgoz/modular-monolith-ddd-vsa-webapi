using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundJobs;

public static class ModuleInstaller
{
    public static IServiceCollection AddBackgroundJobsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
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
            });

        var isServer = configuration
                      .GetSection(nameof(BackgroundJobsOptions))
                      .Get<BackgroundJobsOptions>()?
                      .IsServer ?? throw new InvalidOperationException("BackgroundJobsOptions is not configured.");

        if (isServer)
        {
            services.AddHangfireServer((sp, cfg) =>
            {
                var pollingFrequencyInSeconds = sp.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.PollingFrequencyInSeconds;
                cfg.SchedulePollingInterval = TimeSpan.FromSeconds(pollingFrequencyInSeconds);
            });
        }

        return services;
    }

    public static WebApplication UseBackgroundJobsModule(this WebApplication app)
    {
        var dashboardPath = app.Services.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.DashboardPath;

        app.UseHangfireDashboard(dashboardPath, new DashboardOptions()
        {
            AsyncAuthorization = [new HangfireCustomAuthorizationFilter()]
        });

        return app;
    }
}
