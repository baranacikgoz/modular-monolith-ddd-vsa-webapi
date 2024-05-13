using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Entities;

public interface IAuditableEntity
{
    DateTime CreatedOn { get; set; }
    ApplicationUserId CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    ApplicationUserId? LastModifiedBy { get; set; }
    string LastModifiedIp { get; set; }
}

