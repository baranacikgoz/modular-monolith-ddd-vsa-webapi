using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;
using DomainConstants = Inventory.Domain.WarehouseConnections.Constants;
using Response = Inventory.Endpoints.WarehouseConnections.v1.Create.Response;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

/// <summary>The in-memory provider end to end: connected without an address or a credential, and used without any network call.</summary>
[Collection("IntegrationTestCollection")]
public class DummyProviderTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    [Fact]
    public async Task Create_DummyProviderWithoutAddressOrCredential_StoresAPlaceholderConnection()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var storeId = DefaultIdType.CreateVersion7();

        var response = await client.PostAsJsonAsync(
            new Uri("/v1/warehouse-connections", UriKind.Relative), new { storeId, provider = "Dummy" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var created = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, created.Id);
        Assert.Equal(WarehouseProvider.Dummy, stored.Provider);
        Assert.Equal(WarehouseConnectionStatus.Active, stored.Status);
        Assert.Equal(new Uri(DomainConstants.DummyBaseUrl), stored.BaseUrl);
        Assert.Equal(created.WebhookSecret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
    }

    [Fact]
    public async Task Release_StoreWithADummyConnection_ReleasesWithoutAnyNetworkCall()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(
            Factory.Services, storeId, WarehouseProvider.Dummy, baseUrl: new Uri(DomainConstants.DummyBaseUrl));
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);

        var response = await StockLevelTestData.AuthedClient(Factory)
            .PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.Empty(factory.Warehouse.Requests);
    }
}
