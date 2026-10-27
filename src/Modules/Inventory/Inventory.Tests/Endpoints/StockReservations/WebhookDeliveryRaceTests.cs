using System.Net;
using Common.Application.Auth;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.IntegrationEvents;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Webhooks;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     The warehouse delivers at least once and may deliver the same delivery twice at the same moment. Whatever order the
///     two requests interleave in, the delivery is applied once and recorded once, and the other request is acknowledged.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WebhookDeliveryRaceTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string DeliveryId = "delivery-race";
    private const string ProviderReference = "provider-ref-race";

    /// <summary>
    ///     Just before the first save that carries a new delivery row, a competing request commits its own work through a scope
    ///     of its own, so the save under test loses the race exactly once.
    /// </summary>
    private sealed class CompetingRequestInterceptor(Func<Task> compete) : SaveChangesInterceptor
    {
        private bool _competed;

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (!_competed && eventData.Context!.ChangeTracker.Entries<WebhookDelivery>().Any(e => e.State == EntityState.Added))
            {
                _competed = true;
                await compete();
            }

            return result;
        }
    }

    private async Task<(WarehouseConnection Connection, StockReservation Reservation, string Secret)> SeedAsync(bool committed)
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (connection, secret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId, committed);
        return (connection, reservation, secret);
    }

    private static InventoryDbContext NewContext(IServiceProvider services, CompetingRequestInterceptor interceptor)
    {
        var options = new DbContextOptionsBuilder<InventoryDbContext>(services.GetRequiredService<DbContextOptions<InventoryDbContext>>())
            .AddInterceptors(interceptor)
            .Options;

        return new InventoryDbContext(
            options, services.GetRequiredService<TimeProvider>(), services.GetRequiredService<ICurrentUser>(),
            services.GetRequiredService<ILogger<BaseDbContext>>(), services.GetRequiredService<EventDispatcher>(),
            services.GetRequiredService<IntegrationEventOutbox>());
    }

    /// <summary>Delivers through an intake whose save is overtaken by <paramref name="competingDeliveryId" /> delivered first.</summary>
    private async Task<Result<WebhookDeliveryOutcome>> DeliverOvertakenAsync(
        WarehouseConnection connection, StockReservation reservation, string ownDeliveryId, string competingDeliveryId,
        string providerReference = ProviderReference)
    {
        var interceptor = new CompetingRequestInterceptor(async () =>
        {
            await using var competingScope = Factory.Services.CreateAsyncScope();
            await competingScope.ServiceProvider.GetRequiredService<WarehouseWebhookIntake>()
                .ReceiveAsync(connection, competingDeliveryId, reservation.Id, providerReference, CancellationToken.None);
        });

        await using var scope = Factory.Services.CreateAsyncScope();
        await using var racingDb = NewContext(scope.ServiceProvider, interceptor);
        var intake = new WarehouseWebhookIntake(racingDb, TimeProvider.System);

        return await intake.ReceiveAsync(connection, ownDeliveryId, reservation.Id, providerReference, CancellationToken.None);
    }

    private async Task<int> CountDeliveriesAsync(WarehouseConnectionId connectionId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.WebhookDeliveries.AsNoTracking().CountAsync(d => d.ConnectionId == connectionId);
    }

    [Fact]
    public async Task Webhook_ManyIdenticalDeliveriesAtOnce_AreAllAcknowledgedAndApplyOneEffect()
    {
        var (connection, reservation, secret) = await SeedAsync(committed: false);
        var clients = Enumerable.Range(0, 16).Select(_ => Factory.CreateClient()).ToList();

        var responses = await Task.WhenAll(clients.Select(client => WarehouseWebhookSender.PostAsync(
            client, connection.Id, secret, reservation.Id, DeliveryId, ProviderReference)));

        Assert.All(responses, r => Assert.Equal(HttpStatusCode.NoContent, r.StatusCode));
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal(1, await CountDeliveriesAsync(connection.Id));
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        Assert.Single(queued, e => e.ReservationId == reservation.Id.Value);
    }

    // The effect here is a no-op (the reservation is already committed with this reference), so no row version stops the
    // second insert: only the unique index on (connection, delivery id) does.
    [Fact]
    public async Task ReceiveAsync_SameDeliveryOvertakesOnAnAlreadyCommittedReservation_LosesOnTheUniqueIndexAndIsADuplicate()
    {
        var (connection, reservation, _) = await SeedAsync(committed: true);
        var committedReference = (await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id)).ProviderReference!;

        var result = await DeliverOvertakenAsync(connection, reservation, DeliveryId, competingDeliveryId: DeliveryId, committedReference);

        Assert.Equal(WebhookDeliveryOutcome.Duplicate, result.Value);
        Assert.Equal(1, await CountDeliveriesAsync(connection.Id));
    }

    [Fact]
    public async Task ReceiveAsync_SameDeliveryOvertakesOnAnActiveReservation_IsADuplicateAndTheEffectHappenedOnce()
    {
        var (connection, reservation, _) = await SeedAsync(committed: false);

        var result = await DeliverOvertakenAsync(connection, reservation, DeliveryId, competingDeliveryId: DeliveryId);

        Assert.Equal(WebhookDeliveryOutcome.Duplicate, result.Value);
        Assert.Equal(1, await CountDeliveriesAsync(connection.Id));
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        Assert.Single(queued, e => e.ReservationId == reservation.Id.Value);
    }

    // A different delivery committed the reservation first: this one is not a duplicate, so the conflict (a row version or an
    // audit key clash, whichever statement meets the winner's commit first) is surfaced for the warehouse to deliver it again,
    // when the commit is a benign duplicate, and nothing of it was recorded.
    [Fact]
    public async Task ReceiveAsync_AnotherDeliveryCommittedTheReservationFirst_SurfacesTheConflictAndRecordsNothing()
    {
        var (connection, reservation, _) = await SeedAsync(committed: false);

        await Assert.ThrowsAnyAsync<DbUpdateException>(
            () => DeliverOvertakenAsync(connection, reservation, ownDeliveryId: "delivery-own", competingDeliveryId: "delivery-other"));

        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var recorded = await db.WebhookDeliveries.AsNoTracking()
            .Where(d => d.ConnectionId == connection.Id).Select(d => d.DeliveryId).ToListAsync();
        Assert.Equal(["delivery-other"], recorded);
    }
}
