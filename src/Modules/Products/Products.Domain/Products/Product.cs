using System.Text.Json.Serialization;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Domain.Products;

public readonly record struct ProductId(DefaultIdType Value) : IStronglyTypedId, IComparable<ProductId>
{
    public static ProductId New()
    {
        return new ProductId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ProductId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }

    /// <summary>Keyset cursor comparisons: in a query EF translates them to SQL on the uuid column, so a page boundary
    /// follows Postgres' own uuid ordering. The C# bodies are only the in-memory fallback and order differently from
    /// Postgres, so never sort or compare already materialized ids with them.</summary>
    public static bool operator >(ProductId left, ProductId right)
    {
        return left.Value.CompareTo(right.Value) > 0;
    }

    public static bool operator <(ProductId left, ProductId right)
    {
        return left.Value.CompareTo(right.Value) < 0;
    }

    public static bool operator >=(ProductId left, ProductId right)
    {
        return left.Value.CompareTo(right.Value) >= 0;
    }

    public static bool operator <=(ProductId left, ProductId right)
    {
        return left.Value.CompareTo(right.Value) <= 0;
    }

    public int CompareTo(ProductId other)
    {
        return Value.CompareTo(other.Value);
    }
}

public class Product : AggregateRoot<ProductId>, ISearchLocalized
{
    private readonly List<ProductImage> _images = [];

    public Product() : base(new ProductId(DefaultIdType.Empty))
    {
    } // ORMs need a parameterless ctor

    public StoreId StoreId { get; private set; }

    [JsonIgnore] public Store Store { get; } = default!;

    public ProductTemplateId ProductTemplateId { get; private set; }

    [JsonIgnore] public ProductTemplate ProductTemplate { get; } = default!;

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    /// <summary>The store's own code for the product, unique within the store; null when the store keeps none. Set at creation only.</summary>
    public string? Sku { get; private set; }

    /// <summary>Size and weight of the product as shipped; null until the store sets them.</summary>
    public PackageDimensions? PackageDimensions { get; private set; }

    /// <summary>Name and value pairs the store describes the product with. Read and replaced as a whole (one jsonb column).</summary>
    public IReadOnlyList<ProductAttribute> Attributes { get; private set; } = [];

    // Per-row search config; stamped on insert by ApplySearchLanguageInterceptor (no domain event).
    public string Language { get; private set; } = "simple_unaccent";

    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    public static Product Create(StoreId storeId, ProductTemplateId productTemplateId, string name, string description,
        int quantity, decimal price, string? sku = null)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event =
            new V1ProductCreatedDomainEvent(id, storeId, productTemplateId, name, description, quantity, price);

        product.Id = id;
        product.StoreId = storeId;
        product.ProductTemplateId = productTemplateId;
        product.Name = name;
        product.Description = description;
        product.Quantity = quantity;
        product.Price = price;

        product.RaiseEvent(@event);

        // The created event is frozen and has no SKU, so the SKU gets an event of its own right after it.
        if (sku is not null)
        {
            var skuEvent = new V1ProductSkuAssignedDomainEvent(id, sku);
            product.Sku = sku;
            product.RaiseEvent(skuEvent);
        }

        return product;
    }

    public void Update(string? name, string? description, int? quantity, decimal? price)
    {
        if (!string.IsNullOrEmpty(name) && !string.Equals(Name, name, StringComparison.Ordinal))
        {
            UpdateName(name);
        }

        if (!string.IsNullOrEmpty(description) && !string.Equals(Description, description, StringComparison.Ordinal))
        {
            UpdateDescription(description);
        }

        if (quantity.HasValue && quantity.Value != Quantity)
        {
            UpdateQuantity(quantity.Value);
        }

        if (price.HasValue && price.Value != Price)
        {
            UpdatePrice(price.Value);
        }
    }

    private void UpdateName(string name)
    {
        if (string.Equals(Name, name, StringComparison.Ordinal))
        {
            return;
        }

        var @event = new V1ProductNameUpdatedDomainEvent(Id, name);
        Name = name;
        RaiseEvent(@event);
    }

    private void UpdateDescription(string description)
    {
        if (string.Equals(Description, description, StringComparison.Ordinal))
        {
            return;
        }

        var @event = new V1ProductDescriptionUpdatedDomainEvent(Id, description);
        Description = description;
        RaiseEvent(@event);
    }

    private void UpdateQuantity(int quantity)
    {
        if (quantity == Quantity)
        {
            return;
        }

        // Always build the event before mutating (house style, not just for old-value reads).
        var @event = quantity > Quantity
            ? new V1ProductQuantityIncreasedDomainEvent(Id, quantity)
            : (DomainEvent)new V1ProductQuantityDecreasedDomainEvent(Id, quantity);

        Quantity = quantity;
        RaiseEvent(@event);
    }

    private void UpdatePrice(decimal price)
    {
        if (price == Price)
        {
            return;
        }

        // Always build the event before mutating (house style, not just for old-value reads).
        var @event = price > Price
            ? new V1ProductPriceIncreasedDomainEvent(Id, price)
            : (DomainEvent)new V1ProductPriceDecreasedDomainEvent(Id, price);

        Price = price;
        RaiseEvent(@event);
    }

    /// <summary>The same values as the current ones are no change and raise nothing.</summary>
    public void SetPackageDimensions(PackageDimensions dimensions)
    {
        if (dimensions.Equals(PackageDimensions))
        {
            return;
        }

        var @event = new V1ProductPackageDimensionsChangedDomainEvent(Id, dimensions.ToSnapshot());

        PackageDimensions = dimensions;
        RaiseEvent(@event);
    }

    /// <summary>Replaces the whole list. The same content in the same order is no change and raises nothing; the maximum comes
    /// from the caller (it is an option), the rest of the rules are the product's own.</summary>
    public Result SetAttributes(IReadOnlyList<ProductAttribute> attributes, int maxAttributes)
    {
        if (attributes.Count > maxAttributes)
        {
            return ProductErrors.TooManyAttributes;
        }

        if (attributes.Select(a => a.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != attributes.Count)
        {
            return ProductErrors.DuplicateAttributeName;
        }

        if (Attributes.SequenceEqual(attributes))
        {
            return Result.Success;
        }

        var @event = new V1ProductAttributesReplacedDomainEvent(Id, attributes.Select(a => a.ToSnapshot()).ToList());

        Attributes = attributes.ToList();
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>The caller has uploaded (or is about to upload) the file under <paramref name="objectKey" />; the key is a
    /// content hash, so the same bytes never become two images of one product.</summary>
    public Result<ProductImage> AddImage(string objectKey, string contentType, long sizeBytes, int maxImages)
    {
        if (_images.Count >= maxImages)
        {
            return ProductErrors.ImageLimitReached;
        }

        if (_images.Any(i => i.ObjectKey == objectKey))
        {
            return ProductErrors.ImageAlreadyAdded;
        }

        var image = ProductImage.Create(Id, objectKey, contentType, sizeBytes);
        var @event = new V1ProductImageAddedDomainEvent(Id, image.Id, objectKey);

        _images.Add(image);
        RaiseEvent(@event);

        return image;
    }

    public void RemoveImage(ProductImage image)
    {
        var @event = new V1ProductImageRemovedDomainEvent(Id, image.Id, image.ObjectKey);

        _images.Remove(image);
        RaiseEvent(@event);
    }
}
