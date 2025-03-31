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
    public static IServiceCollection AddModuleDbContext<TContextInterface, TContextImplementation>(this IServiceCollection services, string moduleName)
        where TContextImplementation : Microsoft.EntityFrameworkCore.DbContext
    {
        services.AddDbContext<TContextImplementation>((sp, options) =>
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

        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TContextImplementation));
        if (descriptor != null)
        {
            services.Add(new ServiceDescriptor(
                typeof(TContextInterface),
                sp => sp.GetRequiredService<TContextImplementation>(),
                descriptor.Lifetime));
        }

        return services;
    }
}
