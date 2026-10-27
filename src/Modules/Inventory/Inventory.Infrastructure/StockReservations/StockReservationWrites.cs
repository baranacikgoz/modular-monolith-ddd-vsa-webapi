using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using EntityFramework.Exceptions.Common;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>
///     Saves the outcome of something that already happened at the warehouse system (a release it confirmed, a reconcile that
///     found the truth) on a reservation that was loaded before the call. A webhook that lands while the call is in flight
///     changes the same row, so the save of the loaded copy loses: either the row's concurrency check fails or the audit row
///     of the event (keyed by aggregate and version) is already taken. The outcome is real and must not be answered with a
///     conflict, so the reservation is read again and the same command applied to what is there now, once.
///     A command that fails on the fresh copy fails the write; a second lost race is left to the caller's conflict handling.
/// </summary>
public static class StockReservationWrites
{
    public static async Task<Result> ApplyAndSaveAsync(
        this IInventoryDbContext dbContext,
        StockReservation loaded,
        Func<StockReservation, Result> command,
        CancellationToken cancellationToken)
    {
        try
        {
            return await ApplyOnceAsync(dbContext, loaded, command, cancellationToken);
        }
        catch (Exception ex) when (ex is DbUpdateConcurrencyException or UniqueConstraintException)
        {
            // Only the stale copy goes: anything else the scope tracks (a consumer's pending inbox row) still belongs to the save.
            dbContext.Entry(loaded).State = EntityState.Detached;

            return await dbContext.StockReservations
                .TagWith(nameof(ApplyAndSaveAsync), loaded.Id)
                .Where(r => r.Id == loaded.Id)
                .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
                .BindAsync(fresh => ApplyOnceAsync(dbContext, fresh, command, cancellationToken));
        }
    }

    private static Task<Result> ApplyOnceAsync(
        IInventoryDbContext dbContext,
        StockReservation reservation,
        Func<StockReservation, Result> command,
        CancellationToken cancellationToken)
    {
        return command(reservation).BindAsync(async () =>
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        });
    }
}
