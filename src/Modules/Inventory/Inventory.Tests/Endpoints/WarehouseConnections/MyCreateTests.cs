using System.Net;
using System.Net.Http.Json;
using System.Text;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Response = Inventory.Endpoints.WarehouseConnections.v1.Create.Response;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

/// <summary>
///     A store owner connects their own active store. The role in the token is checked by Keycloak before the route runs; the
///     store is checked here, against the local store access copy.
/// </summary>
[Collection("IntegrationTestCollection")]
public class MyCreateTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly Uri Route = new("/v1/warehouse-connections/my", UriKind.Relative);

    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private static object BodyOf(
        DefaultIdType storeId, string provider = "FormApi", string baseUrl = "https://warehouse.example-partner.local/api")
    {
        return new { storeId, provider, baseUrl, apiCredential = WarehouseConnectionTestData.ApiCredential };
    }

    private async Task<bool> AnyConnectionOfAsync(DefaultIdType storeId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.WarehouseConnections.AsNoTracking().AnyAsync(c => c.StoreId == storeId);
    }

    [Fact]
    public async Task Create_OwnerOfAnActiveStore_ConnectsItWithBothSecretsEncryptedAndShowsTheWebhookSecretOnce()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var created = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        Assert.False(string.IsNullOrEmpty(created.WebhookSecret));
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, created.Id);
        Assert.Equal(storeId, stored.StoreId);
        Assert.Equal(WarehouseProvider.FormApi, stored.Provider);
        Assert.Equal(WarehouseConnectionStatus.Active, stored.Status);
        Assert.Equal(WarehouseConnectionTestData.ApiCredential, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedApiCredential));
        Assert.Equal(created.WebhookSecret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
        Assert.DoesNotContain(created.WebhookSecret, Encoding.UTF8.GetString(stored.EncryptedWebhookSecret), StringComparison.Ordinal);
    }

    // Cross-store negative: another owner's store is answered exactly like a store that does not exist.
    [Fact]
    public async Task Create_StoreOfAnotherOwner_ReturnsNotFoundAndStoresNothing()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, NewOwner());

        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.False(await AnyConnectionOfAsync(storeId));
    }

    [Fact]
    public async Task Create_StoreThatIsNotActive_ReturnsNotFoundAndStoresNothing()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner, isActive: false);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.False(await AnyConnectionOfAsync(storeId));
    }

    [Fact]
    public async Task Create_StoreTheCopyHasNeverSeen_ReturnsNotFoundAndStoresNothing()
    {
        var storeId = DefaultIdType.CreateVersion7();

        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.False(await AnyConnectionOfAsync(storeId));
    }

    [Fact]
    public async Task Create_StoreAlreadyHasAnActiveConnection_ReturnsConflict()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).PostAsJsonAsync(Route, BodyOf(storeId));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    // The address policy is the create request's own validator, shared by both routes: an owner cannot aim the server inward either.
    [Fact]
    public async Task Create_BaseUrlLeadingToAnInternalAddress_ReturnsBadRequestAndStoresNothing()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner)
            .PostAsJsonAsync(Route, BodyOf(storeId, baseUrl: "https://169.254.169.254/latest/meta-data"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.False(await AnyConnectionOfAsync(storeId));
    }

    [Fact]
    public async Task Create_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().PostAsJsonAsync(Route, BodyOf(DefaultIdType.CreateVersion7()));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // The slice tests let every authenticated caller through every scope, so the scope each create route asks for is pinned here:
    // the store owner's route asks for the scope only the store-owner role holds, the administration route for the admin one.
    [Theory]
    [InlineData("/my", KeycloakScopes.WarehouseConnections.Connect)]
    [InlineData("", KeycloakScopes.WarehouseConnections.Create)]
    public void CreateRoute_RequiresItsScope(string route, string scope)
    {
        var endpoints = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>();
        var pattern = $"/v{{version:apiVersion}}/warehouse-connections{route}";

        var endpoint = Assert.Single(endpoints, e =>
            e.RoutePattern.RawText?.TrimEnd('/') == pattern
            && e.Metadata.GetMetadata<IHttpMethodMetadata>()!.HttpMethods.Contains("POST", StringComparer.Ordinal));

        var policies = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>().Select(a => a.Policy).ToList();
        Assert.Contains(KeycloakPermission.FromScope(scope).PolicyName(), policies);
    }
}
