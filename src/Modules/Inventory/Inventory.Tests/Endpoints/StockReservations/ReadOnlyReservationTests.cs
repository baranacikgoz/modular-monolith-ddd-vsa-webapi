using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.DomainEvents.v1;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.StockReservations;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
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

    private Task<HttpResponseMessage> ResolveAsync(StockReservationId id, object body)
    {
        return StockLevelTestData.AuthedClient(Factory).PostAsJsonAsync(
            new Uri($"/v1/stock-reservations/{id}/resolve-read-only", UriKind.Relative), body);
    }

    [Fact]
    public async Task ResolveReadOnly_ToReleased_LiftsTheFlagReleasesTheReservationAndTheOrdinaryCommandsWorkAgain()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await ResolveAsync(reservation.Id, new { targetStatus = "Released" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.False(reloaded.IsReadOnly);
        Assert.Null(reloaded.ProviderStatus);
        var resolved = Assert.Single(await RecordedEventsAsync<V1StockReservationReadOnlyResolvedDomainEvent>(reservation.Id));
        Assert.Equal(V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Released, resolved.ResolvedStatus);
        Assert.Single(await RecordedEventsAsync<V1StockReservationReleasedDomainEvent>(reservation.Id));

        // Released is final for the commands, but a command no longer answers ReservationReadOnly.
        var release = await StockLevelTestData.AuthedClient(Factory)
            .PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);
        Assert.Equal(HttpStatusCode.NoContent, release.StatusCode);
    }

    [Fact]
    public async Task ResolveReadOnly_ToCommitted_CommitsWithTheReferenceAndQueuesTheCommittedIntegrationEvent()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await ResolveAsync(reservation.Id, new { targetStatus = "Committed", providerReference = "WH-7" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal("WH-7", reloaded.ProviderReference);
        Assert.False(reloaded.IsReadOnly);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        Assert.Single(queued, e => e.ReservationId == reservation.Id.Value);
    }

    [Fact]
    public async Task ResolveReadOnly_ToCommittedWithoutAReference_ReturnsBadRequestAndKeepsTheReservationReadOnly()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await ResolveAsync(reservation.Id, new { targetStatus = "Committed" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(StockReservationErrors.ProviderReferenceRequired.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.True(reloaded.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
    }

    [Fact]
    public async Task ResolveReadOnly_WritableReservation_ReturnsConflict()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);

        var response = await ResolveAsync(reservation.Id, new { targetStatus = "Released" });

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Contains(StockReservationErrors.NotReadOnly.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task ResolveReadOnly_TargetThatIsNotAResolution_ReturnsBadRequestFromTheValidator()
    {
        var reservation = await SeedReadOnlyReservationAsync();

        var response = await ResolveAsync(reservation.Id, new { targetStatus = "Active" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.True(reloaded.IsReadOnly);
    }

    [Fact]
    public async Task ResolveReadOnly_UnknownReservation_ReturnsNotFound()
    {
        var response = await ResolveAsync(StockReservationId.New(), new { targetStatus = "Released" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<List<T>> RecordedEventsAsync<T>(StockReservationId id) where T : class
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var entries = await db.AuditLog.AsNoTracking().Where(a => a.AggregateId == id.Value).ToListAsync();
        return entries.Select(a => a.Event).OfType<T>().ToList();
    }
}
