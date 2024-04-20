using Common.Core.Contracts;
using Common.Core.Contracts.Identity;
using Common.Core.Contracts.Results;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain.DomainEvents;

namespace Sales.Features.Stores.Domain;

public readonly record struct StoreId(Guid Value) : IStronglyTypedId
{
    public static StoreId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out StoreId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class Store : AggregateRoot<StoreId>
{
    public ApplicationUserId OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(ApplicationUserId ownerId, string name)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new StoreCreatedDomainEvent(id, ownerId, name);
        store.RaiseEvent(@event);

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

    protected override void ApplyEvent(DomainEvent @event)
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
        Id = @event.Id;
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
    private Store() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
