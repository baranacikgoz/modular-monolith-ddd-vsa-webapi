using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.AuditLog;

public static class Setup
{
    public static IServiceCollection AddAuditLogServices(this IServiceCollection services)
    {
        // Audit log entry insertion is handled directly by BaseDbContext.SaveChangesAsync()
        // in a unified event collection pass. No interceptor registration needed.

        // Register the retention service and recurring job for purging expired audit log entries.
        services.AddScoped<AuditLogRetentionService>();
        services.AddHostedService<AuditLogRetentionJobRegistrar>();

        return services;
    }
}
