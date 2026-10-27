using System.Net;
using Common.Application.Options;
using Common.Infrastructure.Security;
using Common.Tests;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.Errors;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Microsoft.Extensions.Options;
using Xunit;
using WarehouseConstants = Inventory.Domain.WarehouseConnections.Constants;

namespace Inventory.Tests.Gateway;

public class WarehouseGatewayFactoryTests : WarehouseTransportTestBase
{
    private const string Credential = "factory-credential";

    private readonly SecretProtector _protector = new(Options.Create(new SecretProtectionOptions
    {
        EncryptionKey = SecretProtectionOptionsValidator.DevelopmentPlaceholderKey,
        PreviousEncryptionKeys = []
    }));

    private readonly DummyWarehouseState _dummyState = new();

    private WarehouseGatewayFactory NewFactory(string environmentName = "Development")
    {
        return new WarehouseGatewayFactory(
            Client, _protector, TestWarehouseResilience.Pipelines(), _dummyState, new FakeHostEnvironment(environmentName));
    }

    private WarehouseConnection ConnectionOf(WarehouseProvider provider, Uri baseUrl)
    {
        return TestWarehouseResilience.Connection(provider, _protector.Encrypt(Credential), baseUrl);
    }

    [Fact]
    public async Task Create_JsonApiConnection_ReturnsTheJsonGatewayAddressedAtTheConnectionWithTheDecryptedCredential()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref-1" });
        var connection = ConnectionOf(WarehouseProvider.JsonApi, new Uri("https://warehouse.example-partner.local/api"));

        var created = NewFactory().Create(connection);

        Assert.False(created.IsFailure);
        Assert.IsType<JsonApiWarehouseGateway>(created.Value);
        var released = await created.Value.RequestReleaseAsync(StockReservationId.New(), "product-1", 2, CancellationToken.None);
        Assert.Equal("ref-1", released.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/releases"), request.Uri);
        Assert.Equal($"Bearer {Credential}", request.Authorization);
    }

    [Fact]
    public async Task Create_FormApiConnection_ReturnsTheFormGatewayAddressedAtTheConnectionWithTheDecryptedCredential()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "ref-2" } });
        var connection = ConnectionOf(WarehouseProvider.FormApi, new Uri("https://warehouse.example-other.local/gateway/"));

        var created = NewFactory().Create(connection);

        Assert.False(created.IsFailure);
        Assert.IsType<FormApiWarehouseGateway>(created.Value);
        var released = await created.Value.RequestReleaseAsync(StockReservationId.New(), "product-1", 2, CancellationToken.None);
        Assert.Equal("ref-2", released.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(new Uri("https://warehouse.example-other.local/gateway/v2/holds/release"), request.Uri);
        Assert.Equal(Credential, request.ApiKey);
    }

    // The test pipelines, like resiliency.json, have no profile for the dummy provider, and its stored credential is not
    // even decryptable here: the factory must ask for neither.
    [Fact]
    public async Task Create_DummyConnection_ReturnsTheInMemoryGatewayAndNeverTouchesTheNetwork()
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.Dummy, baseUrl: new Uri(WarehouseConstants.DummyBaseUrl));

        var created = NewFactory().Create(connection);

        Assert.False(created.IsFailure);
        Assert.IsType<DummyWarehouseGateway>(created.Value);
        var released = await created.Value.RequestReleaseAsync(StockReservationId.New(), "product-1", 2, CancellationToken.None);
        Assert.False(released.IsFailure);
        Assert.Empty(Handler.Requests);
    }

    // A connection row copied into a production database from elsewhere would otherwise answer every release and stock push
    // from memory, so the warehouse it stands for would never hear of them.
    [Fact]
    public void Create_DummyConnectionInProduction_ReturnsADefiniteErrorAndBuildsNoGateway()
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.Dummy, baseUrl: new Uri(WarehouseConstants.DummyBaseUrl));

        var created = NewFactory("Production").Create(connection);

        Assert.True(created.IsFailure);
        Assert.Equal(WarehouseConnectionErrors.DummyProviderNotAllowedInProduction.Key, created.Error!.Key);
        Assert.Empty(Handler.Requests);
    }

    [Fact]
    public async Task Create_RealProviderInProduction_StillBuildsItsGateway()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref-3" });
        var connection = ConnectionOf(WarehouseProvider.JsonApi, new Uri("https://warehouse.example-partner.local/api"));

        var created = NewFactory("Production").Create(connection);

        Assert.False(created.IsFailure);
        Assert.IsType<JsonApiWarehouseGateway>(created.Value);
        Assert.False((await created.Value.RequestReleaseAsync(StockReservationId.New(), "product-1", 2, CancellationToken.None)).IsFailure);
    }

    [Fact]
    public void Create_DeactivatedDummyConnection_ReturnsAnErrorAndBuildsNoGateway()
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.Dummy, baseUrl: new Uri(WarehouseConstants.DummyBaseUrl));
        connection.Deactivate();

        var created = NewFactory().Create(connection);

        Assert.Equal(WarehouseConnectionErrors.NotActive.Key, created.Error!.Key);
    }

    [Fact]
    public void Create_DeactivatedConnection_ReturnsAnErrorAndBuildsNoGateway()
    {
        var connection = ConnectionOf(WarehouseProvider.JsonApi, new Uri("https://warehouse.example-partner.local/api/"));
        connection.Deactivate();

        var created = NewFactory().Create(connection);

        Assert.Equal(WarehouseConnectionErrors.NotActive.Key, created.Error!.Key);
    }
}
