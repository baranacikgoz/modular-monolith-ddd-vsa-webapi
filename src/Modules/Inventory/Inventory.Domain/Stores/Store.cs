using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores.DomainEvents;

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

        var @event = new StoreCreatedDomainEvent(id, ownerId, name, description, logoUrl);
        store.RaiseEvent(@event);

        return store;
    }

    public Result UpdateName(string newName)
    {
        if (string.Equals(Name, newName, StringComparison.Ordinal))
        {
            return Error.SameValue(nameof(Name), newName);
        }

        var storeNameUpdatedEvent = new StoreNameUpdatedDomainEvent(Id, Name, newName);
        RaiseEvent(storeNameUpdatedEvent);
        return Result.Success;
    }

    public Result UpdateDescription(string newDescription)
    {
        if (string.Equals(Description, newDescription, StringComparison.Ordinal))
        {
            return Error.SameValue(nameof(Description), newDescription);
        }

        var storeDescriptionUpdatedEvent = new StoreDescriptionUpdatedDomainEvent(Id, Description, newDescription);
        RaiseEvent(storeDescriptionUpdatedEvent);
        return Result.Success;
    }

    public StoreProduct AddProduct(ProductId productId, int quantity, decimal price)
    {
        var storeProduct = StoreProduct.Create(Id, productId, quantity, price);
        var @event = new ProductAddedToStoreDomainEvent(Id, storeProduct);
        RaiseEvent(@event);

        return storeProduct;
    }

    public void UpdateProductQuantity(StoreProduct product, int newQuantity)
    {
        if (product.Quantity == newQuantity)
        {
            return;
        }

        RaiseEvent(
            newQuantity > product.Quantity
            ? new ProductQuantityIncreasedDomainEvent(product, newQuantity)
            : new ProductQuantityDecreasedDomainEvent(product, newQuantity));
    }

    public Result UpdateProductPrice(StoreProductId productId, decimal newPrice)
        => Result<StoreProduct>
            .Create(
                funcToGetValue: () => _products.SingleOrDefault(p => p.Id == productId),
                errorIfValueNull: Error.NotFound(nameof(StoreProduct), productId))
            .Tap(product => newPrice == product.Price
                                       ? Error.SameValue(nameof(StoreProduct), newPrice)
                                       : product)
            .Tap(product => RaiseEvent(
                                newPrice > product.Price
                                ? new ProductPriceIncreasedDomainEvent(product, newPrice)
                                : new ProductPriceDecreasedDomainEvent(product, newPrice)));

    public void RemoveProductFromStore(StoreProduct product)
    {
        var @event = new ProductRemovedFromStoreDomainEvent(Id, product);
        RaiseEvent(@event);
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case StoreCreatedDomainEvent e:
                Apply(e);
                break;
            case StoreNameUpdatedDomainEvent e:
                Apply(e);
                break;
            case StoreDescriptionUpdatedDomainEvent e:
                Apply(e);
                break;
            case ProductAddedToStoreDomainEvent e:
                Apply(e);
                break;
            case ProductRemovedFromStoreDomainEvent e:
                Apply(e);
                break;
            case ProductQuantityIncreasedDomainEvent e:
                Apply(e);
                break;
            case ProductQuantityDecreasedDomainEvent e:
                Apply(e);
                break;
            case ProductPriceIncreasedDomainEvent e:
                Apply(e);
                break;
            case ProductPriceDecreasedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Can not apply the unknown event {@event.GetType().Name}");
        }
    }

    protected override void UndoEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case StoreCreatedDomainEvent _:
                throw new InvalidOperationException($"{nameof(StoreCreatedDomainEvent)} is undoable.");
            case StoreNameUpdatedDomainEvent e:
                UndoWith(e, new StoreNameUpdatedDomainEvent(Id, OldName: e.NewName, NewName: e.OldName));
                break;
            case StoreDescriptionUpdatedDomainEvent e:
                UndoWith(e, new StoreDescriptionUpdatedDomainEvent(Id, OldDescription: e.NewDescription, NewDescription: e.OldDescription));
                break;
            case ProductAddedToStoreDomainEvent e:
                UndoWith(e, new ProductRemovedFromStoreDomainEvent(Id, e.Product));
                break;
            case ProductRemovedFromStoreDomainEvent e:
                UndoWith(e, new ProductAddedToStoreDomainEvent(Id, e.Product));
                break;
            case ProductQuantityIncreasedDomainEvent e:
                UndoWith(e, new ProductQuantityDecreasedDomainEvent(e.Product, NewQuantity: e.Product.Quantity));
                break;
            case ProductQuantityDecreasedDomainEvent e:
                UndoWith(e, new ProductQuantityIncreasedDomainEvent(e.Product, NewQuantity: e.Product.Quantity));
                break;
            case ProductPriceIncreasedDomainEvent e:
                UndoWith(e, new ProductPriceDecreasedDomainEvent(e.Product, NewPrice: e.Product.Price));
                break;
            case ProductPriceDecreasedDomainEvent e:
                UndoWith(e, new ProductPriceIncreasedDomainEvent(e.Product, NewPrice: e.Product.Price));
                break;
            default:
                throw new InvalidOperationException($"Can not undo the unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(StoreCreatedDomainEvent @event)
    {
        Id = @event.StoreId;
        OwnerId = @event.OwnerId;
        Name = @event.Name;
        Description = @event.Description;
        LogoUrl = @event.LogoUrl;
    }

    private void Apply(StoreNameUpdatedDomainEvent @event)
    {
        Name = @event.NewName;
    }

    private void Apply(StoreDescriptionUpdatedDomainEvent @event)
    {
        Description = @event.NewDescription;
    }

    private void Apply(ProductAddedToStoreDomainEvent @event)
    {
        _products.Add(@event.Product);
    }

    private void Apply(ProductRemovedFromStoreDomainEvent @event)
    {
        _products.Remove(@event.Product);
    }

    private static void Apply(ProductQuantityIncreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newQuantity = @event.NewQuantity;

        storeProduct.UpdateQuantity(newQuantity);
    }

    private static void Apply(ProductQuantityDecreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newQuantity = @event.NewQuantity;

        storeProduct.UpdateQuantity(newQuantity);
    }

    private static void Apply(ProductPriceIncreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newPrice = @event.NewPrice;

        storeProduct.UpdatePrice(newPrice);
    }

    private static void Apply(ProductPriceDecreasedDomainEvent @event)
    {
        var storeProduct = @event.Product;
        var newPrice = @event.NewPrice;

        storeProduct.UpdatePrice(newPrice);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Store() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
