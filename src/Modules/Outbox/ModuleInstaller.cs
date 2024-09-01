using System.Data;
using System.Reflection;
using Common.Infrastructure;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence.Outbox;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;

public class OutboxModule : IModule
{
    public int RegistrationPriority => 0;

    public IEnumerable<Assembly> GetAssemblies()
    {
        yield return typeof(IAssemblyReference).Assembly;
    }

    public void Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
                .AddScoped<IOutboxDbContext, OutboxDbContext>()
                .AddScoped<InsertOutboxMessagesInterceptor>()
                .AddDbContext<OutboxDbContext>((sp, options) =>
                {
                    var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                    options
                    .UseNpgsql(
                        connectionString,
                        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Outbox)));
                })
                .AddSingleton<OutboxBackgroundProcessor>()
                .AddHostedService<OutboxBackgroundProcessor>();

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> RateLimitingPolicies() => [];

    public IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters()
    {
        // This periodic outbox background processor pollutes efcore traces
        yield return (providerName, dbCommand) => !dbCommand.CommandText.Contains("FROM \"Outbox\".", StringComparison.OrdinalIgnoreCase);
    }

    public void Use(WebApplication app, RouteGroupBuilder routeGroupBuilder)
    {
        using var scope = app.Services.CreateScope();
        var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
        busControl.Start();

        var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        context.Database.Migrate();

        busControl.Stop();
    }
}
