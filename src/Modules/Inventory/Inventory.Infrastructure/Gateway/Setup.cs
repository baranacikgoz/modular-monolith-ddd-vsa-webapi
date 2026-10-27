using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Application.Gateway;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Gateway;

public static class Setup
{
    public static IServiceCollection AddWarehouseGatewayInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<WarehouseResiliencePipelines>();
        services.TryAddSingleton<IWarehouseHostResolver, SystemWarehouseHostResolver>();
        services.AddSingleton<WarehouseConnectGuard>();

        // No standard resilience handler (retry, breaker, timeout) on this client: each call goes through the pipeline of its
        // own connection instead (WarehouseResiliencePipelines), which has no retry, so a release is never sent twice. The
        // client only carries an overall timeout; the base address is the connection's and is applied per call.
        services.AddResilientHttpClient<IWarehouseGatewayFactory, WarehouseGatewayFactory>(
                (serviceProvider, httpClient) =>
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(
                        serviceProvider.GetRequiredService<IOptions<ResiliencyOptions>>().Value.TotalRequestTimeoutSeconds);
                },
                useStandardPipeline: false)
            // The address of a connection is caller supplied. The handler resolves and checks it on every connect, never
            // follows a redirect (a 3xx would otherwise lead to any address) and never goes through a proxy (the proxy would
            // resolve the name, so the check would see the proxy instead of the target).
            .ConfigurePrimaryHttpMessageHandler(serviceProvider => CreateHandler(
                serviceProvider.GetRequiredService<WarehouseConnectGuard>(),
                TimeSpan.FromMinutes(
                    serviceProvider.GetRequiredService<IOptions<ResiliencyOptions>>().Value.PooledConnectionLifetimeMinutes)));

        return services;
    }

    internal static SocketsHttpHandler CreateHandler(WarehouseConnectGuard guard, TimeSpan pooledConnectionLifetime)
    {
        return new SocketsHttpHandler
        {
            PooledConnectionLifetime = pooledConnectionLifetime,
            AllowAutoRedirect = false,
            UseProxy = false,
            ConnectCallback = guard.ConnectAsync
        };
    }
}
