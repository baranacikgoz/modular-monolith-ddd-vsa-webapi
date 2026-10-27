using System.Net;
using Common.Application.Jobs;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Application.Exports;

public readonly record struct ProductExportJobId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductExportJobId New()
    {
        return new ProductExportJobId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ProductExportJobId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

public static class ProductExportErrors
{
    /// <summary>The download was asked for before the job succeeded.</summary>
    public static readonly Error NotReady = new() { Key = nameof(NotReady), StatusCode = HttpStatusCode.Conflict };

    /// <summary>A save lost the race on the one-active-export-per-user index.</summary>
    public static readonly Error AlreadyQueued = new() { Key = nameof(AlreadyQueued), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error RowLimitExceeded = new() { Key = nameof(RowLimitExceeded) };

    public const string UnexpectedError = "UnexpectedError";
}

/// <summary>
///     One queued CSV export of products. The endpoint writes this row and queues one
///     <c>ProductExportRequestedIntegrationEvent</c> in the same transaction; the consumer builds the file, uploads it
///     to the private object store and records <see cref="ObjectKey" /> and <see cref="FileName" /> on success. Clients
///     poll the row and then download the file. <see cref="JobRow{TId}.Progress" /> counts the products written so far.
/// </summary>
#pragma warning disable S1144 // Setters exist for EF materialization: after creation the runner writes set-based (ExecuteUpdate).
public sealed class ProductExportJob : JobRow<ProductExportJobId>
{
    public const int FileNameMaxLength = 128;
    public const int ObjectKeyMaxLength = 512;
    public const string CsvContentType = "text/csv";

#pragma warning disable IDE0051 // EF Core binds this constructor when materializing rows
    private ProductExportJob(ProductExportJobId id) : base(id)
    {
    }
#pragma warning restore IDE0051

    private ProductExportJob(ApplicationUserId requestedBy, DateTimeOffset queuedOn, StoreId? storeId)
        : base(ProductExportJobId.New(), requestedBy, queuedOn)
    {
        StoreId = storeId;
    }

    /// <summary>Only the products of this store are exported; every product when null.</summary>
    public StoreId? StoreId { get; private set; }

    /// <summary>Set on success, with <see cref="ObjectKey" />.</summary>
    public string? FileName { get; private set; }

    public string? ObjectKey { get; private set; }

    public static ProductExportJob Queue(ApplicationUserId requestedBy, DateTimeOffset queuedOn, StoreId? storeId)
    {
        return new ProductExportJob(requestedBy, queuedOn, storeId);
    }
}
#pragma warning restore S1144
