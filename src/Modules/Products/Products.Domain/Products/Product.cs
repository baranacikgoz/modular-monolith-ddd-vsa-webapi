using System.Text.Json.Serialization;
using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Domain.Products;

public readonly record struct ProductId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductId New() => new(DefaultIdType.CreateVersion7());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out ProductId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class Product : AggregateRoot<ProductId>
{
    public StoreId StoreId { get; private set; }

    [JsonIgnore]
    public Store Store { get; } = default!;

    public ProductTemplateId ProductTemplateId { get; private set; }

    [JsonIgnore]
    public ProductTemplate ProductTemplate { get; } = default!;

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public static Product Create(StoreId storeId, ProductTemplateId productTemplateId, string name, string description, int quantity, decimal price)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event = new V1ProductCreatedDomainEvent(id, storeId, productTemplateId, name, description, quantity, price);
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

        RaiseEvent(new V1ProductNameUpdatedDomainEvent(Id, name));
    }

    private void UpdateDescription(string description)
    {
        if (string.Equals(Description, description, StringComparison.Ordinal))
        {
            return;
        }

        RaiseEvent(new V1ProductDescriptionUpdatedDomainEvent(Id, description));
    }

    private void UpdateQuantity(int quantity)
    {
        if (quantity == Quantity)
        {
            return;
        }

        RaiseEvent(
            quantity > Quantity
            ? new V1ProductQuantityIncreasedDomainEvent(Id, quantity)
            : new V1ProductQuantityDecreasedDomainEvent(Id, quantity));
    }

    private void UpdatePrice(decimal price)
    {
        if (price == Price)
        {
            return;
        }

        RaiseEvent(
            price > Price
            ? new V1ProductPriceIncreasedDomainEvent(Id, price)
            : new V1ProductPriceDecreasedDomainEvent(Id, price));
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case V1ProductCreatedDomainEvent e:
                Apply(e);
                break;
            case V1ProductNameUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1ProductDescriptionUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1ProductQuantityIncreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductQuantityDecreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductPriceIncreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductPriceDecreasedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Can not apply the unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(V1ProductCreatedDomainEvent @event)
    {
        Id = @event.ProductId;
        StoreId = @event.StoreId;
        ProductTemplateId = @event.ProductTemplateId;
        Name = @event.Name;
        Description = @event.Description;
        Quantity = @event.Quantity;
        Price = @event.Price;
    }

    private void Apply(V1ProductNameUpdatedDomainEvent @event)
    {
        Name = @event.Name;
    }

    private void Apply(V1ProductDescriptionUpdatedDomainEvent @event)
    {
        Description = @event.Description;
    }

    private void Apply(V1ProductQuantityIncreasedDomainEvent @event)
    {
        Quantity = @event.Quantity;
    }

    private void Apply(V1ProductQuantityDecreasedDomainEvent @event)
    {
        Quantity = @event.Quantity;
    }

    private void Apply(V1ProductPriceIncreasedDomainEvent @event)
    {
        Price = @event.Price;
    }

    private void Apply(V1ProductPriceDecreasedDomainEvent @event)
    {
        Price = @event.Price;
    }

    public Product() : base(new(DefaultIdType.Empty)) { } // ORMs need a parameterless ctor
}
