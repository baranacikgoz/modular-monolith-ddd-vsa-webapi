using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

[Collection("IntegrationTestCollection")]
public class CommitTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Commit_ActiveReservation_ReturnsNoContentAndQueuesTheCommittedIntegrationEvent()
    {
        var productId = DefaultIdType.CreateVersion7();
        var reservation = StockReservation.Reserve(productId, 4, DateTimeOffset.UtcNow.AddDays(1));
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            db.StockReservations.Add(reservation);
            await db.SaveChangesAsync();
        }

        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(
            new Uri($"/v1/stock-reservations/{reservation.Id}/commit", UriKind.Relative),
            new { providerReference = "WH-1" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        var committed = Assert.Single(queued, e => e.ReservationId == reservation.Id.Value);
        Assert.Equal(productId, committed.ProductId);
        Assert.Equal(4, committed.Quantity);
    }

    private async Task<StockReservation> SeedActiveReservationAsync()
    {
        var reservation = StockReservation.Reserve(DefaultIdType.CreateVersion7(), 4, DateTimeOffset.UtcNow.AddDays(1));
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();
        return reservation;
    }

    /// <summary>Plays the warehouse webhook that confirms the reservation between the endpoint's read and its save.</summary>
    private void WebhookCommitsBeforeTheEndpointSaves(StockReservationId id, string providerReference)
    {
        factory.BeforeNextRequestSave = async () =>
        {
            await using var scope = Factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var competing = await db.StockReservations.SingleAsync(r => r.Id == id);
            Assert.False(competing.ReceiveProviderStatus(ReservationStatus.Committed, providerReference, DateTimeOffset.UtcNow).IsFailure);
            await db.SaveChangesAsync();
        };
    }

    [Fact]
    public async Task Commit_WebhookConfirmsTheSameReferenceJustBeforeTheSave_ReloadsAndSucceedsWithOneEvent()
    {
        var reservation = await SeedActiveReservationAsync();
        WebhookCommitsBeforeTheEndpointSaves(reservation.Id, "WH-1");

        var response = await StockLevelTestData.AuthedClient(Factory).PostAsJsonAsync(
            new Uri($"/v1/stock-reservations/{reservation.Id}/commit", UriKind.Relative),
            new { providerReference = "WH-1" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal("WH-1", reloaded.ProviderReference);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        Assert.Single(queued, e => e.ReservationId == reservation.Id.Value);
    }

    [Fact]
    public async Task Commit_WebhookConfirmsAnotherReferenceJustBeforeTheSave_AppliesTheCommandToTheFreshCopyAndFlagsTheConflict()
    {
        var reservation = await SeedActiveReservationAsync();
        WebhookCommitsBeforeTheEndpointSaves(reservation.Id, "WH-FROM-WEBHOOK");

        var response = await StockLevelTestData.AuthedClient(Factory).PostAsJsonAsync(
            new Uri($"/v1/stock-reservations/{reservation.Id}/commit", UriKind.Relative),
            new { providerReference = "WH-FROM-CALLER" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, reservation.Id);
        Assert.Equal(ReservationStatus.RequiresReconciliation, reloaded.Status);
        Assert.Equal("WH-FROM-WEBHOOK", reloaded.ProviderReference);
    }
}
