using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure;
public static class ModuleInstaller
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
        => services.AddNotificationServices();
}
