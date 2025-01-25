using System.Linq.Expressions;
using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity;

namespace IAM.Application.Users.Specifications;

public sealed class UserByIdSpec : SingleResultSpecification<ApplicationUser>
{
    public UserByIdSpec(ApplicationUserId id)
        => Query
            .Where(p => p.Id == id);
}

public sealed class UserByIdSpec<TDto> : SingleResultSpecification<ApplicationUser, TDto>
{
    public UserByIdSpec(ApplicationUserId id, Expression<Func<ApplicationUser, TDto>> selector)
        => Query
            .Select(selector)
            .Where(p => p.Id == id);
}
