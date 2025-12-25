using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
    {
        return services.AddNotificationServices();
    }

    public static WebApplication UseNotificationsModule(this WebApplication app)
    {
        // Will be filled when module reaches maturity

        return app;
    }
}
