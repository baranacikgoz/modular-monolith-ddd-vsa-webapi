using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Exports;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.DownloadExport;

/// <summary>
///     Downloads a Succeeded export as the CSV file itself. Not yet Succeeded is 409 NotReady (poll GET
///     /products/exports/{id} first); another user's job is 404. A streamed body cannot be a <c>Result&lt;Response&gt;</c>,
///     so the handler returns either the file result, which the Result transformer passes through untouched, or the failed
///     Result, which it turns into the usual problem response. This is the one endpoint that does not return a Result on
///     success.
/// </summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("exports/{id}/download", DownloadExportAsync)
            .WithDescription("Download the CSV file of one of my finished product export jobs.")
            .RequireScope(KeycloakScopes.Products.Export)
            .Produces(StatusCodes.Status200OK, contentType: ProductExportJob.CsvContentType)
            .TransformResultTo<FileDownload>();
    }

    private static async Task<object> DownloadExportAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        IPrivateObjectStore objectStore,
        CancellationToken cancellationToken)
    {
        var file = await dbContext.ExportJobs
            .AsNoTracking()
            .TagWith(nameof(DownloadExportAsync), request.Id)
            .Where(j => j.Id == request.Id && j.RequestedBy == currentUser.Id)
            .Select(j => new FileRow(j.Status, j.FileName, j.ObjectKey))
            .SingleAsResultAsync(nameof(ProductExportJob), cancellationToken)
            .BindAsync(row => Task.FromResult(EnsureReady(row)))
            .MapAsync(async reference => new FileDownload(
                await objectStore.DownloadAsync(reference.ObjectKey, cancellationToken),
                ProductExportJob.CsvContentType,
                reference.FileName));

        return file.Match<FileDownload, object>(
            download => TypedResults.File(download.Content, download.ContentType, download.FileName),
            _ => file);
    }

    private sealed record FileRow(JobStatus Status, string? FileName, string? ObjectKey);

    private sealed record FileReference(string FileName, string ObjectKey);

    internal sealed record FileDownload(Stream Content, string ContentType, string FileName);

    private static Result<FileReference> EnsureReady(FileRow row)
    {
        return row is { Status: JobStatus.Succeeded, FileName: { } fileName, ObjectKey: { } objectKey }
            ? new FileReference(fileName, objectKey)
            : ProductExportErrors.NotReady;
    }
}
