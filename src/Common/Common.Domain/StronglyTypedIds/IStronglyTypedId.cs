namespace Common.Domain.StronglyTypedIds;
public interface IStronglyTypedId
{
    Guid Value { get; init; }
}
