using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.StoreProducts;

namespace Products.Domain.Products;

public readonly record struct ProductId(DefaultIdType Value) : IStronglyTypedId
{
    public static ProductId New() => new(DefaultIdType.CreateVersion7());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out ProductId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly List<StoreProduct> _storeProducts = [];
    public IReadOnlyList<StoreProduct> StoreProducts => _storeProducts.AsReadOnly();

    public static Product Create(string name, string description)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event = new V1ProductCreatedDomainEvent(id, name, description);
        product.RaiseEvent(@event);

        return product;
    }

    public void Update(string? name, string? description)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            UpdateName(name);
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            UpdateDescription(description);
        }
    }

    private void UpdateName(string newName)
    {
        var @event = new V1ProductNameUpdatedDomainEvent(Id, newName);
        RaiseEvent(@event);
    }

    private void UpdateDescription(string newDescription)
    {
        var @event = new V1ProductDescriptionUpdatedDomainEvent(Id, newDescription);
        RaiseEvent(@event);
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
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(V1ProductCreatedDomainEvent @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
    }

    private void Apply(V1ProductNameUpdatedDomainEvent @event)
    {
        Name = @event.Name;
    }

    private void Apply(V1ProductDescriptionUpdatedDomainEvent @event)
    {
        Description = @event.Description;
    }

    public Product() : base(new(DefaultIdType.Empty)) { } // ORMs need parameterlers ctor
}
