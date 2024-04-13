namespace IdentityAndAuth.Features.Identity.Domain;

public sealed partial class ApplicationUser
{
    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;
}
