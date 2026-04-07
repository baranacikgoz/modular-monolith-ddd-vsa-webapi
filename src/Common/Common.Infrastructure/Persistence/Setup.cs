using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.AuditLog;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
    {
        return services
            .AddAuditLogServices()
            .AddAuditingInterceptors()
            .AddSingleton<SeedingCompletionTracker>()
            .AddHostedService<DatabaseSeederOrchestrator>();
    }
}
