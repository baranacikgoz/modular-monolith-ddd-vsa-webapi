using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Interfaces;
using Common.Events;
using Sales.Features.Products.Domain;

namespace Sales.Features.Stores.Domain;

internal readonly record struct StoreId(Guid Value)
{
    public static StoreId New() => new(Guid.NewGuid());
}

internal class Store : AggregateRoot<StoreId>
{
    private Store(StoreCreatedDomainEvent @event)
        : base(new StoreId(@event.EventId))
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
        var @event = new StoreCreatedDomainEvent(id.Value, ownerId, name);
        var store = new Store(@event);
        store.EnqueueEvent(@event);

        return store;
    }

    public void AddProduct(string name, string description)
    {
        var @event = new ProductAddedDomainEvent(Id.Value, name, description);
        RaiseEvent(@event);
    }

    public Result RemoveProduct(ProductId productId)
        => Result<bool>
            .Create(() => _products.Exists(p => p.Id == productId))
            .Bind(isExist => isExist ? Result.Success : Error.NotFound(nameof(Product), productId.Value))
            .Tap(_ => RaiseEvent(new ProductRemovedDomainEvent(Id.Value, productId.Value)));

    protected override void ApplyEvent(IEvent @event)
    {
        switch (@event)
        {
            case StoreCreatedDomainEvent e:
                Apply(e);
                break;
            case ProductAddedDomainEvent e:
                Apply(e);
                break;
            case ProductRemovedDomainEvent e:
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

    private void Apply(ProductAddedDomainEvent @event)
    {
        _products.Add(Product.Create(Id, @event.Name, @event.Description));
    }

    private void Apply(ProductRemovedDomainEvent @event)
    {
        var product = _products.Find(p => p.Id.Value == @event.ProductId);
        ArgumentNullException.ThrowIfNull(product);

        _products.Remove(product);
    }

#pragma warning disable CS8618
    private Store() : base(StoreId.New()) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
