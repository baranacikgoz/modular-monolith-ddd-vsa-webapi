using System.Net;
using Common.Domain.ResultMonad;

namespace Inventory.Application.BulkAdjustments;

/// <summary>Errors of the bulk adjustment as a whole: the first two answer the upload request, the rest are failure keys a
/// job row carries.</summary>
public static class StockBulkAdjustErrors
{
    public static readonly Error FileTooLarge = new() { Key = nameof(FileTooLarge), StatusCode = HttpStatusCode.RequestEntityTooLarge };

    /// <summary>A save lost the race on the unique (user, client request id) or active-job index.</summary>
    public static readonly Error AlreadyQueued = new() { Key = nameof(AlreadyQueued), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error FileUnreadable = new() { Key = nameof(FileUnreadable) };

    public static readonly Error TooManyRows = new() { Key = nameof(TooManyRows) };

    /// <summary>Another writer changed one of the batch's stock levels between the batch's read and its save.</summary>
    public static readonly Error ConcurrentUpdate = new() { Key = nameof(ConcurrentUpdate), StatusCode = HttpStatusCode.Conflict };

    public const string UnexpectedError = "UnexpectedError";

    /// <summary>The job was handed back to the queue as often as allowed and still never saved a batch: it is not retried again.</summary>
    public const string JobAbandoned = "JobAbandoned";
}

/// <summary>
///     Why one row of the file was rejected. The rest of the file is still applied. A row the stock level itself refuses
///     (<c>StockLevelErrors.InsufficientStock</c>) is reported under that error's own key.
/// </summary>
public static class StockBulkAdjustRowErrors
{
    public static readonly Error MissingProductId = new() { Key = nameof(MissingProductId) };
    public static readonly Error InvalidProductId = new() { Key = nameof(InvalidProductId) };
    public static readonly Error InvalidQuantity = new() { Key = nameof(InvalidQuantity) };

    /// <summary>No stock level exists for the product id.</summary>
    public static readonly Error UnknownProduct = new() { Key = nameof(UnknownProduct) };
}
