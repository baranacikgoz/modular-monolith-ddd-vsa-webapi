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

    private readonly List<StoreProduct> _products = [];
    public virtual IReadOnlyCollection<StoreProduct> Products => _products.AsReadOnly();

    public static Store Create(ApplicationUserId ownerId, string name)
    {
        var id = StoreId.New();
        var store = new Store();

        var @event = new StoreCreatedDomainEvent(id, ownerId, name);
        store.RaiseEvent(@event);

        return store;
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

    public Store() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
}
