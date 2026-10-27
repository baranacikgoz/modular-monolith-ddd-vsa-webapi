using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.StockReservations;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using GetResponse = Inventory.Endpoints.StockReservations.v1.Get.Response;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     A reservation that met a status its provider reported in a word nobody mapped is read-only: no local command applies to
///     it, and neither does the expiry sweep. The provider's last word stays readable next to the local status.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ReadOnlyReservationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    private async Task<StockReservation> SeedReadOnlyReservationAsync(DateTimeOffset? deadline = null)
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        return await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId, readOnly: true, deadline: deadline);
    }

    [Fact]
    public async Task Commit_ReadOnlyReservation_ReturnsConflictAndChangesNothing()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await StockLevelTestData.AuthedClient(Factory).PostAsJsonAsync(
            new Uri($"/v1/stock-reservations/{reservation.Id}/commit", UriKind.Relative), new { providerReference = "WH-1" });

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Contains(StockReservationErrors.ReservationReadOnly.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.ProviderReference);
    }

    [Fact]
    public async Task Release_ReadOnlyReservation_ReturnsConflictWithoutAWarehouseCallOrAMarker()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await StockLevelTestData.AuthedClient(Factory)
            .PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.LastReleaseAttemptReference);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Get_ReadOnlyReservation_ShowsTheFlagAndTheProvidersLastWordNextToTheLocalStatus()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await StockLevelTestData.AuthedClient(Factory)
            .GetAsync(new Uri($"/v1/stock-reservations/{reservation.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<GetResponse>(JsonSerializerOptions);
        Assert.True(body!.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, body.Status);
        Assert.Equal(ReservationStatus.Unknown, body.ProviderStatus);
    }

    [Fact]
    public async Task Sweep_ReadOnlyReservationPastItsDeadline_IsLeftAlone()
    {
        var reservation = await SeedReadOnlyReservationAsync(DateTimeOffset.UtcNow.AddDays(-1));

        using var scope = Factory.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<StockReservationExpirySweepService>().SweepAsync();

        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
    }
}
