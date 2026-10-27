using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Application.Gateway;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Gateway;

public static class Setup
{
    public static IServiceCollection AddWarehouseGatewayInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<WarehouseResiliencePipelines>();

        // No standard resilience handler (retry, breaker, timeout) on this client: each call goes through the pipeline of its
        // own connection instead (WarehouseResiliencePipelines), which has no retry, so a release is never sent twice. The
        // client only carries an overall timeout; the base address is the connection's and is applied per call.
        services.AddResilientHttpClient<IWarehouseGatewayFactory, WarehouseGatewayFactory>(
            (serviceProvider, httpClient) =>
            {
                httpClient.Timeout = TimeSpan.FromSeconds(
                    serviceProvider.GetRequiredService<IOptions<ResiliencyOptions>>().Value.TotalRequestTimeoutSeconds);
            },
            useStandardPipeline: false);

        return services;
    }
}
