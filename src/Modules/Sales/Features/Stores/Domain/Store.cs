using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.DomainEvents;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain.Errors;

namespace Sales.Features.Stores.Domain;

internal readonly record struct StoreId(Guid Value)
{
    public static StoreId New() => new(Guid.NewGuid());
}

internal class Store : AggregateRoot<StoreId>
{
    private Store(Guid ownerId)
        : base(StoreId.New())
    {
        OwnerId = ownerId;
    }

    public Guid OwnerId { get; private set; }
    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(Guid ownerId)
    {
        var store = new Store(ownerId);

        store.AddDomainEvent(new Events
                                .Published
                                .From
                                .Sales
                                .StoreCreated(store.Id.Value, store.OwnerId));

        return store;
    }

    public Result AddProduct(Product product)
    {
        _products.Add(product);

        AddDomainEvent(new Events
                          .Published
                          .From
                          .Sales
                          .ProductAdded(Id.Value, product.Id.Value));

        return Result.Success;
    }

    public Result AddProducts(IEnumerable<Product> products)
    {
        _products.AddRange(products);

        foreach (var product in products)
        {
            AddDomainEvent(new Events
                              .Published
                              .From
                              .Sales
                              .ProductAdded(Id.Value, product.Id.Value));
        }

        return Result.Success;
    }

    public Result RemoveProduct(ProductId productId)
        => Result<Product>
            .Create(
                funcToGetValue: () => _products.SingleOrDefault(p => p.Id == productId),
                errorIfValueNull: StoreErrors.ProductNotFound)
            .Bind(product => _products.Remove(product));
}
