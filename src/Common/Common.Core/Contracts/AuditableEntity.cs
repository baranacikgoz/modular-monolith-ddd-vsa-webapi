using System.ComponentModel.DataAnnotations;

namespace Common.Core.Contracts;
public interface IAuditableEntity
{
    DateTime CreatedOn { get; }
    Guid CreatedBy { get; }
    DateTime? LastModifiedOn { get; }
    Guid? LastModifiedBy { get; }
    string LastModifiedIp { get; }

    void ApplyCreatedAudit(Guid userId, string ipAddress, DateTime createdOn);
    void ApplyUpdatedAudit(Guid userId, string ipAddress, DateTime updatedOn);
}

/// <summary>
/// Non-generic version is intended to be used with the entities may have multiple keys, such as many-to-many join tables.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedOn { get; private set; }
    public Guid CreatedBy { get; private set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    public string LastModifiedIp { get; private set; } = string.Empty;

    [ConcurrencyCheck]
    public virtual uint Version { get; set; }

    public void ApplyCreatedAudit(Guid userId, string ipAddress, DateTime createdOn)
    {
        CreatedBy = userId;
        CreatedOn = createdOn;
        ApplyUpdatedAudit(userId, ipAddress, createdOn);
    }
    public void ApplyUpdatedAudit(Guid userId, string ipAddress, DateTime updatedOn)
    {
        LastModifiedBy = userId;
        LastModifiedOn = updatedOn;
        LastModifiedIp = ipAddress;
    }
}

/// <summary>
/// Generic version is intended to be used with the entities have single key.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AuditableEntity<TId>(TId id) : AuditableEntity where TId : IStronglyTypedId
{
    public TId Id { get; } = id;
}

