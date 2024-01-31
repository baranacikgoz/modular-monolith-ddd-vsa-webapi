using Common.Core.Contracts;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain;

internal readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
}

internal class Product : AuditableEntity<ProductId>
{
    private Product(StoreId storeId, string name)
        : base(ProductId.New())
    {
        StoreId = storeId;
        Name = name;
    }

    public StoreId StoreId { get; private set; }
    public virtual Store? Store { get; private set; }
    public string Name { get; private set; }

    public static Product Create(StoreId storeId, string name)
        => new(storeId, name);
}
