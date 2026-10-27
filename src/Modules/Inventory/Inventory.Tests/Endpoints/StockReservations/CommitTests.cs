using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Tests.Support;
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
}
