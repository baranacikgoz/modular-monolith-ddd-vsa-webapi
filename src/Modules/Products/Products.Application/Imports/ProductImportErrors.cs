using System.Net;
using Common.Domain.ResultMonad;

namespace Products.Application.Imports;

/// <summary>Errors of the import as a whole: the first two answer the upload request, the rest are failure keys a job
/// row carries.</summary>
public static class ProductImportErrors
{
    public static readonly Error FileTooLarge = new() { Key = nameof(FileTooLarge), StatusCode = HttpStatusCode.RequestEntityTooLarge };

    /// <summary>A save lost the race on the unique (user, client request id) or active-job index.</summary>
    public static readonly Error AlreadyQueued = new() { Key = nameof(AlreadyQueued), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error FileUnreadable = new() { Key = nameof(FileUnreadable) };

    public static readonly Error TooManyRows = new() { Key = nameof(TooManyRows) };

    /// <summary>Another writer changed the same store or product between the batch's read and its save.</summary>
    public static readonly Error ConcurrentUpdate = new() { Key = nameof(ConcurrentUpdate), StatusCode = HttpStatusCode.Conflict };

    public const string UnexpectedError = "UnexpectedError";
}

/// <summary>Why one row of the file was rejected. The rest of the file is still imported.</summary>
public static class ProductImportRowErrors
{
    public static readonly Error MissingName = new() { Key = nameof(MissingName) };
    public static readonly Error NameTooLong = new() { Key = nameof(NameTooLong) };
    public static readonly Error DescriptionTooLong = new() { Key = nameof(DescriptionTooLong) };
    public static readonly Error InvalidQuantity = new() { Key = nameof(InvalidQuantity) };
    public static readonly Error InvalidPrice = new() { Key = nameof(InvalidPrice) };
    public static readonly Error SkuTooLong = new() { Key = nameof(SkuTooLong) };

    /// <summary>The row names a SKU for a product that already exists under another one (or none); a SKU is set at creation only.</summary>
    public static readonly Error SkuDiffers = new() { Key = nameof(SkuDiffers) };
}
