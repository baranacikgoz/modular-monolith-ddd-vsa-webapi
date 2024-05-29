using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence.Context.Interceptors;
using Common.Infrastructure.Persistence.EventSourcing;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Persistence.Context;
public static class Setup
{
    public static IServiceCollection AddModuleDbContext<TModuleContext>(this IServiceCollection services, string moduleName)
        where TModuleContext : DbContext
        => services.AddDbContext<TModuleContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

            options
                .UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, moduleName))
                .AddInterceptors(
                    sp.GetRequiredService<ApplyAuditingInterceptor>(),
                    sp.GetRequiredService<InsertOutboxMessagesInterceptor>(),
                    sp.GetRequiredService<InsertEventStoreEventsInterceptor>(),
                    sp.GetRequiredService<ClearAggregateEventsInterceptor>(),
                    sp.GetRequiredService<DeleteStreamIfAggregateIsDeletedInterceptor>());
        });
}
