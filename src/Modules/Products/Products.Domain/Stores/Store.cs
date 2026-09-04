using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Domain.Stores;

public readonly record struct StoreId(DefaultIdType Value) : IStronglyTypedId
{
    public static StoreId New()
    {
        return new StoreId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StoreId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

public class Store : AggregateRoot<StoreId>, ISearchLocalized
{
    private readonly List<Product> _products = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Store() : base(new StoreId(DefaultIdType.Empty))
    {
    } // ORMs need parameterlers ctor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationUserId OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; }
    public string Address { get; private set; }

    // Per-row search config; stamped on insert by ApplySearchLanguageInterceptor (no domain event).
    public string Language { get; private set; } = "simple_unaccent";

    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(ApplicationUserId ownerId, string name, string description, string address)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new V1StoreCreatedDomainEvent(id, ownerId, name, description, address);

        store.Id = id;
        store.OwnerId = ownerId;
        store.Name = name;
        store.Description = description;
        store.Address = address;

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
        Name = name;
        RaiseEvent(new V1StoreNameUpdatedDomainEvent(Id, name));
    }

    private void UpdateDescription(string description)
    {
        Description = description;
        RaiseEvent(new V1StoreDescriptionUpdatedDomainEvent(Id, description));
    }

    private void UpdateAddress(string address)
    {
        Address = address;
        RaiseEvent(new V1StoreAddressUpdatedDomainEvent(Id, address));
    }

    // Event carries only a ProductSnapshot (CLAUDE.md §5), never the live entity: EF's Products
    // navigation (HasField("_products")) needs the real instance to track the FK.
    public void AddProduct(Product product)
    {
        _products.Add(product);
        var @event = new V1ProductAddedToStoreDomainEvent(Id, product.ToAddedSnapshot());
        RaiseEvent(@event);
    }

    public void RemoveProduct(Product product)
    {
        _products.Remove(product);
        var @event = new V1ProductRemovedFromStoreDomainEvent(Id, product.ToRemovedSnapshot());
        RaiseEvent(@event);
    }
}
