using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.StoreAccesses;

public static class Setup
{
    public static IServiceCollection AddStoreAccessReconciliation(this IServiceCollection services)
    {
        services.AddScoped<StoreAccessReconciliationJob>();
        return services.AddHostedService<StoreAccessReconciliationJobRegistrar>();
    }
}
