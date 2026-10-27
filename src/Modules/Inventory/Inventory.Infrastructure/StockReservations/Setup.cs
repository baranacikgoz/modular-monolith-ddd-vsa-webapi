using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.StockReservations;

public static class Setup
{
    public static IServiceCollection AddStockReservationExpirySweep(this IServiceCollection services)
    {
        services.AddScoped<StockReservationExpirySweepService>();
        return services.AddHostedService<StockReservationExpirySweepJobRegistrar>();
    }

    /// <summary>
    ///     The reconciler behind the admin endpoint and the queue consumer, and the sweep that queues a reconcile for every
    ///     release attempt whose outcome was never recorded. The consumer is picked up by the Host's assembly scans.
    /// </summary>
    public static IServiceCollection AddStockReleaseReconciliation(this IServiceCollection services)
    {
        services.AddScoped<StockReleaseReconciler>();
        services.AddScoped<StockReleaseReconcileSweepService>();
        return services.AddHostedService<StockReleaseReconcileSweepJobRegistrar>();
    }
}
