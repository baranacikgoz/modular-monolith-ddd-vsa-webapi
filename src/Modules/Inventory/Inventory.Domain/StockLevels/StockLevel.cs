using Common.Domain.Aggregates;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StockLevels.DomainEvents.v1;

namespace Inventory.Domain.StockLevels;

public readonly record struct StockLevelId(DefaultIdType Value) : IStronglyTypedId
{
    public static StockLevelId New()
    {
        return new StockLevelId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StockLevelId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     Read-model created by consuming Products' <c>ProductCreatedIntegrationEvent</c> (see
///     Inventory.Application/IntegrationEventHandlers). Demo simplification: <see cref="QuantityOnHand"/> is a
///     one-time snapshot taken at product-creation time, never kept in sync with later Product.Quantity
///     changes - those aren't published as IntegrationEvents in this template. A real Inventory module would
///     need that sync wired too (e.g. a ProductQuantityChangedIntegrationEvent consumer).
/// </summary>
public class StockLevel : AggregateRoot<StockLevelId>
{
#pragma warning disable CS8618
    private StockLevel() : base(new StockLevelId(DefaultIdType.Empty)) { }
#pragma warning restore CS8618

    public DefaultIdType ProductId { get; private set; }
    public int QuantityOnHand { get; private set; }

    public static StockLevel Create(DefaultIdType productId, int quantityOnHand)
    {
        var id = StockLevelId.New();

        var @event = new V1StockLevelCreatedDomainEvent(id, productId, quantityOnHand);

        var stockLevel = new StockLevel
        {
            Id = id,
            ProductId = productId,
            QuantityOnHand = quantityOnHand
        };

        stockLevel.RaiseEvent(@event);
        return stockLevel;
    }
}
