using Common.Core.Contracts;
using Common.Core.Interfaces;
using Sales.Features.Products.Domain.DomainEvents;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain;

public readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
}

public class Product : AggregateRoot<ProductId>
{
    private Product(ProductCreatedDomainEvent @event)
        : base(@event.Id)
    {
        Apply(@event);
    }

    public StoreId StoreId { get; private set; }
    public virtual Store? Store { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Product Create(StoreId storeId, string name, string description)
    {
        var id = ProductId.New();
        var @event = new ProductCreatedDomainEvent(id, storeId, name, description);

        var product = new Product(@event);
        product.EnqueueEvent(@event);

        return product;
    }

    protected override void ApplyEvent(IEvent @event)
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
        StoreId = @event.StoreId;
        Name = @event.Name;
        Description = @event.Description;
    }

#pragma warning disable CS8618
    private Product() : base(ProductId.New()) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
