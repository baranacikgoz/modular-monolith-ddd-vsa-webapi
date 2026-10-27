using Common.Application.Jobs;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.BulkAdjustments;

public readonly record struct StockBulkAdjustJobId(DefaultIdType Value) : IStronglyTypedId
{
    public static StockBulkAdjustJobId New()
    {
        return new StockBulkAdjustJobId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StockBulkAdjustJobId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>One rejected row of the uploaded file: its Excel row number and a stable reason key, never a localized
/// sentence.</summary>
public sealed record StockBulkAdjustRowError(int RowNumber, string Reason);

/// <summary>
///     One queued bulk stock adjustment. The upload endpoint stores the workbook in the private object store, writes this
///     row and queues one <c>StockBulkAdjustRequestedIntegrationEvent</c> in the same transaction; the consumer claims the
///     row and works through the file in batches, storing <see cref="LastProcessedRow" /> after every saved batch. Every
///     row of the file is one <c>StockLevel.Adjust</c> with the job's <see cref="Reason" />, so it writes its own ledger
///     row. Event-free bookkeeping (<see cref="JobRow{TId}" />): nothing downstream reacts to a job's lifecycle.
///     <see cref="JobRow{TId}.Progress" /> counts the rows handled so far out of <see cref="TotalRows" />.
/// </summary>
#pragma warning disable S1144 // Setters exist for EF materialization: after creation the runner writes set-based (ExecuteUpdate).
public sealed class StockBulkAdjustJob : JobRow<StockBulkAdjustJobId>
{
    public const int FileNameMaxLength = 256;
    public const int ClientRequestIdMaxLength = 64;
    public const int ObjectKeyMaxLength = 512;

    /// <summary>The header is row 1, so a job that has not processed anything yet is at row 1.</summary>
    public const int HeaderRowNumber = 1;

#pragma warning disable IDE0051 // EF Core binds this constructor when materializing rows
    private StockBulkAdjustJob(StockBulkAdjustJobId id) : base(id)
    {
    }
#pragma warning restore IDE0051

    private StockBulkAdjustJob(
        StockBulkAdjustJobId id, ApplicationUserId requestedBy, DateTimeOffset queuedOn, string clientRequestId, string fileName,
        string objectKey, string reason)
        : base(id, requestedBy, queuedOn)
    {
        ClientRequestId = clientRequestId;
        FileName = fileName;
        ObjectKey = objectKey;
        Reason = reason;
    }

    /// <summary>Chosen by the client per upload: a repeat of the same id by the same user is the same job.</summary>
    public string ClientRequestId { get; private set; } = string.Empty;

    public string FileName { get; private set; } = string.Empty;

    /// <summary>Where the uploaded workbook sits in the private object store; deleted when the job finishes.</summary>
    public string ObjectKey { get; private set; } = string.Empty;

    /// <summary>The explanation every ledger row of this job carries.</summary>
    public string Reason { get; private set; } = string.Empty;

    /// <summary>Excel row number of the last row a saved batch consumed: the next message resumes after it.</summary>
    public int LastProcessedRow { get; private set; } = HeaderRowNumber;

    /// <summary>Data rows of the file (blank rows excluded); counted by the first message.</summary>
    public int TotalRows { get; private set; }

    public int RowsAdjusted { get; private set; }
    public int RowsRejected { get; private set; }

    /// <summary>
    ///     How many times the stuck-job sweep handed this job back to the queue since it last saved a batch. A batch resets it,
    ///     so only a job that keeps dying before it makes progress reaches <c>InventoryOptions.StockJobMaxRequeues</c>.
    /// </summary>
    public int RequeueCount { get; private set; }

    /// <summary>The first rejected rows, capped by <c>InventoryOptions.BulkAdjustMaxStoredRowErrors</c>; the rest are only
    /// counted in <see cref="RowsRejected" />.</summary>
    public IReadOnlyList<StockBulkAdjustRowError> RowErrors { get; private set; } = [];

    public static StockBulkAdjustJob Queue(
        StockBulkAdjustJobId id, ApplicationUserId requestedBy, DateTimeOffset queuedOn, string clientRequestId, string fileName,
        string objectKey, string reason)
    {
        var trimmedName = fileName.Length <= FileNameMaxLength ? fileName : fileName[..FileNameMaxLength];
        return new StockBulkAdjustJob(id, requestedBy, queuedOn, clientRequestId, trimmedName, objectKey, reason);
    }
}
#pragma warning restore S1144
