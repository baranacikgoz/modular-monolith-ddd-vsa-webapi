using Microsoft.Extensions.DependencyInjection;
using Notifications.Application;

namespace Notifications.Infrastructure.Sms;

internal static class Setup
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        return services.AddSingleton<ISmsService, DummySmsService>();
    }
}
