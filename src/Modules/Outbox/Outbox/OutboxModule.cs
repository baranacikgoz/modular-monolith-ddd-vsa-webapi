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

public sealed partial class OutboxModule : IModule
{
    public string Name => "Outbox";
    public int StartupPriority => 1;

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Using AddDbContext (not AddDbContextPool) because BaseDbContext needs to
        // share a connection with OutboxDbContext for atomic transactions via SetDbConnection.
        // Pooled contexts do not support connection swapping.
        services
            .AddScoped<IOutboxDbContext, OutboxDbContext>()
            .AddDbContext<OutboxDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                var observabilityOptions = sp.GetRequiredService<IOptions<ObservabilityOptions>>().Value;

                options
                    .UseNpgsql(
                        connectionString,
                        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Outbox)))
                    .UseExceptionProcessor();

                if (observabilityOptions.LogGeneratedSqlQueries)
                {
                    var logger = sp.GetRequiredService<ILogger<OutboxDbContext>>();
                    options.LogTo(
                        sql => LoggerMessages.LogSql(logger, sql), // Log the SQL query
                        new[] { DbLoggerCategory.Database.Command.Name }, // Only log database commands
                        LogLevel.Information // Set the log level
                    );
                }
            })
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

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Sql}")]
        public static partial void LogSql(ILogger logger, string sql);
    }
}
