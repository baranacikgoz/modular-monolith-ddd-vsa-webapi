using Common.Application.Options;
using Common.Infrastructure.Modules;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Outbox;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;

public sealed class OutboxModule : ICoreModule
{
    public string Name => "Outbox";
    public int StartupPriority => 1;

    public IEnumerable<string> ActivitySourceNames => [Telemetry.OutboxTelemetry.ActivitySourceName];

    public IEnumerable<string> MeterNames => [Telemetry.OutboxTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // AddDbContext (not AddDbContextPool): pooled contexts do not support connection swapping,
        // and OutboxDbContext is used only by the processors — not by BaseDbContext anymore.
        services
            .AddDbContext<OutboxDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

                options
                    .UseNpgsql(
                        connectionString,
                        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Outbox)))
                    .UseExceptionProcessor();
            })
            .AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<OutboxDbContext>())
            .AddHostedService<OutboxKafkaProcessor>();
    }

    public void UseModule(IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(OutboxModule).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<OutboxDbContext>(
            app.ApplicationServices, logger, nameof(Outbox));
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }
}
