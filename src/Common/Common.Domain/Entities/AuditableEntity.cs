using System.ComponentModel.DataAnnotations;
using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Entities;

/// <summary>
/// Non-generic version is intended to be used with the entities may have multiple keys, such as many-to-many join tables.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset CreatedOn { get; set; }
    public ApplicationUserId? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }

    [Timestamp]
    public uint Version { get; set; }
}

/// <summary>
/// Generic version is intended to be used with the entities have single key.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AuditableEntity<TId>(TId id) : AuditableEntity where TId : IStronglyTypedId
{
    public TId Id { get; protected set; } = id;
}

