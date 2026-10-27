using Common.Application.Jobs;
using Common.Domain.StronglyTypedIds;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Application.Imports;

public readonly record struct ProductImportJobId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductImportJobId New()
    {
        return new ProductImportJobId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ProductImportJobId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>One rejected row of the uploaded file: its Excel row number and a stable reason key, never a localized
/// sentence.</summary>
public sealed record ProductImportRowError(int RowNumber, string Reason);

/// <summary>
///     One queued product import. The upload endpoint stores the workbook in the private object store, writes this row
///     and queues one <c>ProductImportRequestedIntegrationEvent</c> in the same transaction; the consumer claims the row
///     and works through the file in batches, storing <see cref="LastProcessedRow" /> after every saved batch. Event-free
///     bookkeeping (<see cref="JobRow{TId}" />): nothing downstream reacts to a job's lifecycle.
///     <see cref="JobRow{TId}.Progress" /> counts the rows handled so far out of <see cref="TotalRows" />.
/// </summary>
#pragma warning disable S1144 // Setters exist for EF materialization: after creation the runner writes set-based (ExecuteUpdate).
public sealed class ProductImportJob : JobRow<ProductImportJobId>
{
    public const int FileNameMaxLength = 256;
    public const int ClientRequestIdMaxLength = 64;
    public const int ObjectKeyMaxLength = 512;

    /// <summary>The header is row 1, so a job that has not processed anything yet is at row 1.</summary>
    public const int HeaderRowNumber = 1;

#pragma warning disable IDE0051 // EF Core binds this constructor when materializing rows
    private ProductImportJob(ProductImportJobId id) : base(id)
    {
    }
#pragma warning restore IDE0051

    private ProductImportJob(
        ProductImportJobId id, ApplicationUserId requestedBy, DateTimeOffset queuedOn, StoreId storeId,
        ProductTemplateId productTemplateId, string clientRequestId, string fileName, string objectKey)
        : base(id, requestedBy, queuedOn)
    {
        StoreId = storeId;
        ProductTemplateId = productTemplateId;
        ClientRequestId = clientRequestId;
        FileName = fileName;
        ObjectKey = objectKey;
    }

    public StoreId StoreId { get; private set; }
    public ProductTemplateId ProductTemplateId { get; private set; }

    /// <summary>Chosen by the client per upload: a repeat of the same id by the same user is the same job.</summary>
    public string ClientRequestId { get; private set; } = string.Empty;

    public string FileName { get; private set; } = string.Empty;

    /// <summary>Where the uploaded workbook sits in the private object store; deleted when the job finishes.</summary>
    public string ObjectKey { get; private set; } = string.Empty;

    /// <summary>Excel row number of the last row a saved batch consumed: the next message resumes after it.</summary>
    public int LastProcessedRow { get; private set; } = HeaderRowNumber;

    /// <summary>Data rows of the file (blank rows excluded); counted by the first message.</summary>
    public int TotalRows { get; private set; }

    public int ProductsCreated { get; private set; }
    public int ProductsUpdated { get; private set; }
    public int RowsRejected { get; private set; }

    /// <summary>The first rejected rows, capped by <c>ProductsOptions.ImportMaxStoredRowErrors</c>; the rest are only
    /// counted in <see cref="RowsRejected" />.</summary>
    public IReadOnlyList<ProductImportRowError> RowErrors { get; private set; } = [];

    public static ProductImportJob Queue(
        ProductImportJobId id, ApplicationUserId requestedBy, DateTimeOffset queuedOn, StoreId storeId,
        ProductTemplateId productTemplateId, string clientRequestId, string fileName, string objectKey)
    {
        var trimmedName = fileName.Length <= FileNameMaxLength ? fileName : fileName[..FileNameMaxLength];
        return new ProductImportJob(id, requestedBy, queuedOn, storeId, productTemplateId, clientRequestId, trimmedName, objectKey);
    }
}
#pragma warning restore S1144
