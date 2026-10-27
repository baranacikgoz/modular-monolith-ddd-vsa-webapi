using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.StockCounters;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Response = Inventory.Endpoints.StockLevels.v1.DailyTotals.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

/// <summary>
///     A store's per-day committed reservation totals inside a range of business days. The days are calendar days in
///     <c>InventoryOptions.BusinessTimeZoneId</c>, the same days <c>StoreDailyStockTotals</c> is keyed by.
/// </summary>
[Collection("IntegrationTestCollection")]
public class DailyTotalsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private InventoryOptions Options => Factory.Services.GetRequiredService<IOptions<InventoryOptions>>().Value;

    private DateOnly BusinessToday =>
        TimeProvider.System.GetToday(TimeZoneInfo.FindSystemTimeZoneById(Options.BusinessTimeZoneId));

    private static Uri RouteOf(DefaultIdType storeId, DateOnly from, DateOnly? to = null)
    {
        var query = $"storeId={storeId}&from={from.ToString("O", CultureInfo.InvariantCulture)}";
        if (to is { } day)
        {
            query += $"&to={day.ToString("O", CultureInfo.InvariantCulture)}";
        }

        return new Uri($"/v1/stock-levels/daily-totals?{query}", UriKind.Relative);
    }

    private async Task SeedAsync(DefaultIdType storeId, DateOnly day, int units)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        await StockCounterWriter.AddCommittedAsync(db, storeId, units, day, DateTimeOffset.UtcNow, CancellationToken.None);
    }

    private async Task<List<Response>> GetDaysAsync(Uri route)
    {
        var response = await StockLevelTestData.AuthedClient(Factory).GetAsync(route);
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        return (await response.Content.ReadFromJsonAsync<List<Response>>(JsonSerializerOptions))!;
    }

    [Fact]
    public async Task GetDailyTotals_ReturnsOnlyThatStoresDaysInsideTheRangeOldestFirst()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var otherStoreId = DefaultIdType.CreateVersion7();
        var first = new DateOnly(2026, 3, 10);
        await SeedAsync(storeId, first.AddDays(2), 5);
        await SeedAsync(storeId, first, 3);
        await SeedAsync(storeId, first, 4);
        await SeedAsync(storeId, first.AddDays(-1), 100);
        await SeedAsync(storeId, first.AddDays(3), 100);
        await SeedAsync(otherStoreId, first, 100);

        var days = await GetDaysAsync(RouteOf(storeId, first, first.AddDays(2)));

        Assert.Equal([first, first.AddDays(2)], days.Select(d => d.Day).ToArray());
        Assert.Equal(2, days[0].CommittedReservations);
        Assert.Equal(7, days[0].CommittedUnits);
        Assert.Equal(1, days[1].CommittedReservations);
        Assert.Equal(5, days[1].CommittedUnits);
    }

    [Fact]
    public async Task GetDailyTotals_ToOmitted_StopsAtTodayInTheBusinessTimeZone()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var today = BusinessToday;
        await SeedAsync(storeId, today.AddDays(-1), 1);
        await SeedAsync(storeId, today, 2);
        await SeedAsync(storeId, today.AddDays(1), 3);

        var days = await GetDaysAsync(RouteOf(storeId, today.AddDays(-1)));

        Assert.Equal([today.AddDays(-1), today], days.Select(d => d.Day).ToArray());
    }

    [Fact]
    public async Task GetDailyTotals_RangeLongerThanTheConfiguredCap_ReturnsBadRequest()
    {
        var from = new DateOnly(2026, 1, 1);
        var to = from.AddDays(Options.DailyTotalsMaxRangeDays);

        var response = await StockLevelTestData.AuthedClient(Factory).GetAsync(RouteOf(DefaultIdType.CreateVersion7(), from, to));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetDailyTotals_RangeExactlyAtTheCap_IsAccepted()
    {
        var from = new DateOnly(2026, 1, 1);
        var to = from.AddDays(Options.DailyTotalsMaxRangeDays - 1);

        var days = await GetDaysAsync(RouteOf(DefaultIdType.CreateVersion7(), from, to));

        Assert.Empty(days);
    }

    [Fact]
    public async Task GetDailyTotals_FromAfterTo_ReturnsBadRequest()
    {
        var from = new DateOnly(2026, 3, 10);

        var response = await StockLevelTestData.AuthedClient(Factory)
            .GetAsync(RouteOf(DefaultIdType.CreateVersion7(), from, from.AddDays(-1)));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetDailyTotals_EmptyStoreId_ReturnsBadRequest()
    {
        var from = new DateOnly(2026, 3, 10);

        var response = await StockLevelTestData.AuthedClient(Factory).GetAsync(RouteOf(DefaultIdType.Empty, from, from));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetDailyTotals_WithoutAuthentication_ReturnsUnauthorized()
    {
        var from = new DateOnly(2026, 3, 10);

        var response = await Factory.CreateClient().GetAsync(RouteOf(DefaultIdType.CreateVersion7(), from, from));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
