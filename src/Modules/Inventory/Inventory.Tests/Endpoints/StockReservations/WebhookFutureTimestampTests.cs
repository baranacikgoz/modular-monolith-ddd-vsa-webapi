using System.Net;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     A delivery is applied in the order of the provider's own timestamp, so a timestamp far ahead of the clock would make
///     every later delivery of that reservation stale. One further ahead than <c>WebhookMaxFutureSkewSeconds</c> is refused
///     with 400 before anything is recorded or applied, so the warehouse can correct its clock and send it again.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WebhookFutureTimestampTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record Scenario(WarehouseConnection Connection, string Secret, StockReservation Reservation);

    private async Task<Scenario> SeedAsync()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (connection, secret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        return new Scenario(connection, secret, reservation);
    }

    private Task<HttpResponseMessage> PostAsync(Scenario scenario, string deliveryId, string status, DateTimeOffset? occurredOn)
    {
        return WarehouseWebhookSender.PostAsync(
            Factory.CreateClient(), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, deliveryId,
            providerReference: "provider-ref-1", status: status, occurredOn: occurredOn);
    }

    private async Task<int> CountDeliveriesAsync(Scenario scenario)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.WebhookDeliveries.AsNoTracking().CountAsync(d => d.ConnectionId == scenario.Connection.Id);
    }

    [Fact]
    public async Task Webhook_TimestampFarInTheFuture_IsRefusedAndNothingIsRecordedOrApplied()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", "confirmed", DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(
            StockReservationErrors.WebhookOccurredOnInTheFuture.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.LastProviderEventOn);
        Assert.Equal(0, await CountDeliveriesAsync(scenario));
    }

    [Fact]
    public async Task Webhook_RefusedFutureTimestamp_DoesNotMakeTheNextDeliveryStale()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", "held", DateTimeOffset.UtcNow.AddDays(1));

        var next = await PostAsync(scenario, "delivery-2", "released", DateTimeOffset.UtcNow.AddSeconds(-5));

        Assert.Equal(HttpStatusCode.NoContent, next.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
    }

    [Fact]
    public async Task Webhook_TimestampSlightlyAhead_IsWithinTheToleranceAndApplied()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", "released", DateTimeOffset.UtcNow.AddSeconds(30));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.Equal(1, await CountDeliveriesAsync(scenario));
    }

    [Fact]
    public async Task Webhook_DeliveryWithoutATimestamp_IsNotAffectedByTheTolerance()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", "released", occurredOn: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
