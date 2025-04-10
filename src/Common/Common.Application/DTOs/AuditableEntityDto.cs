using Common.Domain.StronglyTypedIds;

namespace Common.Application.DTOs;

public abstract record AuditableEntityDto<TId> where TId : IStronglyTypedId
{
    public required TId Id { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
    public required ApplicationUserId? CreatedBy { get; init; }
    public required DateTimeOffset? LastModifiedOn { get; init; }
    public required ApplicationUserId? LastModifiedBy { get; init; }
}
