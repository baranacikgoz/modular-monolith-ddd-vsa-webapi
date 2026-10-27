using System.Net;
using System.Net.Http.Headers;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     Release goes through the warehouse connection of the product's store. The real gateways, pipelines and encryption
///     run; only the network is the factory's fake warehouse.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ReleaseTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    private static Uri RouteOf(StockReservationId id) => new($"/v1/stock-reservations/{id}/release", UriKind.Relative);

    private async Task<StockReservation> SeedConnectedReservationAsync(
        WarehouseProvider provider = WarehouseProvider.JsonApi, bool deactivated = false, bool committed = false)
    {
        var storeId = DefaultIdType.CreateVersion7();
        await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, provider, deactivated);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        return await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId, committed);
    }

    private async Task<HttpResponseMessage> ReleaseAsync(StockReservation reservation)
    {
        return await StockLevelTestData.AuthedClient(Factory).PostAsync(RouteOf(reservation.Id), null);
    }

    [Fact]
    public async Task Release_ActiveReservation_ReleasesThroughTheJsonProviderOfTheProductsStore()
    {
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });
        var reservation = await SeedConnectedReservationAsync();

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        var sent = Assert.Single(factory.Warehouse.Requests);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/releases"), sent.Uri);
        Assert.Equal($"Bearer {WarehouseConnectionTestData.ApiCredential}", sent.Authorization);
    }

    [Fact]
    public async Task Release_ProductOfAStoreOnTheOtherProvider_UsesThatProvidersWire()
    {
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "wh-ref-2" } });
        var reservation = await SeedConnectedReservationAsync(WarehouseProvider.FormApi);

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var sent = Assert.Single(factory.Warehouse.Requests);
        Assert.Equal("application/x-www-form-urlencoded", sent.ContentType);
        Assert.Equal(WarehouseConnectionTestData.ApiCredential, sent.ApiKey);
        Assert.Null(sent.Authorization);
    }

    [Fact]
    public async Task Release_AlreadyReleased_IsAnIdempotentNoOpWithoutASecondWarehouseCall()
    {
        factory.Warehouse.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });
        var reservation = await SeedConnectedReservationAsync();

        var first = await ReleaseAsync(reservation);
        var second = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        Assert.Single(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Release_CommittedReservation_ReturnsBadRequestWithoutAWarehouseCall()
    {
        var reservation = await SeedConnectedReservationAsync(committed: true);

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Release_StoreWithoutAnActiveConnection_ReturnsNotFoundAndLeavesNoAttemptMarker()
    {
        var reservation = await SeedConnectedReservationAsync(deactivated: true);

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.LastReleaseAttemptReference);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Release_WarehouseRefuses_ReturnsBadGatewayAndAbandonsTheMarkerSoARetryCanProceed()
    {
        factory.Warehouse.Responder = _ => new HttpResponseMessage(HttpStatusCode.InternalServerError);
        var reservation = await SeedConnectedReservationAsync();

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.BadGateway, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.LastReleaseAttemptReference);
        Assert.Single(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Release_WarehouseThrottles_ReturnsTooManyRequestsAndAbandonsTheMarker()
    {
        factory.Warehouse.Responder = _ =>
        {
            var throttled = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            throttled.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(30));
            return throttled;
        };
        var reservation = await SeedConnectedReservationAsync();

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.TooManyRequests, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Null(reloaded.LastReleaseAttemptReference);
    }

    [Fact]
    public async Task Release_NoAnswerFromTheWarehouse_ReturnsGatewayTimeoutKeepsTheMarkerAndIsNeverSentAgain()
    {
        factory.Warehouse.Responder = _ => throw new HttpRequestException("Connection reset by peer.");
        var reservation = await SeedConnectedReservationAsync();

        var response = await ReleaseAsync(reservation);
        var again = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.GatewayTimeout, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.NotNull(reloaded.LastReleaseAttemptReference);
        // The marker refuses a second blind release; the warehouse saw exactly one request.
        Assert.Equal(HttpStatusCode.BadRequest, again.StatusCode);
        Assert.Single(factory.Warehouse.Requests);
    }

    /// <summary>Plays the warehouse webhook that lands while the release call is in flight and turns the reservation read-only.</summary>
    private async Task TurnReadOnlyWhileTheProviderIsCalledAsync(StockReservationId id)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var reservation = await db.StockReservations.SingleAsync(r => r.Id == id);
        reservation.RecordUnrecognizedProviderStatus("partially_picked", DateTimeOffset.UtcNow);
        await db.SaveChangesAsync();
    }

    [Fact]
    public async Task Release_WebhookTurnsTheReservationReadOnlyWhileTheProviderReleases_MirrorsTheReleaseAndSucceeds()
    {
        var reservation = await SeedConnectedReservationAsync();
        factory.Warehouse.Responder = _ =>
        {
            TurnReadOnlyWhileTheProviderIsCalledAsync(reservation.Id).GetAwaiter().GetResult();
            return FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });
        };

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.True(reloaded.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(ReservationStatus.Released, reloaded.ProviderStatus);
        Assert.Null(reloaded.LastReleaseAttemptReference);
        Assert.Single(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Release_WebhookAlreadyReleasedTheReservationWhileTheProviderReleases_SucceedsWithoutReleasingTwice()
    {
        var reservation = await SeedConnectedReservationAsync();
        factory.Warehouse.Responder = _ =>
        {
            ReleaseWhileTheProviderIsCalledAsync(reservation.Id).GetAwaiter().GetResult();
            return FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-1" });
        };

        var response = await ReleaseAsync(reservation);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.False(reloaded.IsReadOnly);
    }

    /// <summary>Plays the warehouse webhook that reports the same release while the release call is in flight.</summary>
    private async Task ReleaseWhileTheProviderIsCalledAsync(StockReservationId id)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var reservation = await db.StockReservations.SingleAsync(r => r.Id == id);
        reservation.ReceiveProviderStatus(ReservationStatus.Released, null, DateTimeOffset.UtcNow);
        await db.SaveChangesAsync();
    }
}
