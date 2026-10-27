using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.WarehouseSecrets;

public static class Setup
{
    /// <summary>
    ///     The sweep that queues a rewrite for every page of warehouse connections holding a secret under an old encryption key.
    ///     The consumer that does the rewrite is picked up by the Host's assembly scans.
    /// </summary>
    public static IServiceCollection AddWarehouseSecretReprotection(this IServiceCollection services)
    {
        services.AddScoped<WarehouseSecretReprotectSweepService>();
        return services.AddHostedService<WarehouseSecretReprotectSweepJobRegistrar>();
    }
}
