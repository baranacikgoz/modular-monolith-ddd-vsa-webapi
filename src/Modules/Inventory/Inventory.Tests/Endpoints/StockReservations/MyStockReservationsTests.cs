using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Domain.StockReservations;
using Inventory.Tests.Support;
using Xunit;
using Response = Inventory.Endpoints.StockReservations.v1.Get.Response;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     A store owner reads the reservations of the products of their own active stores, and nothing else. A reservation of
///     any other store is answered exactly like one that does not exist, and a caller without an active store gets an empty list.
/// </summary>
[Collection("IntegrationTestCollection")]
public class MyStockReservationsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private static Uri GetRoute(StockReservationId id) => new($"/v1/stock-reservations/my/{id}", UriKind.Relative);

    private static Uri SearchRoute(string query = "pageNumber=1&pageSize=10") => new($"/v1/stock-reservations/my?{query}", UriKind.Relative);

    /// <summary>A store owned by <paramref name="owner" /> with one product and one reservation of it.</summary>
    private async Task<(DefaultIdType StoreId, DefaultIdType ProductId, StockReservation Reservation)> SeedStoreWithReservationAsync(
        ApplicationUserId owner, bool isActive = true)
    {
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner, isActive);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        return (storeId, productId, reservation);
    }

    private async Task<PaginationResponse<Response>> SearchAsync(ApplicationUserId caller, string query = "pageNumber=1&pageSize=10")
    {
        var response = await StockLevelTestData.AuthedClientOf(Factory, caller).GetAsync(SearchRoute(query));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return (await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions))!;
    }

    [Fact]
    public async Task GetMy_ReservationOfAnOwnActiveStore_ReturnsIt()
    {
        var owner = NewOwner();
        var (_, productId, reservation) = await SeedStoreWithReservationAsync(owner);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).GetAsync(GetRoute(reservation.Id));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        Assert.Equal(reservation.Id, body.Id);
        Assert.Equal(productId, body.ProductId);
        Assert.Equal(10, body.Quantity);
        Assert.Equal(ReservationStatus.Active, body.Status);
    }

    // Cross-store negative: another owner's reservation answers exactly like an id that does not exist.
    [Fact]
    public async Task GetMy_ReservationOfAnotherOwnersStore_ReturnsNotFound()
    {
        var (_, _, theirs) = await SeedStoreWithReservationAsync(NewOwner());

        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).GetAsync(GetRoute(theirs.Id));
        var unknown = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).GetAsync(GetRoute(StockReservationId.New()));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, unknown.StatusCode);
    }

    [Fact]
    public async Task GetMy_ReservationOfAStoreThatIsNoLongerActive_ReturnsNotFound()
    {
        var owner = NewOwner();
        var (_, _, reservation) = await SeedStoreWithReservationAsync(owner, isActive: false);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).GetAsync(GetRoute(reservation.Id));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMy_ReservationOfAProductNobodyProjectedYet_ReturnsNotFound()
    {
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, DefaultIdType.CreateVersion7());

        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).GetAsync(GetRoute(reservation.Id));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMy_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().GetAsync(GetRoute(StockReservationId.New()));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task SearchMy_OwnerWithTwoStores_ListsTheirReservationsNewestFirstAndNoOneElses()
    {
        var owner = NewOwner();
        var (_, _, first) = await SeedStoreWithReservationAsync(owner);
        var (_, _, second) = await SeedStoreWithReservationAsync(owner);
        await SeedStoreWithReservationAsync(NewOwner());

        var page = await SearchAsync(owner);

        Assert.Equal([second.Id, first.Id], page.Data.Select(r => r.Id).ToArray());
    }

    [Fact]
    public async Task SearchMy_CallerWithoutAnActiveStore_GetsAnEmptyPage()
    {
        await SeedStoreWithReservationAsync(NewOwner());
        var inactiveOwner = NewOwner();
        await SeedStoreWithReservationAsync(inactiveOwner, isActive: false);

        var stranger = await SearchAsync(NewOwner());
        var inactive = await SearchAsync(inactiveOwner);

        Assert.Empty(stranger.Data);
        Assert.Empty(inactive.Data);
    }

    // Cross-store negative: naming another owner's product in the filter does not reach it.
    [Fact]
    public async Task SearchMy_FilterNamingAnotherOwnersProduct_ListsNothing()
    {
        var owner = NewOwner();
        await SeedStoreWithReservationAsync(owner);
        var (_, theirProductId, _) = await SeedStoreWithReservationAsync(NewOwner());

        var page = await SearchAsync(owner, $"pageNumber=1&pageSize=10&productId={theirProductId}");

        Assert.Empty(page.Data);
    }

    [Fact]
    public async Task SearchMy_FilterByProductAndStatus_NarrowsTheOwnList()
    {
        var owner = NewOwner();
        var (_, productId, active) = await SeedStoreWithReservationAsync(owner);
        var committed = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId, committed: true);
        var (_, _, otherProduct) = await SeedStoreWithReservationAsync(owner);

        var byProduct = await SearchAsync(owner, $"pageNumber=1&pageSize=10&productId={productId}");
        var byStatus = await SearchAsync(owner, "pageNumber=1&pageSize=10&status=Committed");

        Assert.Equal([committed.Id, active.Id], byProduct.Data.Select(r => r.Id).ToArray());
        Assert.Equal([committed.Id], byStatus.Data.Select(r => r.Id).ToArray());
        Assert.DoesNotContain(otherProduct.Id, byProduct.Data.Select(r => r.Id));
    }

    [Fact]
    public async Task SearchMy_FollowingTheCursor_WalksEveryReservationOnce()
    {
        var owner = NewOwner();
        var (_, productId, first) = await SeedStoreWithReservationAsync(owner);
        var second = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        var third = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);

        var page1 = await SearchAsync(owner, "pageNumber=1&pageSize=2");
        var page2 = await SearchAsync(owner, $"pageNumber=1&pageSize=2&after={page1.NextCursor}");

        Assert.Equal([third.Id, second.Id], page1.Data.Select(r => r.Id).ToArray());
        Assert.Equal([first.Id], page2.Data.Select(r => r.Id).ToArray());
    }

    [Fact]
    public async Task SearchMy_UnknownStatus_ReturnsBadRequest()
    {
        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).GetAsync(SearchRoute("status=99"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SearchMy_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().GetAsync(SearchRoute());

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // The administration route keeps reading any reservation, whoever owns the store.
    [Fact]
    public async Task Get_AdministrationRoute_StillReadsAnyReservation()
    {
        var (_, _, theirs) = await SeedStoreWithReservationAsync(NewOwner());

        var response = await StockLevelTestData.AuthedClient(Factory)
            .GetAsync(new Uri($"/v1/stock-reservations/{theirs.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
