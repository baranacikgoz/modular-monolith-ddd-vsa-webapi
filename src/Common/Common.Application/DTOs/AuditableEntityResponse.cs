using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.DTOs;

public abstract record AuditableEntityResponse<TId> where TId : IStronglyTypedId
{
    public required TId Id { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
    public required ApplicationUserId CreatedBy { get; init; }
    public required DateTimeOffset? LastModifiedOn { get; init; }
    public required ApplicationUserId? LastModifiedBy { get; init; }
}
