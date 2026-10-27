using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.StockPushes;

public static class Setup
{
    /// <summary>
    ///     The push runner behind the queue consumer and the sweep that republishes old marks. The consumer and the mark
    ///     handler are picked up by the Host's assembly scans.
    /// </summary>
    public static IServiceCollection AddStockPushes(this IServiceCollection services)
    {
        services.AddScoped<StockPushRunner>();
        services.AddScoped<StockPushSweepService>();
        return services.AddHostedService<StockPushSweepJobRegistrar>();
    }
}
