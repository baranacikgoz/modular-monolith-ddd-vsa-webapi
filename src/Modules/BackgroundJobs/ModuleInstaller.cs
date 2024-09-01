using System.Data;
using System.Reflection;
using Common.Application.BackgroundJobs;
using Common.Infrastructure;
using Common.Infrastructure.Options;
using Common.InterModuleRequests;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundJobs;

public class BackgroundJobsModule : IModule
{
    public int RegistrationPriority => 2;

    public IEnumerable<Assembly> GetAssemblies()
    {
        yield return typeof(IAssemblyReference).Assembly;
    }

    public void Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
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

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> RateLimitingPolicies() => [];

    public IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters() => [];

    public void Use(WebApplication app, RouteGroupBuilder routeGroupBuilder)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        app.UseHangfireServer();
#pragma warning restore CS0618 // Type or member is obsolete

        var dashboardPath = app.Services.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.DashboardPath;

        app.UseHangfireDashboard(dashboardPath, new DashboardOptions()
        {
            AsyncAuthorization = [new HangfireCustomAuthorizationFilter()]
        });
    }
}
