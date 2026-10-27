using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Endpoints.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class SearchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private async Task<PaginationResponse<WarehouseConnectionResponse>> GetPageAsync(string query)
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var response = await client.GetAsync(new Uri($"/v1/warehouse-connections?{query}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return (await response.Content.ReadFromJsonAsync<PaginationResponse<WarehouseConnectionResponse>>(JsonSerializerOptions))!;
    }

    [Fact]
    public async Task Search_ByStore_ListsThatStoresConnectionsNewestFirstWithoutSecrets()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (older, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, deactivated: true);
        var (newer, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        await WarehouseConnectionTestData.SeedAsync(Factory.Services);

        var page = await GetPageAsync($"pageNumber=1&pageSize=10&storeId={storeId}");

        Assert.Equal([newer.Id, older.Id], page.Data.Select(c => c.Id).ToArray());
        Assert.Equal(2, page.TotalCount);
        Assert.Equal(
            [WarehouseConnectionStatus.Active, WarehouseConnectionStatus.Deactivated],
            page.Data.Select(c => c.Status).ToArray());
    }

    [Fact]
    public async Task Search_ByStatus_ListsOnlyThatStatus()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, deactivated: true);
        var (active, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);

        var page = await GetPageAsync($"pageNumber=1&pageSize=10&storeId={storeId}&status=Active");

        Assert.Equal([active.Id], page.Data.Select(c => c.Id).ToArray());
    }
}
