using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.Webhooks;

public static class Setup
{
    public static IServiceCollection AddWarehouseWebhooks(this IServiceCollection services)
    {
        services.AddScoped<WarehouseWebhookIntake>();
        services.AddScoped<WebhookDeliveryRetentionService>();
        return services.AddHostedService<WebhookDeliveryRetentionJobRegistrar>();
    }
}
