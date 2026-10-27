using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Common.Infrastructure.Security;
using Inventory.Application.Gateway;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using GatewaySetup = Inventory.Infrastructure.Gateway.Setup;

namespace Inventory.Tests.Gateway;

/// <summary>
///     The gateway registration as the host builds it, with the real HTTP handler: the integration tests replace that
///     handler with a fake, so only this proves that the address guard is what the registered client actually connects through.
/// </summary>
public sealed class WarehouseGatewaySetupTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly SecretProtector _protector = new(Options.Create(new SecretProtectionOptions
    {
        EncryptionKey = SecretProtectionOptionsValidator.DevelopmentPlaceholderKey
    }));

    public WarehouseGatewaySetupTests()
    {
        var resiliency = TestWarehouseResilience.ResiliencyConfiguration();
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(Options.Create(resiliency));
        services.AddSingleton(Options.Create(TestInventoryOptions.Create()));
        services.AddSingleton(_protector);
        services.AddKeyedResiliencePipelines();
        services.AddWarehouseGatewayInfrastructure();
        _provider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        _provider.Dispose();
    }

    [Fact]
    public async Task RegisteredClient_BaseUrlThatResolvesToTheLoopback_IsRefusedBeforeAnythingIsSent()
    {
        var connection = TestWarehouseResilience.Connection(
            WarehouseProvider.JsonApi, _protector.Encrypt("credential"), new Uri("https://localhost:9/api/"));
        var gateway = _provider.GetRequiredService<IWarehouseGatewayFactory>().Create(connection);

        var released = await gateway.Value!.RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.True(released.IsFailure);
        Assert.Equal(WarehouseGatewayErrors.AddressNotAllowed.Key, released.Error!.Key);
    }

    [Fact]
    public async Task RegisteredFactory_DummyConnection_KeepsTheStateAcrossGateways()
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.Dummy, baseUrl: new Uri(Constants.DummyBaseUrl));
        var factory = _provider.GetRequiredService<IWarehouseGatewayFactory>();

        await factory.Create(connection).Value!.RequestReleaseAsync("product-1", 1, CancellationToken.None);
        var reconciled = await factory.Create(connection).Value!.ReconcileReleaseAsync("product-1", CancellationToken.None);

        Assert.False(reconciled.IsFailure);
    }
}
