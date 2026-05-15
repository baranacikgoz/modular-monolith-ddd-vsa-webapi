using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.AuditLog;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
    {
        return services
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
