using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Events;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain.Errors;

namespace Sales.Features.Stores.Domain;

internal readonly record struct StoreId(Guid Value)
{
    public static StoreId New() => new(Guid.NewGuid());
}

internal class Store : AggregateRoot<StoreId>
{
    private Store(Guid ownerId, string name)
        : base(StoreId.New())
    {
        OwnerId = ownerId;
        Name = name;
    }

    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public static Store Create(Guid ownerId, string name)
    {
        var store = new Store(ownerId, name);

        store.Name = name;

        store.AddDomainEvent(new EventsOf
                                .Sales
                                .StoreCreatedDomainEvent(store.Id.Value, store.OwnerId));

        return store;
    }

    public Result AddProduct(Product product)
    {
        _products.Add(product);

        AddDomainEvent(new EventsOf
                          .Sales
                          .ProductAddedDomainEvent(Id.Value, product.Id.Value));

        return Result.Success;
    }

    public Result AddProducts(IEnumerable<Product> products)
    {
        _products.AddRange(products);

        foreach (var product in products)
        {
            AddDomainEvent(new EventsOf
                              .Sales
                              .ProductAddedDomainEvent(Id.Value, product.Id.Value));
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
