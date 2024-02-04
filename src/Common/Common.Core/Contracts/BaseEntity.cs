using System.ComponentModel.DataAnnotations;

namespace Common.Core.Contracts;

public abstract class BaseEntity<TId>
{
    protected BaseEntity(TId id)
    {
        Id = id;
    }
    public TId Id { get; }

    [Timestamp]
    public uint Version { get; set; }
}
