using System.ComponentModel.DataAnnotations;
using Common.Core.Contracts.Identity;

namespace Common.Core.Contracts;
public interface IAuditableEntity
{
    DateTime CreatedOn { get; set; }
    ApplicationUserId CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    ApplicationUserId? LastModifiedBy { get; set; }
    string LastModifiedIp { get; set; }
}

/// <summary>
/// Non-generic version is intended to be used with the entities may have multiple keys, such as many-to-many join tables.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public ApplicationUserId CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }
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

