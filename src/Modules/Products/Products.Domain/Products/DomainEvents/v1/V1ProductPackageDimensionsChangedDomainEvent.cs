using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductPackageDimensionsChangedDomainEvent(
    ProductId ProductId,
    V1ProductPackageDimensionsChangedDomainEvent.DimensionsSnapshot Dimensions
) : DomainEvent
{
    public sealed record DimensionsSnapshot(decimal LengthCm, decimal WidthCm, decimal HeightCm, decimal WeightKg);
}

internal static class V1ProductPackageDimensionsChangedDomainEventExtensions
{
    public static V1ProductPackageDimensionsChangedDomainEvent.DimensionsSnapshot ToSnapshot(this PackageDimensions dimensions)
    {
        return new V1ProductPackageDimensionsChangedDomainEvent.DimensionsSnapshot(
            dimensions.LengthCm, dimensions.WidthCm, dimensions.HeightCm, dimensions.WeightKg);
    }
}
