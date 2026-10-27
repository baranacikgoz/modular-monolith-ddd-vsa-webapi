using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.StockCounters;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Response = Inventory.Endpoints.StockLevels.v1.DailyTotals.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

/// <summary>The committed totals of one of the caller's own active stores. A store that is not the caller's has no days to show.</summary>
[Collection("IntegrationTestCollection")]
public class MyDailyTotalsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DateOnly Day = new(2026, 3, 10);

    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private static Uri RouteOf(DefaultIdType storeId) =>
        new($"/v1/stock-levels/my/daily-totals?storeId={storeId}&from={Day.ToString("O", CultureInfo.InvariantCulture)}&to={Day.AddDays(1).ToString("O", CultureInfo.InvariantCulture)}", UriKind.Relative);

    private async Task SeedAsync(DefaultIdType storeId, int units)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        await StockCounterWriter.AddCommittedAsync(db, storeId, units, Day, DateTimeOffset.UtcNow, CancellationToken.None);
    }

    private async Task<List<Response>> GetDaysAsync(ApplicationUserId caller, DefaultIdType storeId)
    {
        var response = await StockLevelTestData.AuthedClientOf(Factory, caller).GetAsync(RouteOf(storeId));
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        return (await response.Content.ReadFromJsonAsync<List<Response>>(JsonSerializerOptions))!;
    }

    [Fact]
    public async Task GetMyDailyTotals_OwnActiveStore_ReturnsItsDays()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);
        await SeedAsync(storeId, 3);
        await SeedAsync(storeId, 4);

        var days = await GetDaysAsync(owner, storeId);

        var day = Assert.Single(days);
        Assert.Equal(Day, day.Day);
        Assert.Equal(2, day.CommittedReservations);
        Assert.Equal(7, day.CommittedUnits);
    }

    // Cross-store negative: naming another owner's store returns no days at all, not that store's days.
    [Fact]
    public async Task GetMyDailyTotals_StoreOfAnotherOwner_ReturnsNoDays()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, NewOwner());
        await SeedAsync(storeId, 9);

        var days = await GetDaysAsync(NewOwner(), storeId);

        Assert.Empty(days);
    }

    [Fact]
    public async Task GetMyDailyTotals_StoreThatIsNoLongerActive_ReturnsNoDays()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner, isActive: false);
        await SeedAsync(storeId, 9);

        var days = await GetDaysAsync(owner, storeId);

        Assert.Empty(days);
    }

    [Fact]
    public async Task GetMyDailyTotals_StoreTheCopyHasNeverSeen_ReturnsNoDays()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await SeedAsync(storeId, 9);

        var days = await GetDaysAsync(NewOwner(), storeId);

        Assert.Empty(days);
    }

    [Fact]
    public async Task GetMyDailyTotals_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().GetAsync(RouteOf(DefaultIdType.CreateVersion7()));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
