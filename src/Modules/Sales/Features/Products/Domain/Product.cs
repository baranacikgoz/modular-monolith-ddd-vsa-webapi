using Common.Core.Contracts;
using Sales.Features.Products.Domain.DomainEvents;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain;

public readonly record struct ProductId(Guid Value) : IStronglyTypedId
{
    public static ProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class Product : AggregateRoot<ProductId>
{
    public StoreId StoreId { get; private set; }
    public virtual Store? Store { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Product Create(StoreId storeId, string name, string description)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event = new ProductCreatedDomainEvent(id, storeId, name, description);
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

    public void Apply(ProductCreatedDomainEvent @event)
    {
        Id = @event.Id;
        StoreId = @event.StoreId;
        Name = @event.Name;
        Description = @event.Description;
    }

#pragma warning disable CS8618
    private Product() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
