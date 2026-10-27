using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.WarehouseConnections;
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
///         Nothing here throws for the warehouse. A refusal, a throttle or an unknown outcome stops the run with the marks
///         still in place, because every call carries absolute quantities that are simply sent again: a bus retry would only
///         hammer the warehouse, and the sweep queues the next attempt once the marks are old enough.
///     </para>
/// </summary>
public sealed partial class StockPushRunner(
    IInventoryDbContext dbContext,
    IWarehouseGatewayFactory gatewayFactory,
    IIntegrationEventOutbox outbox,
    IOptions<InventoryOptions> options,
    ILogger<StockPushRunner> logger)
{
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
                .CombineAsync(connection => Task.FromResult(gatewayFactory.Create(connection)))
                .BindAsync(pair => PushBatchesAsync(pair.Item1.StoreId, pair.Item2, cancellationToken));

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

    private async Task<Result<bool>> PushBatchesAsync(DefaultIdType storeId, IWarehouseGateway gateway, CancellationToken cancellationToken)
    {
        Result<bool> outcome = true;

        // A failed batch skips the batches after it; a page that was not full means the marks are drained.
        for (var batch = 0; batch < options.Value.StockPushBatchesPerMessage; batch++)
        {
            outcome = await outcome.BindAsync(more => more
                ? PushNextBatchAsync(storeId, gateway, cancellationToken)
                : Task.FromResult(Result<bool>.Success(false)));
        }

        return await outcome.BindAsync(_ => MarksRemainAsync(storeId, cancellationToken));
    }

    /// <summary>True when the batch was full, so more marks may follow in this run.</summary>
    private async Task<Result<bool>> PushNextBatchAsync(DefaultIdType storeId, IWarehouseGateway gateway, CancellationToken cancellationToken)
    {
        var batchSize = options.Value.StockPushBatchSize;
        var batch = await ReadOldestMarksAsync(storeId, batchSize, cancellationToken);
        if (batch.Count == 0)
        {
            return false;
        }

        return await SendAsync(gateway, batch, cancellationToken)
            .TapAsync(_ => StockPushMarks.ClearAsync(dbContext, [.. batch.Select(m => new PushedMark(m.StockLevelId, m.MarkVersion))], cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockPushItemsSent.Add(batch.Count))
            .MapAsync(_ => batch.Count == batchSize);
    }

    /// <summary>Marks and current quantities come from one statement, so a mark and the quantity it stands for are read together.</summary>
    private Task<List<DirtyStock>> ReadOldestMarksAsync(DefaultIdType storeId, int take, CancellationToken cancellationToken)
    {
        return dbContext.StockPushDirtyItems
            .AsNoTracking()
            .TagWith(nameof(ReadOldestMarksAsync), storeId)
            .Where(d => d.StoreId == storeId)
            .Join(dbContext.StockLevels, d => d.StockLevelId, s => s.Id, (mark, stockLevel) => new { mark, stockLevel })
            .OrderBy(x => x.mark.MarkedOn)
            .ThenBy(x => x.mark.StockLevelId)
            .Take(take)
            .Select(x => new DirtyStock(x.mark.StockLevelId, x.mark.MarkVersion, x.mark.ProductId, x.stockLevel.QuantityOnHand))
            .ToListAsync(cancellationToken);
    }

    /// <summary>An unknown outcome is a failed batch here, not an exception: the quantities are absolute, so nothing needs reconciling.</summary>
    private static async Task<Result<string>> SendAsync(IWarehouseGateway gateway, List<DirtyStock> batch, CancellationToken cancellationToken)
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

    private async Task<Result<bool>> MarksRemainAsync(DefaultIdType storeId, CancellationToken cancellationToken)
    {
        return await dbContext.StockPushDirtyItems
            .AsNoTracking()
            .TagWith(nameof(MarksRemainAsync), storeId)
            .AnyAsync(d => d.StoreId == storeId, cancellationToken);
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

    private sealed record DirtyStock(StockLevelId StockLevelId, long MarkVersion, DefaultIdType ProductId, int Quantity);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Stock push of connection {ConnectionId} skipped: another run holds its lease.")]
    private static partial void LogLeaseHeld(ILogger logger, Guid connectionId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Stock push of connection {ConnectionId} stopped ({Reason}); its marks stay for the next attempt.")]
    private static partial void LogPushStopped(ILogger logger, Guid connectionId, string reason);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Stock push of connection {ConnectionId} could not release its lease; the server drops it with the session.")]
    private static partial void LogLeaseReleaseFailed(ILogger logger, Guid connectionId, Exception exception);
}
