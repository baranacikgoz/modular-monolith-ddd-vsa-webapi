using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Infrastructure.Alerts;

internal static class Setup
{
    public static IServiceCollection AddAlertRecipients(this IServiceCollection services)
    {
        services.AddScoped<AlertRecipientsReconciliationJob>();
        return services.AddHostedService<AlertRecipientsReconcileJobRegistrar>();
    }
}
