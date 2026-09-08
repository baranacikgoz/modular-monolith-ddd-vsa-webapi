using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

[Collection("IntegrationTestCollection")]
public class StockReservationTimestampTests : BaseIntegrationTest
{
    public StockReservationTimestampTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Reservation_ReservationDeadline_SurvivesDbRoundTripWithinTolerance()
    {
        var deadline = DateTimeOffset.UtcNow.AddDays(3);
        StockReservationId id;

        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var reservation = StockReservation.Reserve(Guid.NewGuid(), 5, deadline);
            db.StockReservations.Add(reservation);
            await db.SaveChangesAsync();
            id = reservation.Id;
        }

        // Fresh scope + fresh DbContext instance: forces an actual read from Postgres rather than
        // returning the tracked in-memory entity. Postgres timestamptz stores microsecond precision while
        // .NET DateTimeOffset ticks are 100ns, so the last tick digit is silently truncated on write - a
        // bare Assert.Equal against the in-memory value fails nondeterministically depending on the
        // captured tick's last digit. The tolerance overload is required here specifically because this
        // value round-tripped through the database; an in-memory-only comparison wouldn't need it.
        using var freshScope = Factory.Services.CreateScope();
        var freshDb = freshScope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var reloaded = await freshDb.StockReservations.AsNoTracking().SingleAsync(r => r.Id == id);

        Assert.Equal(deadline, reloaded.ReservationDeadline, TimeSpan.FromSeconds(1));
    }
}
