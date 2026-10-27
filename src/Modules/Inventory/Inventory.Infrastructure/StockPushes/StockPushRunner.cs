using System.Net;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.Errors;
using Inventory.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     Sends the current quantity of every dirty stock level of one connection's store to its warehouse. Takes the
///     connection's lease first (a run that finds it held does nothing: the holder reads the marks fresh, so it covers
///     them), then pushes the oldest marks in batches of <see cref="InventoryOptions.StockPushBatchSize" /> and clears
///     each batch by the mark version it read: a stock level that changed after the read keeps its row and goes out next
///     time. A message makes at most <see cref="InventoryOptions.StockPushBatchesPerMessage" /> calls; marks left over by
///     the cap, or by a change that raced the push, are handed to a message of the run's own, queued after the lease is
///     released so the next run cannot find it held.
///     <para>
///         Nothing here throws for the warehouse. A throttle, an outage or an unknown outcome stops the run with the marks
///         still in place, because every call carries absolute quantities that are simply sent again: a bus retry would only
///         hammer the warehouse, and the sweep queues the next attempt once the marks are old enough.
///     </para>
///     <para>
///         A batch the warehouse refuses outright (a client error) is halved and sent again in halves, down to single stock
///         levels, so the one it will not take is found while the others still go out. That one stays marked and counts one
///         refusal; at <see cref="InventoryOptions.StockPushMaxAttempts" /> it is parked: no push reads it and the sweep
///         ignores it, until its quantity is marked again. One stock level the warehouse cannot take never holds back the
///         ones behind it.
///     </para>
///     <para>
///         A connection the host will never build a gateway for (a Dummy connection in a Production host) fails every run the
///         same way and no warehouse is to blame, so each run counts one attempt on every mark of its store instead of leaving
///         the sweep to queue the same failing run for ever: at <see cref="InventoryOptions.StockPushMaxAttempts" /> the marks
///         are parked like a refused one. A connection that is merely deactivated counts nothing: the sweep does not queue for
///         it and its marks wait for the store's next active connection.
///     </para>
/// </summary>
public sealed partial class StockPushRunner(
    IInventoryDbContext dbContext,
    IWarehouseGatewayFactory gatewayFactory,
    IIntegrationEventOutbox outbox,
    IOptions<InventoryOptions> options,
    TimeProvider timeProvider,
    ILogger<StockPushRunner> logger)
{
    private static readonly string CallFailedKey = WarehouseGatewayErrors.CallFailed(HttpStatusCode.BadRequest).Key;

    private static readonly string DummyInProductionKey = WarehouseConnectionErrors.DummyProviderNotAllowedInProduction.Key;

    public async Task RunAsync(WarehouseConnectionId connectionId, CancellationToken cancellationToken)
    {
        // The lease is a session lock: the DbContext's connection stays open, and the same one, for the whole run.
        await dbContext.Database.OpenConnectionAsync(cancellationToken);
        try
        {
            if (await PushUnderLeaseAsync(connectionId, cancellationToken))
            {
                outbox.Collect(new StockPushDueIntegrationEvent(connectionId.Value));
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync();
        }
    }

    /// <summary>True when marks remain that a later message has to send.</summary>
    private async Task<bool> PushUnderLeaseAsync(WarehouseConnectionId connectionId, CancellationToken cancellationToken)
    {
        if (!await StockPushLease.TryAcquireAsync(dbContext, connectionId, cancellationToken))
        {
            LogLeaseHeld(logger, connectionId.Value);
            return false;
        }

        try
        {
            var outcome = await dbContext.WarehouseConnections
                .AsNoTracking()
                .TagWith(nameof(PushUnderLeaseAsync), connectionId)
                .Where(c => c.Id == connectionId)
                .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
                .BindAsync(connection => CreateGatewayAsync(connection, cancellationToken))
                .BindAsync(target => PushBatchesAsync(target.StoreId, target.Gateway, cancellationToken));

            return outcome.Match(
                marksRemain => marksRemain,
                error =>
                {
                    LogPushStopped(logger, connectionId.Value, error.Key);
                    return false;
                });
        }
        finally
        {
            await ReleaseLeaseAsync(connectionId);
        }
    }

    /// <summary>
    ///     The gateway of the connection, or the factory's refusal. A refusal that no later run can cure counts an attempt on the
    ///     store's marks on its way out (see <see cref="StockPushMarks.RecordConfigurationFailureAsync" />).
    /// </summary>
    private Task<Result<PushTarget>> CreateGatewayAsync(WarehouseConnection connection, CancellationToken cancellationToken)
    {
        return gatewayFactory.Create(connection).Match(
            gateway => Task.FromResult(Result<PushTarget>.Success(new PushTarget(connection.StoreId, gateway))),
            error => CountConfigurationFailureAsync(connection, error, cancellationToken));
    }

    private async Task<Result<PushTarget>> CountConfigurationFailureAsync(
        WarehouseConnection connection, Error error, CancellationToken cancellationToken)
    {
        if (error.Key == DummyInProductionKey)
        {
            var counted = await StockPushMarks.RecordConfigurationFailureAsync(
                dbContext, connection.StoreId, error.Key, options.Value.StockPushMaxAttempts, timeProvider.GetUtcNow(), cancellationToken);
            LogMarksCounted(logger, connection.Id.Value, error.Key, counted);
        }

        return error;
    }

    private async Task<Result<bool>> PushBatchesAsync(DefaultIdType storeId, IWarehouseGateway gateway, CancellationToken cancellationToken)
    {
        // The stock levels the warehouse refused in this run: they stay marked, but this run does not read them again.
        var refused = new List<StockLevelId>();
        Result<bool> outcome = true;

        // A failed batch skips the batches after it; a page that was not full means the marks are drained.
        for (var batch = 0; batch < options.Value.StockPushBatchesPerMessage; batch++)
        {
            outcome = await outcome.BindAsync(more => more
                ? PushNextBatchAsync(storeId, gateway, refused, cancellationToken)
                : Task.FromResult(Result<bool>.Success(false)));
        }

        return await outcome.BindAsync(_ => MarksRemainAsync(storeId, refused, cancellationToken));
    }

    /// <summary>True when the batch was full, so more marks may follow in this run.</summary>
    private async Task<Result<bool>> PushNextBatchAsync(
        DefaultIdType storeId, IWarehouseGateway gateway, List<StockLevelId> refused, CancellationToken cancellationToken)
    {
        var batchSize = options.Value.StockPushBatchSize;
        var batch = await ReadOldestMarksAsync(storeId, batchSize, refused, cancellationToken);
        if (batch.Count == 0)
        {
            return false;
        }

        return await PushIsolatingRefusalsAsync(gateway, batch, refused, cancellationToken)
            .BindAsync(() => batch.Count == batchSize);
    }

    /// <summary>
    ///     Sends <paramref name="items" /> and clears them. When the warehouse refuses the batch itself, it is halved and each
    ///     half sent on its own, down to single stock levels; the one the warehouse still refuses alone is counted, not
    ///     cleared. Any other failure is not the stock levels' doing (throttled, down, no answer) and stops the run with every
    ///     mark in place.
    /// </summary>
    private async Task<Result> PushIsolatingRefusalsAsync(
        IWarehouseGateway gateway, IReadOnlyList<DirtyStock> items, List<StockLevelId> refused, CancellationToken cancellationToken)
    {
        var sent = await SendAsync(gateway, items, cancellationToken);
        if (!sent.IsFailure)
        {
            await StockPushMarks.ClearAsync(
                dbContext, [.. items.Select(m => new PushedMark(m.StockLevelId, m.MarkVersion))], cancellationToken);
            InventoryTelemetry.StockPushItemsSent.Add(items.Count);
            return Result.Success;
        }

        if (!WarehouseRefusedTheItems(sent.Error!))
        {
            return sent.Error!;
        }

        if (items.Count == 1)
        {
            var single = items[0];
            await StockPushMarks.RecordRefusalAsync(
                dbContext, new PushedMark(single.StockLevelId, single.MarkVersion), sent.Error!.Key, timeProvider.GetUtcNow(), cancellationToken);
            refused.Add(single.StockLevelId);
            InventoryTelemetry.StockPushItemsRefused.Add(1);
            return Result.Success;
        }

        var half = items.Count / 2;
        return await PushIsolatingRefusalsAsync(gateway, [.. items.Take(half)], refused, cancellationToken)
            .BindAsync(() => PushIsolatingRefusalsAsync(gateway, [.. items.Skip(half)], refused, cancellationToken));
    }

    /// <summary>A client error of the warehouse (not a timeout, not a throttle) says it will not take what it was sent.</summary>
    private static bool WarehouseRefusedTheItems(Error error)
    {
        return error.Key == CallFailedKey
               && error.Value is int status
               && status is >= 400 and < 500
               && status != (int)HttpStatusCode.RequestTimeout;
    }

    /// <summary>
    ///     Marks and current quantities come from one statement, so a mark and the quantity it stands for are read together.
    ///     A parked mark (at the attempt maximum) and one the warehouse refused in this run are left out.
    /// </summary>
    private Task<List<DirtyStock>> ReadOldestMarksAsync(
        DefaultIdType storeId, int take, IReadOnlyCollection<StockLevelId> refused, CancellationToken cancellationToken)
    {
        var maxAttempts = options.Value.StockPushMaxAttempts;
        var skipped = refused.ToArray();

        return dbContext.StockPushDirtyItems
            .AsNoTracking()
            .TagWith(nameof(ReadOldestMarksAsync), storeId)
            .Where(d => d.StoreId == storeId && d.PushAttempts < maxAttempts && !skipped.Contains(d.StockLevelId))
            .Join(dbContext.StockLevels, d => d.StockLevelId, s => s.Id, (mark, stockLevel) => new { mark, stockLevel })
            .OrderBy(x => x.mark.MarkedOn)
            .ThenBy(x => x.mark.StockLevelId)
            .Take(take)
            .Select(x => new DirtyStock(x.mark.StockLevelId, x.mark.MarkVersion, x.mark.ProductId, x.stockLevel.QuantityOnHand))
            .ToListAsync(cancellationToken);
    }

    /// <summary>An unknown outcome is a failed batch here, not an exception: the quantities are absolute, so nothing needs reconciling.</summary>
    private static async Task<Result<string>> SendAsync(
        IWarehouseGateway gateway, IReadOnlyList<DirtyStock> batch, CancellationToken cancellationToken)
    {
        try
        {
            return await gateway.PushStockAsync(
                [.. batch.Select(m => new WarehouseStockQuantity(m.ProductId.ToString(), m.Quantity))], cancellationToken);
        }
        catch (WarehouseOutcomeUnknownException)
        {
            return WarehouseGatewayErrors.OutcomeUnknown;
        }
    }

    /// <summary>True when a mark is left that a later message can send: a parked one, or one refused just now, is not.</summary>
    private async Task<Result<bool>> MarksRemainAsync(
        DefaultIdType storeId, IReadOnlyCollection<StockLevelId> refused, CancellationToken cancellationToken)
    {
        var maxAttempts = options.Value.StockPushMaxAttempts;
        var skipped = refused.ToArray();

        return await dbContext.StockPushDirtyItems
            .AsNoTracking()
            .TagWith(nameof(MarksRemainAsync), storeId)
            .AnyAsync(d => d.StoreId == storeId && d.PushAttempts < maxAttempts && !skipped.Contains(d.StockLevelId), cancellationToken);
    }

    private async Task ReleaseLeaseAsync(WarehouseConnectionId connectionId)
    {
        try
        {
            await StockPushLease.ReleaseAsync(dbContext, connectionId);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            // A session that cannot answer is gone, and the server drops the lock of a gone session.
            LogLeaseReleaseFailed(logger, connectionId.Value, ex);
        }
    }

    private sealed record PushTarget(DefaultIdType StoreId, IWarehouseGateway Gateway);

    private sealed record DirtyStock(StockLevelId StockLevelId, long MarkVersion, DefaultIdType ProductId, int Quantity);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Stock push of connection {ConnectionId} skipped: another run holds its lease.")]
    private static partial void LogLeaseHeld(ILogger logger, Guid connectionId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Stock push of connection {ConnectionId} stopped ({Reason}); its marks stay for the next attempt.")]
    private static partial void LogPushStopped(ILogger logger, Guid connectionId, string reason);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Stock push of connection {ConnectionId} cannot run ({Reason}); counted one attempt on {MarkCount} marks of its store.")]
    private static partial void LogMarksCounted(ILogger logger, Guid connectionId, string reason, int markCount);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Stock push of connection {ConnectionId} could not release its lease; the server drops it with the session.")]
    private static partial void LogLeaseReleaseFailed(ILogger logger, Guid connectionId, Exception exception);
}
