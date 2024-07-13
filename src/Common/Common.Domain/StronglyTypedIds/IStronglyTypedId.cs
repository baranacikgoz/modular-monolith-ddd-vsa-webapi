namespace Common.Domain.StronglyTypedIds;
public interface IStronglyTypedId
{
    DefaultIdType Value { get; init; }
}
