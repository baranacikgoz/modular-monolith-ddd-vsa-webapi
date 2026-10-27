using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Inventory.Endpoints.WarehouseConnections;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

/// <summary>
///     The connections of the caller's own stores, read through the local store access copy: the answer never waits on Products, and
///     a store that is not active disappears from it.
/// </summary>
[Collection("IntegrationTestCollection")]
public class MySearchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private async Task GrantAccessAsync(DefaultIdType storeId, ApplicationUserId owner, bool isActive)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var row = StoreAccess.Create(storeId);
        row.ApplyIfNewer(owner, isActive, 1);
        db.StoreAccess.Add(row);
        await db.SaveChangesAsync();
    }

    private async Task<PaginationResponse<WarehouseConnectionResponse>> GetMineAsync(ApplicationUserId caller)
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", caller.Value.ToString());

        var response = await client.GetAsync(new Uri("/v1/warehouse-connections/my?pageNumber=1&pageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return (await response.Content.ReadFromJsonAsync<PaginationResponse<WarehouseConnectionResponse>>(JsonSerializerOptions))!;
    }

    [Fact]
    public async Task GetMine_CallerWithAnActiveStore_ListsOnlyTheConnectionsOfThatStore()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await GrantAccessAsync(storeId, owner, isActive: true);
        var (mine, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var otherStoreId = DefaultIdType.CreateVersion7();
        await GrantAccessAsync(otherStoreId, NewOwner(), isActive: true);
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, otherStoreId);

        var page = await GetMineAsync(owner);

        var listed = Assert.Single(page.Data);
        Assert.Equal(mine.Id, listed.Id);
        Assert.Equal(storeId, listed.StoreId);
        Assert.Equal(1, page.TotalCount);
    }

    [Fact]
    public async Task GetMine_CallerWithoutAStore_GetsAnEmptyList()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await GrantAccessAsync(storeId, NewOwner(), isActive: true);
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);

        var page = await GetMineAsync(NewOwner());

        Assert.Empty(page.Data);
        Assert.Equal(0, page.TotalCount);
    }

    [Fact]
    public async Task GetMine_StoreThatIsNoLongerActive_DisappearsFromTheList()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await GrantAccessAsync(storeId, owner, isActive: false);
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);

        var page = await GetMineAsync(owner);

        Assert.Empty(page.Data);
    }

    [Fact]
    public async Task GetMine_StoreTheCopyHasNeverSeen_IsNotListed()
    {
        var owner = NewOwner();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, DefaultIdType.CreateVersion7());

        var page = await GetMineAsync(owner);

        Assert.Empty(page.Data);
    }

    [Fact]
    public async Task GetMine_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient()
            .GetAsync(new Uri("/v1/warehouse-connections/my", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
