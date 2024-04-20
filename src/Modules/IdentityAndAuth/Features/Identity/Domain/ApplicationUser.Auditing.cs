using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.Domain;

public sealed partial class ApplicationUser
{
    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public ApplicationUserId CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;
}
