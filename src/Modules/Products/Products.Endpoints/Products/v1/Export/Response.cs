using System.Linq.Expressions;
using Products.Application.Exports;
using Products.Domain.Stores;

namespace Products.Endpoints.Products.v1.Export;

/// <summary>
///     A product export job, as returned by POST /products/exports (202) and GET /products/exports/{id}. Queued and
///     Running are not final; Succeeded means the CSV is ready at GET /products/exports/{id}/download; Failed carries
///     FailureKey (RowLimitExceeded, JobStale, UnexpectedError). <see cref="ProductsWritten" /> is the progress.
/// </summary>
public sealed record Response
{
    public required ProductExportJobId JobId { get; init; }
    public required string Status { get; init; }
    public StoreId? StoreId { get; init; }
    public required DateTimeOffset QueuedOn { get; init; }
    public DateTimeOffset? StartedOn { get; init; }
    public DateTimeOffset? FinishedOn { get; init; }
    public string? FailureKey { get; init; }
    public required int ProductsWritten { get; init; }
    public string? FileName { get; init; }

    internal static readonly Expression<Func<ProductExportJob, Response>> Projection = j => new Response
    {
        JobId = j.Id,
        Status = j.Status.ToString(),
        StoreId = j.StoreId,
        QueuedOn = j.QueuedOn,
        StartedOn = j.StartedOn,
        FinishedOn = j.FinishedOn,
        FailureKey = j.FailureKey,
        ProductsWritten = j.Progress,
        FileName = j.FileName
    };
}
