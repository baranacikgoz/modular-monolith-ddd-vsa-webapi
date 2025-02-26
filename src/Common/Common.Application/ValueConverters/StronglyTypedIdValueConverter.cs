using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Application.ValueConverters;
public class StronglyTypedIdValueConverter<TStronglyTypedId> : ValueConverter<TStronglyTypedId, DefaultIdType>
    where TStronglyTypedId : IStronglyTypedId, new()
{
    public StronglyTypedIdValueConverter()
        : base(
            id => id.Value,
            value => new TStronglyTypedId() { Value = value })
    {

    }
}
