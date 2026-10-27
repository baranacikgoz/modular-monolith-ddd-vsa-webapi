using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.AddImage;

public sealed record Response
{
    public required ProductImageId Id { get; init; }

    /// <summary>Public, unsigned address of the stored file.</summary>
    public required Uri Url { get; init; }
}
