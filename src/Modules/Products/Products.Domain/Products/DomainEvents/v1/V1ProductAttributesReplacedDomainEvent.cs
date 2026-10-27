using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductAttributesReplacedDomainEvent(
    ProductId ProductId,
    IReadOnlyList<V1ProductAttributesReplacedDomainEvent.AttributeSnapshot> Attributes
) : DomainEvent
{
    public sealed record AttributeSnapshot(string Name, string Value);
}

internal static class V1ProductAttributesReplacedDomainEventExtensions
{
    public static V1ProductAttributesReplacedDomainEvent.AttributeSnapshot ToSnapshot(this ProductAttribute attribute)
    {
        return new V1ProductAttributesReplacedDomainEvent.AttributeSnapshot(attribute.Name, attribute.Value);
    }
}
