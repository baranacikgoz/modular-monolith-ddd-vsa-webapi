using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;

namespace Products.Domain.ProductTemplates;

public readonly record struct ProductTemplateId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductTemplateId New()
    {
        return new ProductTemplateId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ProductTemplateId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     ProductTemplates are the immutable product templates are defined by the platform admins to declare what kind of
///     products can be sold on the platform.
///     Store owners create their store-specific products from this.
///     Quantity, price, and any other store-specific information is not defined here, but in the <see cref="Product" />
///     entity.
/// </summary>
public class ProductTemplate : AuditableEntity<ProductTemplateId>
{
    private readonly List<Product> _products = [];

    public ProductTemplate() : base(new ProductTemplateId(DefaultIdType.Empty))
    {
    } // ORMs need parameterlers ctor

    /// <summary>
    ///     Since the ProductTemplate is both immutable and undeletable, IsActive is used to disable the product template for
    ///     future use.
    /// </summary>
    public required bool IsActive { get; set; }

    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required string Color { get; init; }
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    public static ProductTemplate Create(string brand, string model, string color)
    {
        return new ProductTemplate
        {
            Id = ProductTemplateId.New(),
            IsActive = true,
            Brand = brand,
            Model = model,
            Color = color
        };
    }

    public Result Activate()
    {
        IsActive = true;
        return Result.Success;
    }

    public Result Deactivate()
    {
        IsActive = false;
        return Result.Success;
    }
}
