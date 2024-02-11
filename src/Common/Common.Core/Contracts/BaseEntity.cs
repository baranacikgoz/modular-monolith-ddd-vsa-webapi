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

/// <summary>
/// For tables requiring multiple keys like many-to-many relationships
/// </summary>
public abstract class BaseEntity
{

    [Timestamp]
    public uint Version { get; set; }
}
