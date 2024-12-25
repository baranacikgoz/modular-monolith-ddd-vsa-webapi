using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Entities;

public interface IAuditableEntity
{
    DateTimeOffset CreatedOn { get; }
    ApplicationUserId CreatedBy { get; set; }
    DateTimeOffset? LastModifiedOn { get; set; }
    ApplicationUserId? LastModifiedBy { get; set; }
    string LastModifiedIp { get; set; }
}

