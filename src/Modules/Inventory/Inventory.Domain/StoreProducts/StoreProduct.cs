using System.Text.Json.Serialization;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Products;
using Inventory.Domain.Stores;

namespace Inventory.Domain.StoreProducts;

public readonly record struct StoreProductId(Guid Value) : IStronglyTypedId
{
    public static StoreProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out StoreProductId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

public class StoreProduct : AuditableEntity<StoreProductId>
{
    private StoreProduct(StoreId storeId, ProductId productId, int quantity, decimal price)
        : base(StoreProductId.New())
    {
        StoreId = storeId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public StoreId StoreId { get; }

    [JsonIgnore]
    public virtual Store? Store { get; }
    public ProductId ProductId { get; }

    [JsonIgnore]
    public virtual Product? Product { get; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public static StoreProduct Create(StoreId storeId, ProductId productId, int quantity, decimal price)
        => new(storeId, productId, quantity, price);

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void UpdatePrice(decimal price)
    {
        Price = price;
    }

    public StoreProduct() : base(new(Guid.Empty)) { } // ORMs need a parameterless ctor
}
