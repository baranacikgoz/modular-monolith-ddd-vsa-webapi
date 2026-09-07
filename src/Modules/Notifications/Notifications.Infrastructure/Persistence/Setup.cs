using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notifications.Application.Persistence;

namespace Notifications.Infrastructure.Persistence;

internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services.AddModuleDbContext<INotificationsDbContext, NotificationsDbContext>(nameof(Notifications));
    }

    public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(Setup).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<NotificationsDbContext>(
            app.ApplicationServices, logger, nameof(Notifications));

        return app;
    }
}
