using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Domain.Stores;

public readonly record struct StoreId(DefaultIdType Value) : IStronglyTypedId
{
    public static StoreId New() => new(DefaultIdType.CreateVersion7());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out StoreId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class Store : AggregateRoot<StoreId>
{
    public ApplicationUserId OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; }
    public string Address { get; private set; }

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(ApplicationUserId ownerId, string name, string description, string address)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new V1StoreCreatedDomainEvent(id, ownerId, name, description, address);
        store.RaiseEvent(@event);

        return store;
    }

    public void Update(string? name, string? description, string? address)
    {
        if (!string.IsNullOrEmpty(name) && !string.Equals(Name, name, StringComparison.Ordinal))
        {
            UpdateName(name);
        }

        if (!string.IsNullOrEmpty(description) && !string.Equals(Description, description, StringComparison.Ordinal))
        {
            UpdateDescription(description);
        }

        if (!string.IsNullOrEmpty(address) && !string.Equals(Address, address, StringComparison.Ordinal))
        {
            UpdateAddress(address);
        }
    }

    private void UpdateName(string name)
    {
        var storeNameUpdatedEvent = new V1StoreNameUpdatedDomainEvent(Id, name);
        RaiseEvent(storeNameUpdatedEvent);
    }

    private void UpdateDescription(string description)
    {
        var storeDescriptionUpdatedEvent = new V1StoreDescriptionUpdatedDomainEvent(Id, description);
        RaiseEvent(storeDescriptionUpdatedEvent);
    }

    private void UpdateAddress(string address)
    {
        var storeAddressUpdatedEvent = new V1StoreAddressUpdatedDomainEvent(Id, address);
        RaiseEvent(storeAddressUpdatedEvent);
    }

    public void AddProduct(Product product)
    {
        var @event = new V1ProductAddedToStoreDomainEvent(Id, product);
        RaiseEvent(@event);
    }

    public void RemoveProduct(Product product)
    {
        var @event = new V1ProductRemovedFromStoreDomainEvent(Id, product);
        RaiseEvent(@event);
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case V1StoreCreatedDomainEvent e:
                Apply(e);
                break;
            case V1StoreNameUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1StoreDescriptionUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1StoreAddressUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1ProductAddedToStoreDomainEvent e:
                Apply(e);
                break;
            case V1ProductRemovedFromStoreDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Can not apply the unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(V1StoreCreatedDomainEvent @event)
    {
        Id = @event.StoreId;
        OwnerId = @event.OwnerId;
        Name = @event.Name;
        Description = @event.Description;
        Address = @event.Address;
    }

    private void Apply(V1StoreNameUpdatedDomainEvent @event)
    {
        Name = @event.Name;
    }

    private void Apply(V1StoreDescriptionUpdatedDomainEvent @event)
    {
        Description = @event.Description;
    }

    private void Apply(V1StoreAddressUpdatedDomainEvent @event)
    {
        Address = @event.Address;
    }

    private void Apply(V1ProductAddedToStoreDomainEvent @event)
    {
        _products.Add(@event.Product);
    }

    private void Apply(V1ProductRemovedFromStoreDomainEvent @event)
    {
        _products.Remove(@event.Product);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Store() : base(new(DefaultIdType.Empty)) { } // ORMs need parameterlers ctor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
