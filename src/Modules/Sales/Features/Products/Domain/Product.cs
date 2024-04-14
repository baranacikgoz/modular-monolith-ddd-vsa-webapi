using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Contracts.Money;
using Sales.Features.Products.Domain.DomainEvents;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain;

public readonly record struct ProductId(Guid Value) : IStronglyTypedId
{
    public static ProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class Product : AggregateRoot<ProductId>
{
    public StoreId StoreId { get; private set; }
    public virtual Store? Store { get; private set; }
    public Price Price { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Product Create(StoreId storeId, Price price, string name, string description)
    {
        var id = ProductId.New();
        var product = new Product();

        var @event = new ProductCreatedDomainEvent(id, storeId, price, name, description);
        product.RaiseEvent(@event);

        return product;
    }

    public Result UpdatePrice(Price price)
        => Price.EnsureSameCurrency(price)
            .Tap(() => Price.EnsureAmountsAreNotTheSame(price))
            .Tap(() => RaiseEvent(price.CompareTo(Price) switch
            {
                -1 => new ProductPriceDecreasedEvent(Price, price),
                1 => new ProductPriceIncreasedEvent(Price, price),
                _ => throw new InvalidOperationException("Unhandled CompareTo() value.")
            }));

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case ProductCreatedDomainEvent e:
                Apply(e);
                break;
            case ProductPriceDecreasedEvent e:
                Apply(e);
                break;
            case ProductPriceIncreasedEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(ProductCreatedDomainEvent @event)
    {
        Id = @event.Id;
        StoreId = @event.StoreId;
        Price = @event.Price;
        Name = @event.Name;
        Description = @event.Description;
    }

    private void Apply(ProductPriceDecreasedEvent @event)
    {
        Price = @event.NewPrice;
    }

    private void Apply(ProductPriceIncreasedEvent @event)
    {
        Price = @event.NewPrice;
    }

#pragma warning disable CS8618
    private Product() : base(new(Guid.Empty)) { } // ORMs need parameterlers ctor
#pragma warning disable CS8618
}
