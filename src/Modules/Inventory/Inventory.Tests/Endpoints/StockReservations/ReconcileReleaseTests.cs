using System.Net;
using System.Net.Http.Json;
using System.Web;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.StockReservations;
using Inventory.Tests.Support;
using MassTransit;
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
        Assert.Equal($"/api/releases/{reservation.Id}/status", asked.Uri!.AbsolutePath);
    }

    [Fact]
    public async Task Reconcile_WarehouseHasNoSuchRelease_AbandonsTheMarkerSoTheReleaseCanBeTriedAgain()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.NotFound, new { error = "release_not_found" });

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

    // A wrong base address answers 404 from some other server. That is not the warehouse saying the release was never made,
    // so the marker stays and a second release is not let through.
    [Fact]
    public async Task Reconcile_BareNotFound_ReturnsBadGatewayAndKeepsTheMarker()
    {
        var reservation = await SeedUnansweredReleaseAsync();
        factory.Warehouse.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var response = await ReconcileAsync(reservation);

        Assert.Equal(HttpStatusCode.BadGateway, response.StatusCode);
        Assert.Contains("WarehouseGatewayEndpointNotFound", await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.NotNull(reloaded.LastReleaseAttemptReference);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        var again = await StockLevelTestData.AuthedClient(Factory).PostAsync(ReleaseRoute(reservation.Id), null);
        Assert.Equal(HttpStatusCode.BadRequest, again.StatusCode);
    }

    private static string ReservationAskedAbout(Uri uri, WarehouseProvider provider)
    {
        return provider == WarehouseProvider.JsonApi
            ? uri.Segments[^2].TrimEnd('/')
            : HttpUtility.ParseQueryString(uri.Query)["ref"]!;
    }

    private static HttpResponseMessage WarehouseThatReleasedOnly(
        HttpRequestMessage request, WarehouseProvider provider, StockReservationId released)
    {
        var knowsIt = ReservationAskedAbout(request.RequestUri!, provider) == released.Value.ToString();
        if (provider == WarehouseProvider.JsonApi)
        {
            return knowsIt
                ? FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" })
                : FakeWarehouseHandler.Json(HttpStatusCode.NotFound, new { error = "release_not_found" });
        }

        return knowsIt
            ? FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "wh-ref-1" } })
            : FakeWarehouseHandler.Json(HttpStatusCode.NotFound, new { result = new { error = "hold_not_found" } });
    }

    // The warehouse is asked about the reservation, not about the product: when it released one of two reservations of the
    // same product, the other one's reconcile must not report that release as its own.
    [Theory]
    [InlineData(WarehouseProvider.JsonApi)]
    [InlineData(WarehouseProvider.FormApi)]
    public async Task Reconcile_TwoReservationsOfOneProduct_AreReconciledIndependently(WarehouseProvider provider)
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, provider);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var released = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        var notReleased = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        factory.Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");
        foreach (var reservation in new[] { released, notReleased })
        {
            var unanswered = await StockLevelTestData.AuthedClient(Factory).PostAsync(ReleaseRoute(reservation.Id), null);
            Assert.Equal(HttpStatusCode.GatewayTimeout, unanswered.StatusCode);
        }

        factory.Warehouse.Reset();
        factory.Warehouse.Responder = request => WarehouseThatReleasedOnly(request, provider, released.Id);

        var releasedAnswer = await ReconcileAsync(released);
        var notReleasedAnswer = await ReconcileAsync(notReleased);

        Assert.Equal(HttpStatusCode.OK, releasedAnswer.StatusCode);
        Assert.Equal(HttpStatusCode.OK, notReleasedAnswer.StatusCode);
        Assert.Equal(
            ReleaseReconcileOutcome.Released, (await releasedAnswer.Content.ReadFromJsonAsync<ReconcileResponse>(JsonSerializerOptions))!.Outcome);
        Assert.Equal(
            ReleaseReconcileOutcome.NotReleased, (await notReleasedAnswer.Content.ReadFromJsonAsync<ReconcileResponse>(JsonSerializerOptions))!.Outcome);
        var reloadedReleased = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, released.Id);
        var reloadedNotReleased = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, notReleased.Id);
        Assert.Equal(ReservationStatus.Released, reloadedReleased.Status);
        Assert.Equal(ReservationStatus.Active, reloadedNotReleased.Status);
        Assert.Null(reloadedNotReleased.LastReleaseAttemptReference);
        Assert.Equal(
            [released.Id.Value.ToString(), notReleased.Id.Value.ToString()],
            factory.Warehouse.Requests.Select(r => ReservationAskedAbout(r.Uri!, provider)).ToArray());
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

    /// <summary>The consumer is picked up by the Host's assembly scan; this is the check that a rename or a moved namespace does not silently drop its queue.</summary>
    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(ReconcileStockReleaseHandler)}", StringComparison.Ordinal));
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
