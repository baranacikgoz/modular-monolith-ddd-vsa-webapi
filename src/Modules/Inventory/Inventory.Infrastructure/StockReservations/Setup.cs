using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.StockReservations;

public static class Setup
{
    public static IServiceCollection AddStockReservationExpirySweep(this IServiceCollection services)
    {
        services.AddScoped<StockReservationExpirySweepService>();
        return services.AddHostedService<StockReservationExpirySweepJobRegistrar>();
    }
}
