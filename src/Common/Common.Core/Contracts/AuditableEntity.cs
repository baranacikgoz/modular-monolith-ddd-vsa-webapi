namespace Common.Core.Contracts;

public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
    public DateTime CreatedOn { get; protected set; }
    public Guid CreatedBy { get; protected set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; protected set; }
    public Guid? LastModifiedBy { get; protected set; }
    public string LastModifiedIp { get; protected set; } = string.Empty;

    protected AuditableEntity(TId id)
        : base(id)
    {
        LastModifiedOn = DateTime.UtcNow;
    }

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

public interface IAuditableEntity
{
    void ApplyCreatedAudit(Guid userId, string ipAddress, DateTime createdOn);
    void ApplyUpdatedAudit(Guid userId, string ipAddress, DateTime updatedOn);
}
