using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Common.Infrastructure.FeatureManagement;

public static class Setup
{
    public static IServiceCollection AddCommonFeatureManagement(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFeatureManagement(configuration.GetSection("FeatureManagement"))
            .WithTargeting<HttpContextTargetingContextAccessor>();

        return services;
    }
}
