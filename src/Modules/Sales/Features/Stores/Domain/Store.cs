using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Interfaces;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain.DomainEvents;

namespace Sales.Features.Stores.Domain;

public readonly record struct StoreId(Guid Value)
{
    public static StoreId New() => new(Guid.NewGuid());
}

public class Store : AggregateRoot<StoreId>
{
    private Store(StoreCreatedDomainEvent @event)
        : base(@event.Id)
    {
        // Constructors should be side-effect free (for serialization/deserialization etc.)
        // That's why I used "Apply" instead of "RaiseEvent".
        // Event is Enqueued in "Create" factory method.

        Apply(@event);
    }

    public Guid OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(Guid ownerId, string name)
    {
        var id = StoreId.New();
        var @event = new StoreCreatedDomainEvent(id, ownerId, name);
        var store = new Store(@event);
        store.EnqueueEvent(@event);

        return store;
    }

    public void AddProduct(Product product)
    {
        var @event = new ProductAddedToStoreDomainEvent(this, product);
        RaiseEvent(@event);
    }

    public Result RemoveProduct(Product product)
        => Result<bool>
            .Create(() => _products.Exists(p => p.Id == product.Id))
            .Bind(isExist => isExist ? Result.Success : Error.NotFound(nameof(Product), product.Id.Value))
            .Tap(_ => RaiseEvent(new ProductRemovedFromStoreDomainEvent(this, product)));

    protected override void ApplyEvent(IEvent @event)
    {
        switch (@event)
        {
            case StoreCreatedDomainEvent e:
                Apply(e);
                break;
            case ProductAddedToStoreDomainEvent e:
                Apply(e);
                break;
            case ProductRemovedFromStoreDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(StoreCreatedDomainEvent @event)
    {
        OwnerId = @event.OwnerId;
        Name = @event.Name;
    }

    private void Apply(ProductAddedToStoreDomainEvent @event)
    {
        _products.Add(@event.Product);
    }

    private void Apply(ProductRemovedFromStoreDomainEvent @event)
    {
        _products.Remove(@event.Product);
    }

#pragma warning disable CS8618
    private Store() : base(StoreId.New()) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
