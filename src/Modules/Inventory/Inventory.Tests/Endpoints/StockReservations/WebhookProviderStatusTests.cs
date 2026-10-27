using System.Net;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.DomainEvents.v1;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ReservationConstants = Inventory.Domain.StockReservations.Constants;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     The status a warehouse reports for a reservation is mapped from its raw word. A word nobody mapped is acknowledged and
///     recorded, never an error: the reservation stops following the provider and only mirrors it from then on.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WebhookProviderStatusTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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

    private Task<HttpResponseMessage> PostAsync(
        Scenario scenario, string deliveryId, string? providerReference, string? status)
    {
        return WarehouseWebhookSender.PostAsync(
            Factory.CreateClient(), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, deliveryId, providerReference,
            status: status);
    }

    private async Task<List<T>> RecordedEventsAsync<T>(StockReservationId id) where T : class
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var entries = await db.AuditLog.AsNoTracking().Where(a => a.AggregateId == id.Value).ToListAsync();
        return entries.Select(a => a.Event).OfType<T>().ToList();
    }

    [Fact]
    public async Task Webhook_ReleasedStatus_ReleasesTheReservationWithoutAProviderReference()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", providerReference: null, status: "released");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded.Status);
        Assert.False(reloaded.IsReadOnly);
        Assert.Single(await RecordedEventsAsync<V1StockReservationReleasedDomainEvent>(scenario.Reservation.Id));
    }

    [Fact]
    public async Task Webhook_UnrecognizedStatus_IsAcknowledgedRecordedAndTurnsTheReservationReadOnly()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", providerReference: null, status: "partially_picked");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.True(reloaded.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(ReservationStatus.Unknown, reloaded.ProviderStatus);
        var recorded = Assert.Single(await RecordedEventsAsync<V1StockReservationProviderStatusUnrecognizedDomainEvent>(scenario.Reservation.Id));
        Assert.Equal("partially_picked", recorded.RawStatus);
    }

    [Fact]
    public async Task Webhook_UnrecognizedStatusLongerThanTheBound_IsStillAcknowledgedAndRecordedCut()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(
            scenario, "delivery-1", providerReference: null, status: new string('w', ReservationConstants.ProviderStatusMaxLength * 3));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var recorded = Assert.Single(await RecordedEventsAsync<V1StockReservationProviderStatusUnrecognizedDomainEvent>(scenario.Reservation.Id));
        Assert.Equal(ReservationConstants.ProviderStatusMaxLength, recorded.RawStatus.Length);
    }

    [Fact]
    public async Task Webhook_KnownStatusAfterTheReservationTurnedReadOnly_OnlyMirrorsItWithNoLocalEffect()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", providerReference: null, status: "partially_picked");

        var response = await PostAsync(scenario, "delivery-2", providerReference: "provider-ref-1", status: "confirmed");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Null(reloaded.ProviderReference);
        Assert.Equal(ReservationStatus.Committed, reloaded.ProviderStatus);
        Assert.Single(await RecordedEventsAsync<V1StockReservationProviderStatusMirroredDomainEvent>(scenario.Reservation.Id));
        Assert.Empty(await RecordedEventsAsync<V1StockReservationCommittedDomainEvent>(scenario.Reservation.Id));
    }

    [Fact]
    public async Task Webhook_SecondUnrecognizedStatusOnAReadOnlyReservation_IsAcknowledgedAndRecordedAgain()
    {
        var scenario = await SeedAsync();
        await PostAsync(scenario, "delivery-1", providerReference: null, status: "partially_picked");

        var response = await PostAsync(scenario, "delivery-2", providerReference: null, status: "in_transit");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var recorded = await RecordedEventsAsync<V1StockReservationProviderStatusUnrecognizedDomainEvent>(scenario.Reservation.Id);
        Assert.Equal(["partially_picked", "in_transit"], recorded.OrderBy(e => e.ReceivedAt).Select(e => e.RawStatus));
    }

    [Fact]
    public async Task Webhook_ConfirmedStatusWithoutAProviderReference_ReturnsBadRequestAndChangesNothing()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", providerReference: null, status: "confirmed");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.False(reloaded.IsReadOnly);
    }

    [Fact]
    public async Task Webhook_BodyWithoutAStatus_IsTheOriginalConfirmationDelivery()
    {
        var scenario = await SeedAsync();

        var response = await PostAsync(scenario, "delivery-1", providerReference: "provider-ref-1", status: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
    }
}
