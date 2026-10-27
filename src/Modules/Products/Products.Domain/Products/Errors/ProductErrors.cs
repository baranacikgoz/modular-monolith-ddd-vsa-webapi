using System.Net;
using Common.Domain.ResultMonad;

namespace Products.Domain.Products.Errors;

public static class ProductErrors
{
    public static readonly Error ImageLimitReached = new() { Key = nameof(ImageLimitReached), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error ImageAlreadyAdded = new() { Key = nameof(ImageAlreadyAdded), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error ImageFileTooLarge = new() { Key = nameof(ImageFileTooLarge), StatusCode = HttpStatusCode.RequestEntityTooLarge };

    public static readonly Error ImageUnsupportedType = new() { Key = nameof(ImageUnsupportedType), StatusCode = HttpStatusCode.UnsupportedMediaType };
}
