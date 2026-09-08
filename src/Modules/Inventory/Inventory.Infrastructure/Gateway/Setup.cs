using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Application.Gateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.Gateway;

public static class Setup
{
    public static IServiceCollection AddWarehouseGatewayInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var inventoryOptions = configuration.GetSection(nameof(InventoryOptions)).Get<InventoryOptions>()
            ?? throw new InvalidOperationException($"Configuration for {nameof(InventoryOptions)} is null.");

        return inventoryOptions.WarehouseGatewayProvider switch
        {
            WarehouseGatewayProvider.Dummy => services.AddSingleton<IWarehouseGateway, DummyWarehouseGateway>(),
            WarehouseGatewayProvider.Http => services.AddHttpWarehouseGateway(inventoryOptions),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), inventoryOptions.WarehouseGatewayProvider,
                "Unknown WarehouseGatewayProvider.")
        };
    }

    private static IServiceCollection AddHttpWarehouseGateway(this IServiceCollection services, InventoryOptions inventoryOptions)
    {
        services.AddResilientHttpClient<IWarehouseGateway, HttpWarehouseGateway>(
            httpClient =>
            {
                // Trailing slash is REQUIRED: HttpClient drops the last BaseAddress segment
                // when combining with a relative URI otherwise.
#pragma warning disable S1075
                httpClient.BaseAddress = new Uri(inventoryOptions.WarehouseGatewayBaseUrl!.TrimEnd('/') + '/');
#pragma warning restore S1075
            },
            resilience =>
            {
                // A release call is not safely retryable blind: a timeout doesn't tell you whether the
                // warehouse already processed the release, so a naive retry risks a double-release. Same
                // reasoning as disabling retry on Payments' Iyzico gateway for its mutating capture call -
                // ReconcileReleaseAsync exists precisely to resolve this ambiguity instead of retrying blind.
                resilience.Retry.MaxRetryAttempts = 0;
            });

        return services;
    }
}
