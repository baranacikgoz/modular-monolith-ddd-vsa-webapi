using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity;

public sealed partial class ApplicationUser
{
    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public ApplicationUserId CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;
}
