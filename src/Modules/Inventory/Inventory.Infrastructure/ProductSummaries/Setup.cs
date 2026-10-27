using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.ProductSummaries;

public static class Setup
{
    public static IServiceCollection AddProductSummaryReconciliation(this IServiceCollection services)
    {
        services.AddScoped<ProductSummariesReconciliationJob>();
        return services.AddHostedService<ProductSummariesReconciliationJobRegistrar>();
    }
}
