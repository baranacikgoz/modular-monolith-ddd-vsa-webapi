using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
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

    public StoreStatus Status { get; private set; }

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
        store.Status = StoreStatus.Active;

        store.RaiseEvent(@event);

        return store;
    }

    /// <summary>
    ///     Idempotent/retry-safe: a store already Inactive returns Success as a no-op rather than an error, so
    ///     a caller retrying after a timeout (never sure whether the first call landed) doesn't need to check
    ///     current state first. Which event fires branches on whether any product still has stock on hand -
    ///     a downstream consumer (e.g. inventory reconciliation) needs to know that without re-querying every
    ///     product itself.
    /// </summary>
    public Result Deactivate()
    {
        if (Status == StoreStatus.Inactive)
        {
            return Result.Success;
        }

        var productsWithStockCount = _products.Count(p => p.Quantity > 0);

        // Always build the event before mutating (house style, not just for old-value reads).
        DomainEvent @event = productsWithStockCount > 0
            ? new V1StoreDeactivatedWithStockOnHandDomainEvent(Id, productsWithStockCount)
            : new V1StoreDeactivatedDomainEvent(Id);

        Status = StoreStatus.Inactive;
        RaiseEvent(@event);
        return Result.Success;
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
        var @event = new V1StoreNameUpdatedDomainEvent(Id, name);
        Name = name;
        RaiseEvent(@event);
    }

    private void UpdateDescription(string description)
    {
        var @event = new V1StoreDescriptionUpdatedDomainEvent(Id, description);
        Description = description;
        RaiseEvent(@event);
    }

    private void UpdateAddress(string address)
    {
        var @event = new V1StoreAddressUpdatedDomainEvent(Id, address);
        Address = address;
        RaiseEvent(@event);
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

        // Cascade: a post-mutation check of the collection deciding whether a second, higher-level event
        // fires alongside the primary one - removing this product left the store with none.
        if (_products.Count == 0)
        {
            RaiseEvent(new V1StoreEmptiedDomainEvent(Id));
        }
    }
}
