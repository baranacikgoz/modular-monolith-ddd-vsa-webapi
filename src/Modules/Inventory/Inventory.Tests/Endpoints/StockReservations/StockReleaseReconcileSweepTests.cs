using Common.Application.EventBus;
using Common.Application.Options;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.StockReservations;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     The sweep only publishes: one reconcile message per reservation that is active, writable and holds a release attempt
///     marker older than the configured age. It never calls the warehouse.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockReleaseReconcileSweepTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.Warehouse.Reset();
    }

    private async Task<StockReservation> SeedWithMarkerAsync(
        TimeSpan markerAge, bool readOnly = false, bool released = false, bool markerWithoutTime = false)
    {
        var reservation = StockReservation.Reserve(DefaultIdType.CreateVersion7(), 3, DateTimeOffset.UtcNow.AddDays(1));
        reservation.BeginReleaseAttempt(DateTimeOffset.UtcNow - markerAge);
        if (readOnly)
        {
            reservation.RecordUnrecognizedProviderStatus("partially_picked", DateTimeOffset.UtcNow);
        }

        if (released)
        {
            reservation.Release(DateTimeOffset.UtcNow);
        }

        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();

        if (markerWithoutTime)
        {
            await db.Database.ExecuteSqlInterpolatedAsync(
                $"""UPDATE "Inventory"."StockReservations" SET "LastReleaseAttemptOn" = NULL WHERE "Id" = {reservation.Id.Value}""");
        }

        return reservation;
    }

    private async Task SweepAsync(Action<InventoryOptions>? configure = null)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var sweep = new StockReleaseReconcileSweepService(
            scope.ServiceProvider.GetRequiredService<IInventoryDbContext>(),
            scope.ServiceProvider.GetRequiredService<IIntegrationEventOutbox>(),
            TimeProvider.System,
            TestInventoryOptions.Wrapped(configure),
            NullLogger<StockReleaseReconcileSweepService>.Instance);
        await sweep.SweepAsync();
    }

    private async Task<List<DefaultIdType>> QueuedReservationIdsAsync()
    {
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReleaseReconcileRequestedIntegrationEvent>(Factory.Services);
        return [.. queued.Select(e => e.ReservationId)];
    }

    [Fact]
    public async Task Sweep_OnlyActiveWritableReservationsWithAnOldMarker_AreQueued()
    {
        var old = await SeedWithMarkerAsync(TimeSpan.FromHours(1));
        var withoutTime = await SeedWithMarkerAsync(TimeSpan.FromHours(1), markerWithoutTime: true);
        await SeedWithMarkerAsync(TimeSpan.FromSeconds(30));
        await SeedWithMarkerAsync(TimeSpan.FromHours(1), readOnly: true);
        await SeedWithMarkerAsync(TimeSpan.FromHours(1), released: true);
        await SeedWithoutMarkerAsync();

        await SweepAsync();

        var queued = await QueuedReservationIdsAsync();
        Assert.Equal(2, queued.Count);
        Assert.Contains(old.Id.Value, queued);
        Assert.Contains(withoutTime.Id.Value, queued);
        Assert.Empty(factory.Warehouse.Requests);
    }

    [Fact]
    public async Task Sweep_MoreReservationsThanOnePage_QueuesEveryOne()
    {
        for (var i = 0; i < 5; i++)
        {
            await SeedWithMarkerAsync(TimeSpan.FromHours(1));
        }

        await SweepAsync(o => o.SweepPageSize = 2);

        Assert.Equal(5, (await QueuedReservationIdsAsync()).Distinct().Count());
    }

    [Fact]
    public async Task Sweep_NothingStuck_QueuesNothing()
    {
        await SeedWithoutMarkerAsync();

        await SweepAsync();

        Assert.Empty(await QueuedReservationIdsAsync());
    }

    private async Task SeedWithoutMarkerAsync()
    {
        var reservation = StockReservation.Reserve(DefaultIdType.CreateVersion7(), 3, DateTimeOffset.UtcNow.AddDays(1));
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();
    }
}
