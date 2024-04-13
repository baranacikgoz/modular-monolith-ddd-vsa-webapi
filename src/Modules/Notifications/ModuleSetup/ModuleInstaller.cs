using Microsoft.Extensions.DependencyInjection;
using Notifications.Services;

namespace Notifications.ModuleSetup;
public static class ModuleInstaller
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
        => services.AddNotificationServices();
}
