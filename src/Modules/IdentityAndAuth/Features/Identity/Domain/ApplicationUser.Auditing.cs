namespace IdentityAndAuth.Features.Identity.Domain;

public sealed partial class ApplicationUser
{
    public DateTime CreatedOn { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? LastModifiedOn { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    public string LastModifiedIp { get; private set; } = string.Empty;

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
