using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Products.Domain.Products;

public readonly record struct ProductImageId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductImageId New()
    {
        return new ProductImageId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ProductImageId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>An image of a <see cref="Product" />. The file sits in the public object store under
/// <see cref="ObjectKey" />; the row only points at it.</summary>
public class ProductImage : AuditableEntity<ProductImageId>
{
    public ProductImage() : base(new ProductImageId(DefaultIdType.Empty))
    {
    } // ORMs need a parameterless ctor

    public ProductId ProductId { get; private set; }
    public string ObjectKey { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }

    internal static ProductImage Create(ProductId productId, string objectKey, string contentType, long sizeBytes)
    {
        return new ProductImage
        {
            Id = ProductImageId.New(),
            ProductId = productId,
            ObjectKey = objectKey,
            ContentType = contentType,
            SizeBytes = sizeBytes
        };
    }
}
