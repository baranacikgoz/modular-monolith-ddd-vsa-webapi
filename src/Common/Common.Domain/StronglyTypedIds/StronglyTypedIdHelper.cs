namespace Common.Domain.StronglyTypedIds;

public static class StronglyTypedIdHelper
{
    public static bool TryDeserialize<TId>(string str, out TId? id) where TId : IStronglyTypedId, new()
    {
        if (!DefaultIdType.TryParse(str, out var guid))
        {
            id = default;
            return false;
        }

        id = new TId { Value = guid };
        return true;
    }
}
