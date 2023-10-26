namespace Common.Core.Contracts;

public abstract class AuditableEntity<TId> : BaseEntity<TId>
{
    public DateTime CreatedOn { get; }
    public Guid CreatedBy { get; protected set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; protected set; }
    public Guid? LastModifiedBy { get; protected set; }

    protected AuditableEntity(TId id)
        : base(id)
    {
        CreatedOn = DateTime.UtcNow;
        LastModifiedOn = DateTime.UtcNow;
    }
}
