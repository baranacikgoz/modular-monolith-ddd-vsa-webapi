using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.Webhooks;

public static class Setup
{
    public static IServiceCollection AddWarehouseWebhookIntake(this IServiceCollection services)
    {
        return services.AddScoped<WarehouseWebhookIntake>();
    }
}
