using System.Net;
using Common.Domain.ResultMonad;

namespace Products.Domain.Products.Errors;

public static class ProductErrors
{
    public static readonly Error ImageLimitReached = new() { Key = nameof(ImageLimitReached), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error ImageAlreadyAdded = new() { Key = nameof(ImageAlreadyAdded), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error ImageFileTooLarge = new() { Key = nameof(ImageFileTooLarge), StatusCode = HttpStatusCode.RequestEntityTooLarge };

    public static readonly Error ImageUnsupportedType = new() { Key = nameof(ImageUnsupportedType), StatusCode = HttpStatusCode.UnsupportedMediaType };

    /// <summary>Another product of the same store already carries the SKU.</summary>
    public static readonly Error SkuAlreadyExists = new() { Key = nameof(SkuAlreadyExists), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The store or the product template a product points at was removed between the read and the save.</summary>
    public static readonly Error ParentNoLongerExists = new() { Key = nameof(ParentNoLongerExists), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The list holds more attributes than <c>ProductsOptions.MaxAttributesPerProduct</c> allows.</summary>
    public static readonly Error TooManyAttributes = new() { Key = nameof(TooManyAttributes), StatusCode = HttpStatusCode.BadRequest };

    /// <summary>Two attributes share a name, compared without regard to case.</summary>
    public static readonly Error DuplicateAttributeName = new() { Key = nameof(DuplicateAttributeName), StatusCode = HttpStatusCode.BadRequest };

    /// <summary>A package side or the weight is not above zero, or is above the largest value a package may have.</summary>
    public static readonly Error InvalidPackageDimensions = new() { Key = nameof(InvalidPackageDimensions), StatusCode = HttpStatusCode.BadRequest };
}
