using Common.Application.Options;
using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.EventSourcing;
using Common.Infrastructure.Persistence.Outbox;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Persistence.DbContext;
public static class Setup
{
    public static IServiceCollection AddModuleDbContext<TModuleContext>(this IServiceCollection services, string moduleName)
        where TModuleContext : Microsoft.EntityFrameworkCore.DbContext
        => services.AddDbContext<TModuleContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

            options
                .UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName, schema: moduleName))
                .UseExceptionProcessor()
                .AddInterceptors(
                    sp.GetRequiredService<ApplyAuditingInterceptor>(),
                    sp.GetRequiredService<InsertEventStoreEventsInterceptor>(),
                    sp.GetRequiredService<InsertOutboxMessagesAndClearEventsInterceptor>());
        });
}
