using Common.Application.Persistence;
using Common.Application.Persistence.Inbox;
using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.Inbox;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Common.Infrastructure.Persistence.DbContext;

public static class Setup
{
    public static IServiceCollection AddModuleDbContext<TContextInterface, TContextImplementation>(
        this IServiceCollection services,
        string moduleName)
        where TContextInterface : IDbContext
        where TContextImplementation : Microsoft.EntityFrameworkCore.DbContext
    {
        services.AddDbContext<TContextImplementation>((sp, options) =>
        {
            options
                .UseNpgsql(
                    sp.GetRequiredService<NpgsqlDataSource>(),
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, moduleName))
                .UseExceptionProcessor()
                .AddInterceptors(
                    sp.GetRequiredService<ApplyAuditingInterceptor>(),
                    sp.GetRequiredService<ApplySearchLanguageInterceptor>());
        });

        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TContextImplementation));
        if (descriptor != null)
        {
            services.Add(new ServiceDescriptor(
                typeof(TContextInterface),
                sp => sp.GetRequiredService<TContextImplementation>(),
                descriptor.Lifetime));
        }

        // One transactional inbox per module DbContext, sharing the scope's context instance. A consumer
        // injects the generic store closed over its own module's context interface, never the non-generic
        // one (see the remarks on that interface); the cleanup job enumerates every module's target.
        services
            .AddScoped(sp => new InboxStore<TContextInterface>(
                sp.GetRequiredService<TContextInterface>(),
                sp.GetRequiredService<TimeProvider>(),
                moduleName))
            .AddScoped<IInboxStore<TContextInterface>>(sp => sp.GetRequiredService<InboxStore<TContextInterface>>())
            .AddScoped<IInboxCleanupTarget>(sp => sp.GetRequiredService<InboxStore<TContextInterface>>());

        return services;
    }
}
