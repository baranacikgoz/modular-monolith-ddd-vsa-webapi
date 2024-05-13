using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Products.DomainEvents;
using Inventory.Domain.Stores;
using Inventory.Domain.Stores.DomainEvents;

namespace Inventory.Domain.Products;

public readonly record struct ProductId(Guid Value) : IStronglyTypedId
{
    public static ProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out ProductId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Product Create(string name, string description)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event = new ProductCreatedDomainEvent(id, name, description);
        product.RaiseEvent(@event);

        return product;
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case ProductCreatedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(ProductCreatedDomainEvent @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
    }

    protected override void UndoEvent(DomainEvent @event)
    {
        throw new NotImplementedException();
    }

    public Product() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
}
