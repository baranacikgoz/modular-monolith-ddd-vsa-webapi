using System.Diagnostics.Metrics;
using System.Net;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.DomainEvents.v1;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Telemetry;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     A status feed arrives in any order. A delivery that carries the provider's timestamp is applied in the order of that
///     timestamp; one older than what was applied before is acknowledged and recorded, and changes nothing. A delivery without
///     a timestamp is applied in the order it arrives.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WebhookOrderingTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DateTimeOffset T0 = new(2026, 10, 27, 10, 0, 0, TimeSpan.Zero);

    private sealed record Scenario(WarehouseConnection Connection, string Secret, StockReservation Reservation);

    private async Task<Scenario> SeedAsync()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (connection, secret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId);
        return new Scenario(connection, secret, reservation);
    }

    private Task<HttpResponseMessage> PostAsync(
        Scenario scenario, string deliveryId, string? status, DateTimeOffset? occurredOn, string? providerReference = null)
    {
        return WarehouseWebhookSender.PostAsync(
            Factory.CreateClient(), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, deliveryId, providerReference,
            status: status, occurredOn: occurredOn);
    }

    private Task<StockReservation> ReloadAsync(Scenario scenario)
    {
        return WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
    }

    private async Task<List<T>> RecordedEventsAsync<T>(StockReservationId id) where T : class
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var entries = await db.AuditLog.AsNoTracking().Where(a => a.AggregateId == id.Value).ToListAsync();
        return entries.Select(a => a.Event).OfType<T>().ToList();
    }

    [Fact]
    public async Task Webhook_OlderDeliveryArrivesAfterANewerOne_IsAcknowledgedAndChangesNothing()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-2", "released", T0.AddSeconds(2));

        var late = await PostAsync(scenario, "delivery-1", "confirmed", T0.AddSeconds(1), providerReference: "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, late.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.Null(reloaded.ProviderReference);
        Assert.Equal(T0.AddSeconds(2), reloaded.LastProviderEventOn);
        Assert.Empty(await RecordedEventsAsync<V1StockReservationCommittedDomainEvent>(scenario.Reservation.Id));
    }

    [Fact]
    public async Task Webhook_DeliveriesInOrder_AreAppliedAndTheLatestTimestampIsKept()
    {
        var scenario = await SeedAsync();

        var held = await PostAsync(scenario, "delivery-1", "held", T0.AddSeconds(1));
        var confirmed = await PostAsync(scenario, "delivery-2", "confirmed", T0.AddSeconds(2), providerReference: "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, held.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, confirmed.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal(T0.AddSeconds(2), reloaded.LastProviderEventOn);
        Assert.Equal(2, (await RecordedEventsAsync<V1StockReservationProviderEventTimeRecordedDomainEvent>(scenario.Reservation.Id)).Count);
    }

    [Fact]
    public async Task Webhook_StaleDeliveryIsRecorded_SoItsRedeliveryIsADuplicateNotAGainedChance()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-2", "released", T0.AddSeconds(2));
        await PostAsync(scenario, "delivery-1", "confirmed", T0.AddSeconds(1), providerReference: "provider-ref-1");
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var recorded = await db.WebhookDeliveries.AsNoTracking()
            .CountAsync(d => d.ConnectionId == scenario.Connection.Id && d.ReservationId == scenario.Reservation.Id);

        Assert.Equal(2, recorded);
    }

    [Fact]
    public async Task Webhook_StaleDeliveryCarryingAnUnrecognizedWord_DoesNotTurnTheReservationReadOnly()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-2", "held", T0.AddSeconds(2));

        var late = await PostAsync(scenario, "delivery-1", "partially_picked", T0.AddSeconds(1));

        Assert.Equal(HttpStatusCode.NoContent, late.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.False(reloaded.IsReadOnly);
        Assert.Empty(await RecordedEventsAsync<V1StockReservationProviderStatusUnrecognizedDomainEvent>(scenario.Reservation.Id));
    }

    [Fact]
    public async Task Webhook_ReadOnlyReservationGetsAnOlderStatus_KeepsTheNewerMirror()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", "partially_picked", T0.AddSeconds(1));
        await PostAsync(scenario, "delivery-3", "confirmed", T0.AddSeconds(3), providerReference: "provider-ref-1");

        var late = await PostAsync(scenario, "delivery-2", "released", T0.AddSeconds(2));

        Assert.Equal(HttpStatusCode.NoContent, late.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.Equal(ReservationStatus.Committed, reloaded.ProviderStatus);
        Assert.Equal(T0.AddSeconds(3), reloaded.LastProviderEventOn);
    }

    [Fact]
    public async Task Webhook_SameTimestampTwice_AppliesBoth()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", "held", T0);

        var second = await PostAsync(scenario, "delivery-2", "released", T0);

        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        Assert.Equal(ReservationStatus.Released, (await ReloadAsync(scenario)).Status);
    }

    [Fact]
    public async Task Webhook_DeliveryWithoutATimestampAfterAStampedOne_IsAppliedAsBefore()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", "held", T0.AddSeconds(5));

        var unstamped = await PostAsync(scenario, "delivery-2", "released", occurredOn: null);

        Assert.Equal(HttpStatusCode.NoContent, unstamped.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.Equal(T0.AddSeconds(5), reloaded.LastProviderEventOn);
    }

    [Fact]
    public async Task Webhook_TimestampWithAnOffset_IsKeptAsTheSameInstant()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", "held", new DateTimeOffset(2026, 10, 27, 13, 0, 5, TimeSpan.FromHours(3)));

        var older = await PostAsync(scenario, "delivery-2", "released", T0.AddSeconds(4));

        Assert.Equal(HttpStatusCode.NoContent, older.StatusCode);
        var reloaded = await ReloadAsync(scenario);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(T0.AddSeconds(5), reloaded.LastProviderEventOn);
    }

    [Fact]
    public async Task Webhook_StaleDelivery_IsCountedWithTheStaleOutcome()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-2", "released", T0.AddSeconds(2));
        var staleOutcomes = 0;
        using var listener = new MeterListener();
        listener.InstrumentPublished = (instrument, meterListener) =>
        {
            if (ReferenceEquals(instrument, InventoryTelemetry.WebhookDeliveries))
            {
                meterListener.EnableMeasurementEvents(instrument);
            }
        };
        listener.SetMeasurementEventCallback<long>((_, measurement, tags, _) =>
        {
            if (tags.ToArray().Any(tag => tag is { Key: "outcome", Value: "Stale" }))
            {
                Interlocked.Add(ref staleOutcomes, (int)measurement);
            }
        });
        listener.Start();

        await PostAsync(scenario, "delivery-1", "confirmed", T0.AddSeconds(1), providerReference: "provider-ref-1");

        Assert.Equal(1, Volatile.Read(ref staleOutcomes));
    }
}
