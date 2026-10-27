using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Stores.DomainEvents.v1;
using Products.Domain.Stores.DomainEvents.v2;
using Products.Domain.Stores.Errors;

namespace Products.Domain.Stores;

public readonly record struct StoreId(DefaultIdType Value) : IStronglyTypedId, IComparable<StoreId>
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

    /// <summary>Keyset cursor comparisons: in a query EF translates them to SQL on the uuid column, so a page boundary
    /// follows Postgres' own uuid ordering. The C# bodies are only the in-memory fallback and order differently from
    /// Postgres, so never sort or compare already materialized ids with them.</summary>
    public static bool operator >(StoreId left, StoreId right)
    {
        return left.Value.CompareTo(right.Value) > 0;
    }

    public static bool operator <(StoreId left, StoreId right)
    {
        return left.Value.CompareTo(right.Value) < 0;
    }

    public static bool operator >=(StoreId left, StoreId right)
    {
        return left.Value.CompareTo(right.Value) >= 0;
    }

    public static bool operator <=(StoreId left, StoreId right)
    {
        return left.Value.CompareTo(right.Value) <= 0;
    }

    public int CompareTo(StoreId other)
    {
        return Value.CompareTo(other.Value);
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

    /// <summary>A store that waits for an administrator's decision. Create opens an Active store directly; this opens a PendingApproval one.</summary>
    public static Store Apply(ApplicationUserId ownerId, string name, string description, string address)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new V1StoreApplicationSubmittedDomainEvent(id, ownerId, name, description, address);

        store.Id = id;
        store.OwnerId = ownerId;
        store.Name = name;
        store.Description = description;
        store.Address = address;
        store.Status = StoreStatus.PendingApproval;

        store.RaiseEvent(@event);

        return store;
    }

    /// <summary>Idempotent like <see cref="Deactivate" />: an Active store succeeds without an event, so a retry after a timeout publishes nothing twice.</summary>
    public Result Approve()
    {
        if (Status == StoreStatus.Active)
        {
            return Result.Success;
        }

        if (Status != StoreStatus.PendingApproval)
        {
            return StoreErrors.NotPendingApproval;
        }

        var @event = new V1StoreApprovedDomainEvent(Id, OwnerId);

        Status = StoreStatus.Active;
        RaiseEvent(@event);
        return Result.Success;
    }

    public Result Suspend()
    {
        if (Status == StoreStatus.Suspended)
        {
            return Result.Success;
        }

        if (Status != StoreStatus.Active)
        {
            return StoreErrors.NotActive;
        }

        var @event = new V1StoreSuspendedDomainEvent(Id);

        Status = StoreStatus.Suspended;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>Only a Suspended store comes back; a store that was deactivated, even after a suspension, stays closed.</summary>
    public Result Reinstate()
    {
        if (Status == StoreStatus.Active)
        {
            return Result.Success;
        }

        if (Status != StoreStatus.Suspended)
        {
            return StoreErrors.NotSuspended;
        }

        var @event = new V1StoreReinstatedDomainEvent(Id);

        Status = StoreStatus.Active;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>
    ///     Idempotent/retry-safe: a store already Inactive returns Success as a no-op rather than an error, so
    ///     a caller retrying after a timeout (never sure whether the first call landed) doesn't need to check
    ///     current state first. Which event fires branches on whether any product still has stock on hand -
    ///     a downstream consumer (e.g. inventory reconciliation) needs to know that without re-querying every
    ///     product itself. A Suspended store can be deactivated, and deactivating a PendingApproval one rejects its application.
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

    /// <summary>Only an Active store changes; every other status answers with the error of that status and changes nothing.</summary>
    public Result Update(string? name, string? description, string? address)
    {
        return EnsureActive().Tap(() =>
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
        });
    }

    /// <summary>
    ///     Succeeds for an Active store; every other status answers with the error of that status. For work the store's own
    ///     methods do not cover, such as accepting a queued import before its rows are read.
    /// </summary>
    public Result EnsureActive()
    {
        return Status switch
        {
            StoreStatus.Active => Result.Success,
            StoreStatus.PendingApproval => StoreErrors.StoreIsPendingApproval,
            StoreStatus.Suspended => StoreErrors.StoreIsSuspended,
            StoreStatus.Inactive => StoreErrors.StoreIsInactive,
            _ => throw new InvalidOperationException($"Unknown {nameof(StoreStatus)}: {Status}")
        };
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
        var @event = new V2StoreAddressUpdatedDomainEvent(Id, address, Address);
        Address = address;
        RaiseEvent(@event);
    }

    // Event carries only a ProductSnapshot (CLAUDE.md §5), never the live entity: EF's Products
    // navigation (HasField("_products")) needs the real instance to track the FK.
    /// <summary>Only an Active store takes products; every other status answers with the error of that status and changes nothing.</summary>
    public Result AddProduct(Product product)
    {
        return EnsureActive().Tap(() =>
        {
            _products.Add(product);
            var @event = new V1ProductAddedToStoreDomainEvent(Id, product.ToAddedSnapshot());
            RaiseEvent(@event);
        });
    }

    // The store is the gate for every write to one of its products: the Product mutators are internal, so nothing outside
    // this assembly can change a product without going through a method below. The caller loads the product with its store
    // (Include(p => p.Store)) and passes a product that belongs to this store.

    /// <summary>Only an Active store's products change; every other status answers with the error of that status and changes nothing.</summary>
    public Result UpdateProduct(Product product, string? name, string? description, int? quantity, decimal? price)
    {
        return EnsureActive().Tap(() => product.Update(name, description, quantity, price));
    }

    /// <summary>Only an Active store's products change; see <see cref="UpdateProduct" />. A refused list (too many, duplicate name) is the product's own error.</summary>
    public Result SetProductAttributes(Product product, IReadOnlyList<ProductAttribute> attributes, int maxAttributes)
    {
        return EnsureActive().Tap(() => product.SetAttributes(attributes, maxAttributes));
    }

    /// <summary>Only an Active store's products change; see <see cref="UpdateProduct" />.</summary>
    public Result SetProductPackageDimensions(Product product, PackageDimensions dimensions)
    {
        return EnsureActive().Tap(() => product.SetPackageDimensions(dimensions));
    }

    /// <summary>Only an Active store's products change; see <see cref="UpdateProduct" />. The product loads with its images.</summary>
    public Result<ProductImage> AddProductImage(Product product, string objectKey, string contentType, long sizeBytes, int maxImages)
    {
        return EnsureActive().Match(
            () => product.AddImage(objectKey, contentType, sizeBytes, maxImages),
            Result<ProductImage>.Failure);
    }

    /// <summary>Only an Active store's products change; see <see cref="UpdateProduct" />. The image's object leaves in <see cref="V1ProductImageRemovedDomainEvent" />.</summary>
    public Result RemoveProductImage(Product product, ProductImage image)
    {
        return EnsureActive().Tap(() => product.RemoveImage(image));
    }

    /// <summary>
    ///     Only an Active store gives products up; every other status answers with the error of that status and changes nothing.
    ///     The product's image rows are deleted with it, so the caller loads <see cref="Product.Images" /> with the product: their
    ///     object keys leave in <see cref="V1ProductImagesRemovedWithProductDomainEvent" />, otherwise the files stay behind.
    /// </summary>
    public Result RemoveProduct(Product product)
    {
        return EnsureActive().Tap(() =>
        {
            _products.Remove(product);
            var @event = new V1ProductRemovedFromStoreDomainEvent(Id, product.ToRemovedSnapshot());
            RaiseEvent(@event);

            if (product.Images.Count > 0)
            {
                RaiseEvent(new V1ProductImagesRemovedWithProductDomainEvent(Id, product.Id, product.Images.Select(i => i.ObjectKey).ToList()));
            }

            // Cascade: a post-mutation check of the collection deciding whether a second, higher-level event
            // fires alongside the primary one - removing this product left the store with none.
            if (_products.Count == 0)
            {
                RaiseEvent(new V1StoreEmptiedDomainEvent(Id));
            }
        });
    }
}
