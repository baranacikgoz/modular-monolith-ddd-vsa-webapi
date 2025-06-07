using Common.Application.Options;
using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.EventSourcing;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Persistence.DbContext;
public static class Setup
{
    public static IServiceCollection AddModuleDbContext<TContext>(this IServiceCollection services, string moduleName)
        where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
            var observabilityOptions = sp.GetRequiredService<IOptions<ObservabilityOptions>>().Value;

            options
                .UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName, schema: moduleName))
                .UseExceptionProcessor()
                .AddInterceptors(
                    sp.GetRequiredService<ApplyAuditingInterceptor>(),
                    sp.GetRequiredService<InsertEventStoreEventsInterceptor>());

            if (observabilityOptions.LogGeneratedSqlQueries)
            {
                var logger = sp.GetRequiredService<ILogger<TContext>>();

#pragma warning disable
                options.LogTo(
                    sql => logger.LogDebug(sql),                  // Log the SQL query
                    new[] { DbLoggerCategory.Database.Command.Name }, // Only log database commands
                    LogLevel.Information                           // Set the log level
                );
#pragma warning restore
            }
        });

        return services;
    }
}
