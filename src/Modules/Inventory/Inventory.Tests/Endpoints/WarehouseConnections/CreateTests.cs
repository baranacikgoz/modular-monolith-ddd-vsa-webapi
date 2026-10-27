using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.DomainEvents.v1;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Response = Inventory.Endpoints.WarehouseConnections.v1.Create.Response;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class CreateTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly Uri Route = new("/v1/warehouse-connections", UriKind.Relative);

    private static object BodyOf(
        DefaultIdType storeId, string provider = "JsonApi", string baseUrl = "https://warehouse.example-partner.local/api",
        string apiCredential = WarehouseConnectionTestData.ApiCredential)
    {
        return new { storeId, provider, baseUrl, apiCredential };
    }

    [Fact]
    public async Task Create_ValidRequest_StoresBothSecretsEncryptedAndShowsTheWebhookSecretOnce()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var storeId = DefaultIdType.CreateVersion7();

        var response = await client.PostAsJsonAsync(Route, BodyOf(storeId, provider: "FormApi"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var created = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        Assert.False(string.IsNullOrEmpty(created.WebhookSecret));
        Assert.EndsWith($"/webhooks/warehouse/{created.Id}", created.WebhookPath, StringComparison.Ordinal);

        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, created.Id);
        Assert.Equal(storeId, stored.StoreId);
        Assert.Equal(WarehouseProvider.FormApi, stored.Provider);
        Assert.Equal(WarehouseConnectionStatus.Active, stored.Status);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api"), stored.BaseUrl);

        Assert.Equal(WarehouseConnectionTestData.ApiCredential, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedApiCredential));
        Assert.Equal(created.WebhookSecret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
        Assert.DoesNotContain(created.WebhookSecret, Encoding.UTF8.GetString(stored.EncryptedWebhookSecret), StringComparison.Ordinal);
        Assert.DoesNotContain(WarehouseConnectionTestData.ApiCredential, Encoding.UTF8.GetString(stored.EncryptedApiCredential), StringComparison.Ordinal);
    }

    [Fact]
    public async Task Create_ValidRequest_AuditsTheCreationWithoutEitherSecret()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(Route, BodyOf(DefaultIdType.CreateVersion7()));

        var created = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var entry = await db.AuditLog.AsNoTracking().SingleAsync(e => e.AggregateId == created.Id.Value);
        Assert.IsType<V1WarehouseConnectionCreatedDomainEvent>(entry.Event);
        var serialized = JsonSerializer.Serialize(entry.Event, entry.Event.GetType());
        Assert.DoesNotContain(created.WebhookSecret, serialized, StringComparison.Ordinal);
        Assert.DoesNotContain(WarehouseConnectionTestData.ApiCredential, serialized, StringComparison.Ordinal);
    }

    [Fact]
    public async Task Create_StoreAlreadyHasAnActiveConnection_ReturnsConflict()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);

        var response = await client.PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Create_AfterTheStoresConnectionWasDeactivated_Succeeds()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, deactivated: true);

        var response = await client.PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_TwoRequestsForOneStoreAtOnce_OneSucceedsAndTheOtherConflicts()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var clients = Enumerable.Range(0, 8).Select(_ => StockLevelTestData.AuthedClient(Factory)).ToList();

        var responses = await Task.WhenAll(clients.Select(c => c.PostAsJsonAsync(Route, BodyOf(storeId))));

        Assert.Single(responses, r => r.StatusCode == HttpStatusCode.OK);
        Assert.All(responses.Where(r => r.StatusCode != HttpStatusCode.OK), r => Assert.Equal(HttpStatusCode.Conflict, r.StatusCode));
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        Assert.Equal(1, await db.WarehouseConnections.AsNoTracking().CountAsync(c => c.StoreId == storeId));
    }

    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequestAndStoresNothing()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var storeId = DefaultIdType.CreateVersion7();

        // A plain http address and an empty credential: both are refused before anything is encrypted or saved.
        var response = await client.PostAsJsonAsync(
            Route, BodyOf(storeId, baseUrl: "http://warehouse.example-partner.local/api", apiCredential: string.Empty));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        Assert.False(await db.WarehouseConnections.AsNoTracking().AnyAsync(c => c.StoreId == storeId));
    }
}
