using System.ComponentModel.DataAnnotations;

namespace Common.Core.Contracts;

public abstract class BaseEntity<TId>
{
    protected BaseEntity(TId id)
    {
        Id = id;
    }
    public TId Id { get; }

#pragma warning disable CA1819
    [Timestamp]
    public byte[] RowVersion { get; set; } = default!;
#pragma warning restore CA1819

}
