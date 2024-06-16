using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores.DomainEvents.v1;

namespace Inventory.Domain.Stores;

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
    public string Description { get; private set; }
    public Uri? LogoUrl { get; private set; }

    private readonly List<StoreProduct> _products = [];
    public virtual IReadOnlyCollection<StoreProduct> Products => _products.AsReadOnly();

    public static Store Create(ApplicationUserId ownerId, string name, string description, Uri? logoUrl = null)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new V1StoreCreatedDomainEvent(id, ownerId, name, description, logoUrl);
        store.RaiseEvent(@event);

        return store;
    }

    public void Update(string? name, string? description)
    {
        if (!string.IsNullOrEmpty(name) && !string.Equals(Name, name, StringComparison.Ordinal))
        {
            UpdateName(name);
        }

        if (!string.IsNullOrEmpty(description) && !string.Equals(Description, description, StringComparison.Ordinal))
        {
            UpdateDescription(description);
        }
    }

    private void UpdateName(string newName)
    {
        var storeNameUpdatedEvent = new V1StoreNameUpdatedDomainEvent(Id, Name, newName);
        RaiseEvent(storeNameUpdatedEvent);
    }

    private void UpdateDescription(string newDescription)
    {
        var storeDescriptionUpdatedEvent = new V1StoreDescriptionUpdatedDomainEvent(Id, Description, newDescription);
        RaiseEvent(storeDescriptionUpdatedEvent);
    }

    public StoreProduct AddProduct(ProductId productId, int quantity, decimal price)
    {
        var storeProduct = StoreProduct.Create(Id, productId, quantity, price);
        var @event = new V1ProductAddedToStoreDomainEvent(Id, storeProduct);
        RaiseEvent(@event);

        return storeProduct;
    }

    public Result UpdateProduct(StoreProductId productId, int? newQuantity, decimal? newPrice)
        => Result<StoreProduct>
            .Create(
                funcToGetValue: () => _products.SingleOrDefault(p => p.Id == productId),
                errorIfValueNull: Error.NotFound(nameof(StoreProduct), productId))
            .TapWhen(product => UpdateProductQuantity(product, newQuantity!.Value), when: () => newQuantity is not null)
            .TapWhen(product => UpdateProductPrice(product, newPrice!.Value), when: () => newPrice is not null);

    private void UpdateProductQuantity(StoreProduct product, int newQuantity)
    {
        if (newQuantity == product.Quantity)
        {
            return;
        }

        RaiseEvent(
            newQuantity > product.Quantity
            ? new V1ProductQuantityIncreasedDomainEvent(product, newQuantity)
            : new V1ProductQuantityDecreasedDomainEvent(product, newQuantity));
    }

    private void UpdateProductPrice(StoreProduct product, decimal newPrice)
    {
        if (newPrice == product.Price)
        {
            return;
        }

        RaiseEvent(
            newPrice > product.Price
            ? new V1ProductPriceIncreasedDomainEvent(product, newPrice)
            : new V1ProductPriceDecreasedDomainEvent(product, newPrice));
    }

    public Result RemoveProductFromStore(StoreProductId productId)
    {
        if (_products.SingleOrDefault(p => p.Id == productId) is not { } product)
        {
            return Error.NotFound(nameof(StoreProduct), productId);
        }

        var @event = new V1ProductRemovedFromStoreDomainEvent(Id, product);
        RaiseEvent(@event);

        return Result.Success;
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
            case V1ProductAddedToStoreDomainEvent e:
                Apply(e);
                break;
            case V1ProductRemovedFromStoreDomainEvent e:
                Apply(e);
                break;
            case V1ProductQuantityIncreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductQuantityDecreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductPriceIncreasedDomainEvent e:
                Apply(e);
                break;
            case V1ProductPriceDecreasedDomainEvent e:
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
        LogoUrl = @event.LogoUrl;
    }

    private void Apply(V1StoreNameUpdatedDomainEvent @event)
    {
        Name = @event.NewName;
    }

    private void Apply(V1StoreDescriptionUpdatedDomainEvent @event)
    {
        Description = @event.NewDescription;
    }

    private void Apply(V1ProductAddedToStoreDomainEvent @event)
    {
        _products.Add(@event.Product);
    }

    private void Apply(V1ProductRemovedFromStoreDomainEvent @event)
    {
        _products.Remove(@event.Product);
    }

    private static void Apply(V1ProductQuantityIncreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newQuantity = @event.NewQuantity;

        storeProduct.UpdateQuantity(newQuantity);
    }

    private static void Apply(V1ProductQuantityDecreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newQuantity = @event.NewQuantity;

        storeProduct.UpdateQuantity(newQuantity);
    }

    private static void Apply(V1ProductPriceIncreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newPrice = @event.NewPrice;

        storeProduct.UpdatePrice(newPrice);
    }

    private static void Apply(V1ProductPriceDecreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newPrice = @event.NewPrice;

        storeProduct.UpdatePrice(newPrice);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Store() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
