using System.Text.Json.Serialization;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Domain.Products;

public readonly record struct ProductId(DefaultIdType Value) : IStronglyTypedId
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
}

public class Product : AggregateRoot<ProductId>, ISearchLocalized
{
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

    // Per-row search config; stamped on insert by ApplySearchLanguageInterceptor (no domain event).
    public string Language { get; private set; } = "simple_unaccent";

    public static Product Create(StoreId storeId, ProductTemplateId productTemplateId, string name, string description,
        int quantity, decimal price)
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
}
