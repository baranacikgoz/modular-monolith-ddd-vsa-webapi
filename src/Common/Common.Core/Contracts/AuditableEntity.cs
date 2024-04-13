using System.ComponentModel.DataAnnotations;

namespace Common.Core.Contracts;
public interface IAuditableEntity
{
    DateTime CreatedOn { get; set; }
    Guid CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    Guid? LastModifiedBy { get; set; }
    string LastModifiedIp { get; set; }
}

/// <summary>
/// Non-generic version is intended to be used with the entities may have multiple keys, such as many-to-many join tables.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;

    [Timestamp]
    public uint Version { get; set; }
}

/// <summary>
/// Generic version is intended to be used with the entities have single key.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AuditableEntity<TId>(TId id) : AuditableEntity where TId : IStronglyTypedId
{
    public TId Id { get; } = id;
}

