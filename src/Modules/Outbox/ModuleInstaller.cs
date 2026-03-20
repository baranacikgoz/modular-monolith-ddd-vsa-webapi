using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using EntityFramework.Exceptions.PostgreSQL;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;

public static partial class ModuleInstaller
{
    public static IServiceCollection AddOutboxModule(this IServiceCollection services)
        => services
            .AddScoped<IOutboxDbContext, OutboxDbContext>()
            .AddDbContextPool<OutboxDbContext>((sp, options) =>
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

    public static WebApplication UseOutboxModule(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            context.Database.Migrate();

            busControl.Stop();
        }

        return app;
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Sql}")]
        public static partial void LogSql(ILogger logger, string sql);
    }
}
