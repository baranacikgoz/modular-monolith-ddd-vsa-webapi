using System.Linq.Expressions;
using Inventory.Application.BulkAdjustments;

namespace Inventory.Endpoints.StockLevels.v1.BulkAdjust;

/// <summary>
///     A bulk adjustment job, as returned by POST /stock-levels/bulk-adjustments (202), GET /stock-levels/bulk-adjustments/{id}
///     and the list. Status: Queued and Running are not final; Succeeded means the file was processed (RowErrors lists the
///     rejected rows, the rest was applied); Failed carries FailureKey (TooManyRows, FileUnreadable, JobStale,
///     UnexpectedError). ProcessedRows out of TotalRows is the progress; TotalRows is 0 until the first queue message has
///     counted the file.
/// </summary>
public sealed record Response
{
    public required StockBulkAdjustJobId JobId { get; init; }
    public required string Status { get; init; }
    public required string FileName { get; init; }
    public required string Reason { get; init; }
    public required DateTimeOffset QueuedOn { get; init; }
    public DateTimeOffset? StartedOn { get; init; }
    public DateTimeOffset? FinishedOn { get; init; }
    public string? FailureKey { get; init; }
    public required int TotalRows { get; init; }
    public required int ProcessedRows { get; init; }
    public required int RowsAdjusted { get; init; }
    public required int RowsRejected { get; init; }

    /// <summary>The first rejected rows with a short reason key (MissingProductId, InvalidProductId, InvalidQuantity,
    /// UnknownProduct, InsufficientStock, ConcurrentUpdate), never a localized sentence. Capped; <see cref="RowsRejected" />
    /// counts them all.</summary>
    public required IReadOnlyList<StockBulkAdjustRowError> RowErrors { get; init; }

    internal static readonly Expression<Func<StockBulkAdjustJob, Response>> Projection = j => new Response
    {
        JobId = j.Id,
        Status = j.Status.ToString(),
        FileName = j.FileName,
        Reason = j.Reason,
        QueuedOn = j.QueuedOn,
        StartedOn = j.StartedOn,
        FinishedOn = j.FinishedOn,
        FailureKey = j.FailureKey,
        TotalRows = j.TotalRows,
        ProcessedRows = j.Progress,
        RowsAdjusted = j.RowsAdjusted,
        RowsRejected = j.RowsRejected,
        RowErrors = j.RowErrors
    };
}
