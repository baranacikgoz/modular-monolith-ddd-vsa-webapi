using Common.Application.Options;
using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.AuditLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Common.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
    {
        return services
            // Single shared NpgsqlDataSource → one bounded connection pool for the whole app.
            // Every DbContext, the audit-log retention worker, and the postgresql health check resolve
            // this same instance, so they share one pool (capped via "Maximum Pool Size" in the connection
            // string) instead of each spinning up its own data source with its own 100-connection pool.
            .AddSingleton(sp =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                return new NpgsqlDataSourceBuilder(connectionString).Build();
            })
            // Explicit registration required: without IAM loaded, AddAuthentication() is never called,
            // and ASP.NET Core's implicit TimeProvider.System registration never happens.
            // All DbContexts and interceptors depend on TimeProvider — so it must live here in Common.
            .AddSingleton(TimeProvider.System)
            .AddAuditLogServices()
            .AddAuditingInterceptors()
            .AddSingleton<SeedingCompletionTracker>()
            .AddHostedService<DatabaseSeederOrchestrator>();
    }
}
