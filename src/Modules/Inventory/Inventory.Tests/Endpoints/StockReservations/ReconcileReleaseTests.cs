using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Infrastructure.StockReservations;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ReconcileResponse = Inventory.Endpoints.StockReservations.v1.ReconcileRelease.Response;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     A release the warehouse system did not answer in time keeps its attempt marker. Reconciling asks the warehouse what
///     became of it and applies the answer, from the admin endpoint and from the queue message a recurring sweep publishes.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ReconcileReleaseTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    private static Uri ReleaseRoute(StockReservationId id) => new($"/v1/stock-reservations/{id}/release", UriKind.Relative);

    private static Uri ReconcileRoute(StockReservationId id) => new($"/v1/stock-reservations/{id}/reconcile-release", UriKind.Relative);

    private async Task<StockReservation> SeedConnectedReservationAsync()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        return await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
    }

    /// <summary>A release the warehouse never answered: the marker stays and the warehouse is asked afterwards.</summary>
    private async Task<StockReservation> SeedUnansweredReleaseAsync()
    {
        var reservation = await SeedConnectedReservationAsync();
        factory.Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");
        var released = await StockLevelTestData.AuthedClient(Factory).PostAsync(ReleaseRoute(reservation.Id), null);
        Assert.Equal(HttpStatusCode.GatewayTimeout, released.StatusCode);
        factory.Warehouse.Reset();
        return reservation;
    }

    private Task<HttpResponseMessage> ReconcileAsync(StockReservation reservation)
    {
        return StockLevelTestData.AuthedClient(Factory).PostAsync(ReconcileRoute(reservation.Id), null);
    }

    [Fact]
    public async Task Reconcile_WarehouseReleasedTheHold_CompletesTheReleaseLocally()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<ReconcileResponse>(JsonSerializerOptions);
        Assert.Equal(ReleaseReconcileOutcome.Released, body!.Outcome);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        var asked = Assert.Single(factory.Warehouse.Requests);
        Assert.Equal(HttpMethod.Get, asked.Method);
        Assert.Equal($"/api/releases/{reservation.ProductId}/status", asked.Uri!.AbsolutePath);
    }

    [Fact]
    public async Task Reconcile_WarehouseHasNoSuchRelease_AbandonsTheMarkerSoTheReleaseCanBeTriedAgain()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<ReconcileResponse>(JsonSerializerOptions);
        Assert.Equal(ReleaseReconcileOutcome.NotReleased, body!.Outcome);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.LastReleaseAttemptReference);
        Assert.Null(reloaded.LastReleaseAttemptOn);

        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-2" });
        var again = await StockLevelTestData.AuthedClient(Factory).PostAsync(ReleaseRoute(reservation.Id), null);
        Assert.Equal(HttpStatusCode.NoContent, again.StatusCode);
    }

    [Fact]
    public async Task Reconcile_WarehouseStillDoesNotAnswer_ReturnsGatewayTimeoutAndKeepsTheMarker()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.GatewayTimeout, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.NotNull(reloaded.LastReleaseAttemptReference);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
    }

    [Fact]
    public async Task Reconcile_WarehouseAnswersWithAnError_ReturnsBadGatewayAndKeepsTheMarker()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => new HttpResponseMessage(HttpStatusCode.InternalServerError);

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.BadGateway, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.NotNull(reloaded.LastReleaseAttemptReference);
    }

    [Fact]
    public async Task Reconcile_ReservationWithoutAReleaseAttempt_ReturnsConflictWithoutAWarehouseCall()
    {
        var reservation = await SeedConnectedReservationAsync();

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Contains(StockReservationErrors.NoReleaseAttempt.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Reconcile_AlreadyReleasedReservation_AnswersAlreadyReleasedWithoutAWarehouseCall()
    {
        var reservation = await SeedConnectedReservationAsync();
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });
        await StockLevelTestData.AuthedClient(Factory).PostAsync(ReleaseRoute(reservation.Id), null);
        factory.Warehouse.Reset();

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<ReconcileResponse>(JsonSerializerOptions);
        Assert.Equal(ReleaseReconcileOutcome.AlreadyReleased, body!.Outcome);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Reconcile_ReservationTurnedReadOnlyMeanwhile_MirrorsTheReleaseAndClearsTheMarker()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var tracked = await db.StockReservations.SingleAsync(r => r.Id == reservation.Id);
            tracked.RecordUnrecognizedProviderStatus("partially_picked", DateTimeOffset.UtcNow);
            await db.SaveChangesAsync();
        }

        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.True(reloaded.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(ReservationStatus.Released, reloaded.ProviderStatus);
        Assert.Null(reloaded.LastReleaseAttemptReference);
    }

    [Fact]
    public async Task Reconcile_UnknownReservation_ReturnsNotFound()
    {
        var response = await StockLevelTestData.AuthedClient(Factory).PostAsync(ReconcileRoute(StockReservationId.New()), null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Consume_ReconcileMessage_ReconcilesTheReservation()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });

        await ConsumerHarness.DeliverAsync<ReconcileStockReleaseHandler, StockReleaseReconcileRequestedIntegrationEvent>(
            Factory.Services, new StockReleaseReconcileRequestedIntegrationEvent(reservation.Id.Value));

        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
    }

    [Fact]
    public async Task Consume_WarehouseDoesNotAnswer_DoesNotThrowAndKeepsTheMarker()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        await ConsumerHarness.DeliverAsync<ReconcileStockReleaseHandler, StockReleaseReconcileRequestedIntegrationEvent>(
            Factory.Services, new StockReleaseReconcileRequestedIntegrationEvent(reservation.Id.Value));

        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.NotNull(reloaded.LastReleaseAttemptReference);
    }

    [Fact]
    public async Task Consume_ReservationThatIsGoneOrHasNothingToReconcile_DoesNotThrow()
    {
        var withoutMarker = await SeedConnectedReservationAsync();

        await ConsumerHarness.DeliverAsync<ReconcileStockReleaseHandler, StockReleaseReconcileRequestedIntegrationEvent>(
            Factory.Services, new StockReleaseReconcileRequestedIntegrationEvent(withoutMarker.Id.Value));
        await ConsumerHarness.DeliverAsync<ReconcileStockReleaseHandler, StockReleaseReconcileRequestedIntegrationEvent>(
            Factory.Services, new StockReleaseReconcileRequestedIntegrationEvent(DefaultIdType.CreateVersion7()));

        Assert.Empty(factory.Warehouse.Requests);
    }
}
