using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.BulkAdjustments;

public static class Setup
{
    /// <summary>The accept-queue-poll bulk adjustment of the module: its enqueuer and runner (the queue consumer itself is
    /// picked up by the Host's consumer scan), the housekeeping behind the job table and the sweep that hands stuck jobs back
    /// to the queue.</summary>
    public static IServiceCollection AddStockBulkAdjustments(this IServiceCollection services)
    {
        return services
            .AddScoped<StockBulkAdjustJobEnqueuer>()
            .AddScoped<StockBulkAdjustJobRunner>()
            .AddScoped<StockBulkAdjustJobHousekeeping>()
            .AddScoped<StockJobsHousekeeping>()
            .AddScoped<StockJobRequeueService>()
            .AddHostedService<StockJobsHousekeepingRegistrar>()
            .AddHostedService<StockJobRequeueJobRegistrar>();
    }
}
