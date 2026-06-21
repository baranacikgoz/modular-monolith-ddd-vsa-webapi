using Common.Infrastructure.Persistence.Auditing;
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

        return services;
    }
}
