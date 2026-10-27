using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.Exports;
using Products.Infrastructure.Imports;

namespace Products.Infrastructure.Jobs;

public static class Setup
{
    /// <summary>The accept-queue-poll jobs of the module: their enqueuers and runners (the queue consumers themselves are
    /// picked up by the Host's consumer scan) and the housekeeping behind both job tables.</summary>
    public static IServiceCollection AddProductJobs(this IServiceCollection services)
    {
        return services
            .AddScoped<ProductImportJobEnqueuer>()
            .AddScoped<ProductImportJobRunner>()
            .AddScoped<ProductExportJobEnqueuer>()
            .AddScoped<ProductExportJobRunner>()
            .AddScoped<ProductImportJobHousekeeping>()
            .AddScoped<ProductExportJobHousekeeping>()
            .AddScoped<ProductJobsHousekeeping>()
            .AddHostedService<ProductJobsHousekeepingRegistrar>();
    }
}
