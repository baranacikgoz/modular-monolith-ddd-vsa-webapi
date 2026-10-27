using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Application.Pagination;
using Common.IntegrationEvents;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;
using ListApplicationsResponse = Products.Endpoints.Stores.v1.ListApplications.Response;
using MyGetResponse = Products.Endpoints.Stores.v1.My.Get.Response;
using SearchResponse = Products.Endpoints.Stores.v1.Search.Response;

namespace Products.Tests.Endpoints.Stores;

public class ApprovalLifecycleTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private async Task<Store> SeedAsync(StoreStatus status, ApplicationUserId? owner = null)
    {
        var store = Store.Apply(owner ?? new ApplicationUserId(Guid.NewGuid()), $"Store {Guid.NewGuid():N}", "Description", "Address");
        store.ClearEvents();

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

    private async Task<StoreStatus> StatusOfAsync(StoreId id)
    {
        return await Db.Stores.AsNoTracking().Where(s => s.Id == id).Select(s => s.Status).SingleAsync();
    }

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    private static Uri Route(StoreId id, string action) => new($"/v1/stores/{id}/{action}", UriKind.Relative);

    [Fact]
    public async Task Approve_PendingStore_ActivatesItAndQueuesTheApprovedEventForTheOwner()
    {
        var owner = new ApplicationUserId(Guid.NewGuid());
        var store = await SeedAsync(StoreStatus.PendingApproval, owner);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "approve"), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(StoreStatus.Active, await StatusOfAsync(store.Id));
        var published = Assert.Single(await OutboxEventsAsync());
        var approved = Assert.IsType<StoreApprovedIntegrationEvent>(published);
        Assert.Equal(store.Id.Value, approved.StoreId);
        Assert.Equal(owner, approved.OwnerId);
    }

    [Fact]
    public async Task Approve_InactiveStore_ReturnsConflictAndPublishesNothing()
    {
        var store = await SeedAsync(StoreStatus.Inactive);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "approve"), content: null);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("NotPendingApproval", await ErrorKeyAsync(response));
        Assert.Empty(await OutboxEventsAsync());
    }

    [Fact]
    public async Task Approve_WithoutAuthentication_ReturnsUnauthorized()
    {
        var store = await SeedAsync(StoreStatus.PendingApproval);

        using var response = await Factory.CreateClient().PostAsync(Route(store.Id, "approve"), content: null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Suspend_ActiveStore_SuspendsIt()
    {
        var store = await SeedAsync(StoreStatus.Active);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "suspend"), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(StoreStatus.Suspended, await StatusOfAsync(store.Id));
    }

    [Fact]
    public async Task Suspend_PendingStore_ReturnsConflict()
    {
        var store = await SeedAsync(StoreStatus.PendingApproval);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "suspend"), content: null);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("NotActive", await ErrorKeyAsync(response));
    }

    [Fact]
    public async Task Reinstate_SuspendedStore_ActivatesIt()
    {
        var store = await SeedAsync(StoreStatus.Suspended);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "reinstate"), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(StoreStatus.Active, await StatusOfAsync(store.Id));
    }

    [Fact]
    public async Task Reinstate_DeactivatedStore_ReturnsConflict()
    {
        var store = await SeedAsync(StoreStatus.Inactive);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "reinstate"), content: null);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("NotSuspended", await ErrorKeyAsync(response));
    }

    [Fact]
    public async Task Deactivate_SuspendedStore_ClosesItForGood()
    {
        var store = await SeedAsync(StoreStatus.Suspended);

        using var response = await AuthorizedClient().PostAsync(Route(store.Id, "deactivate"), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(StoreStatus.Inactive, await StatusOfAsync(store.Id));
    }

    [Fact]
    public async Task ListApplications_ReturnsOnlyPendingStoresOldestFirst()
    {
        var first = await SeedAsync(StoreStatus.PendingApproval);
        await SeedAsync(StoreStatus.Active);
        var second = await SeedAsync(StoreStatus.PendingApproval);

        using var response = await AuthorizedClient().GetAsync(new Uri("/v1/stores/applications?PageNumber=1&PageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var page = await response.Content.ReadFromJsonAsync<PaginationResponse<ListApplicationsResponse>>(JsonSerializerOptions);
        Assert.Equal([first.Id, second.Id], page!.Data.Select(a => a.Id));
    }

    [Fact]
    public async Task Search_DoesNotListStoresStillWaitingForApproval()
    {
        var pending = await SeedAsync(StoreStatus.PendingApproval);
        var active = await SeedAsync(StoreStatus.Active);

        using var response = await AuthorizedClient().GetAsync(new Uri("/v1/stores/search?PageNumber=1&PageSize=50", UriKind.Relative));

        var page = await response.Content.ReadFromJsonAsync<PaginationResponse<SearchResponse>>(JsonSerializerOptions);
        Assert.NotNull(page);
        Assert.Contains(page.Data, s => s.Id == active.Id);
        Assert.DoesNotContain(page.Data, s => s.Id == pending.Id);
    }

    [Fact]
    public async Task MyGet_ForAnApplicant_ShowsThatTheStoreIsPending()
    {
        await SeedAsync(StoreStatus.PendingApproval, DefaultUser);

        using var response = await AuthorizedClient().GetAsync(new Uri("/v1/stores/my", UriKind.Relative));

        var store = await response.Content.ReadFromJsonAsync<MyGetResponse>(JsonSerializerOptions);
        Assert.Equal(StoreStatus.PendingApproval, store!.Status);
    }
}
