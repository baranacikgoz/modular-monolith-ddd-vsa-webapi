using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.BulkAdjustments;

public static class Setup
{
    /// <summary>The accept-queue-poll bulk adjustment of the module: its enqueuer and runner (the queue consumer itself is
    /// picked up by the Host's consumer scan) and the housekeeping behind the job table.</summary>
    public static IServiceCollection AddStockBulkAdjustments(this IServiceCollection services)
    {
        return services
            .AddScoped<StockBulkAdjustJobEnqueuer>()
            .AddScoped<StockBulkAdjustJobRunner>()
            .AddScoped<StockBulkAdjustJobHousekeeping>()
            .AddScoped<StockJobsHousekeeping>()
            .AddHostedService<StockJobsHousekeepingRegistrar>();
    }
}
