using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;
using ListAllResponse = Products.Endpoints.Stores.v1.ListAll.Response;

namespace Products.Tests.Endpoints.Stores;

/// <summary>
///     The administration listing shows a store in any status, which the public search deliberately does not. The scope the
///     route asks for is pinned in <see cref="StoreRouteScopesTests" />.
/// </summary>
public class ListAllTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private async Task<Store> SeedAsync(StoreStatus status)
    {
        var store = Store.Apply(new ApplicationUserId(Guid.NewGuid()), $"Store {Guid.NewGuid():N}", "Description", "Address");

        // Walk the status through the aggregate, the only place that changes it.
        if (status is StoreStatus.Active or StoreStatus.Suspended or StoreStatus.Inactive)
        {
            store.Approve();
        }

        if (status == StoreStatus.Suspended)
        {
            store.Suspend();
        }

        if (status == StoreStatus.Inactive)
        {
            store.Deactivate();
        }

        store.ClearEvents();
        Db.Stores.Add(store);
        await Db.SaveChangesAsync();
        return store;
    }

    private async Task<PaginationResponse<ListAllResponse>> ListAsync(string query)
    {
        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/stores/administration?PageNumber=1&PageSize=50{query}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var page = await response.Content.ReadFromJsonAsync<PaginationResponse<ListAllResponse>>(JsonSerializerOptions);
        return page!;
    }

    [Theory]
    [InlineData(StoreStatus.Active)]
    [InlineData(StoreStatus.Inactive)]
    [InlineData(StoreStatus.PendingApproval)]
    [InlineData(StoreStatus.Suspended)]
    public async Task ListAll_FilteredByStatus_ReturnsOnlyStoresInThatStatus(StoreStatus status)
    {
        var expected = await SeedAsync(status);
        foreach (var other in Enum.GetValues<StoreStatus>().Where(s => s != status))
        {
            await SeedAsync(other);
        }

        var page = await ListAsync($"&Status={status}");

        var listed = Assert.Single(page.Data);
        Assert.Equal(expected.Id, listed.Id);
        Assert.Equal(status, listed.Status);
        Assert.Equal(1, page.TotalCount);
    }

    [Fact]
    public async Task ListAll_WithoutStatus_ReturnsEveryStoreNewestFirst()
    {
        var first = await SeedAsync(StoreStatus.Suspended);
        var second = await SeedAsync(StoreStatus.PendingApproval);
        var third = await SeedAsync(StoreStatus.Inactive);
        var fourth = await SeedAsync(StoreStatus.Active);

        var page = await ListAsync(string.Empty);

        Assert.Equal([fourth.Id, third.Id, second.Id, first.Id], page.Data.Select(s => s.Id));
        Assert.Equal(
            [StoreStatus.Active, StoreStatus.Inactive, StoreStatus.PendingApproval, StoreStatus.Suspended],
            page.Data.Select(s => s.Status));
    }

    [Fact]
    public async Task ListAll_PagedWithTheCursor_WalksEveryStoreOnce()
    {
        var seeded = new List<StoreId>();
        foreach (var status in Enum.GetValues<StoreStatus>())
        {
            seeded.Add((await SeedAsync(status)).Id);
        }

        var seen = new List<StoreId>();
        string? after = null;
        do
        {
            var cursor = after is null ? string.Empty : $"&After={Uri.EscapeDataString(after)}";
            using var response = await AuthorizedClient().GetAsync(
                new Uri($"/v1/stores/administration?PageNumber=1&PageSize=3&IncludeTotal=false{cursor}", UriKind.Relative));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var page = await response.Content.ReadFromJsonAsync<PaginationResponse<ListAllResponse>>(JsonSerializerOptions);
            seen.AddRange(page!.Data.Select(s => s.Id));
            after = page.NextCursor;
        }
        while (after is not null);

        Assert.Equal(seeded.Order(), seen.Order());
        Assert.Equal(seen.Count, seen.Distinct().Count());
    }

    [Fact]
    public async Task ListAll_UnknownStatus_ReturnsBadRequest()
    {
        using var response = await AuthorizedClient().GetAsync(
            new Uri("/v1/stores/administration?PageNumber=1&PageSize=10&Status=99", UriKind.Relative));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ListAll_WithoutAuthentication_ReturnsUnauthorized()
    {
        using var response = await Factory.CreateClient().GetAsync(
            new Uri("/v1/stores/administration?PageNumber=1&PageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
