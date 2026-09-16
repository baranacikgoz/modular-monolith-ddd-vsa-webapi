using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

/// <remarks>
/// A property converted through this class translates a whole-value query compare fine:
/// <c>.Where(m => m.Id == typedId)</c>. A member-access-then-compare on the same property does
/// not translate and throws "could not be translated" only when the query runs:
/// <c>.Where(m => m.Id.Value == rawGuid)</c>. Wrap the raw value first instead:
/// <c>new TStronglyTypedId(rawGuid)</c>. Enforced by
/// Common.Tests/Architecture/StronglyTypedIdQueryTests.cs.
/// </remarks>
public class StronglyTypedIdValueConverter<TStronglyTypedId> : ValueConverter<TStronglyTypedId, DefaultIdType>
    where TStronglyTypedId : IStronglyTypedId, new()
{
    public StronglyTypedIdValueConverter()
        : base(
            id => id.Value,
            value => new TStronglyTypedId { Value = value })
    {
    }
}
