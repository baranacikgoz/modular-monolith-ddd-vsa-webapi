using System.Linq.Expressions;
using Products.Application.Imports;

namespace Products.Endpoints.Products.v1.Import;

/// <summary>
///     A product import job, as returned by POST /products/imports (202), GET /products/imports/{id} and the list.
///     Status: Queued and Running are not final; Succeeded means the file was processed (RowErrors lists the rejected
///     rows, the rest was imported); Failed carries FailureKey (TooManyRows, FileUnreadable, NotFound, JobStale,
///     UnexpectedError). ProcessedRows out of TotalRows is the progress; TotalRows is 0 until the first queue
///     message has counted the file.
/// </summary>
public sealed record Response
{
    public required ProductImportJobId JobId { get; init; }
    public required string Status { get; init; }
    public required string FileName { get; init; }
    public required DateTimeOffset QueuedOn { get; init; }
    public DateTimeOffset? StartedOn { get; init; }
    public DateTimeOffset? FinishedOn { get; init; }
    public string? FailureKey { get; init; }
    public required int TotalRows { get; init; }
    public required int ProcessedRows { get; init; }
    public required int ProductsCreated { get; init; }
    public required int ProductsUpdated { get; init; }
    public required int RowsRejected { get; init; }

    /// <summary>The first rejected rows with a short reason key (MissingName, NameTooLong, DescriptionTooLong,
    /// InvalidQuantity, InvalidPrice, ConcurrentUpdate), never a localized sentence. Capped; <see cref="RowsRejected" />
    /// counts them all.</summary>
    public required IReadOnlyList<ProductImportRowError> RowErrors { get; init; }

    internal static readonly Expression<Func<ProductImportJob, Response>> Projection = j => new Response
    {
        JobId = j.Id,
        Status = j.Status.ToString(),
        FileName = j.FileName,
        QueuedOn = j.QueuedOn,
        StartedOn = j.StartedOn,
        FinishedOn = j.FinishedOn,
        FailureKey = j.FailureKey,
        TotalRows = j.TotalRows,
        ProcessedRows = j.Progress,
        ProductsCreated = j.ProductsCreated,
        ProductsUpdated = j.ProductsUpdated,
        RowsRejected = j.RowsRejected,
        RowErrors = j.RowErrors
    };
}
