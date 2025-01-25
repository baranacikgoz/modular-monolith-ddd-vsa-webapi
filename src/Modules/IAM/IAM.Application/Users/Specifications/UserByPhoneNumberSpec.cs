using System.Linq.Expressions;
using Ardalis.Specification;
using IAM.Domain.Identity;

namespace IAM.Application.Users.Specifications;

public sealed class UserByPhoneNumberSpec : SingleResultSpecification<ApplicationUser>
{
    public UserByPhoneNumberSpec(string phoneNumber)
        => Query
            .Where(x => x.PhoneNumber == phoneNumber);
}

public sealed class UserByPhoneNumberSpec<TDto> : SingleResultSpecification<ApplicationUser, TDto>
{
    public UserByPhoneNumberSpec(string phoneNumber, Expression<Func<ApplicationUser, TDto>> selector)
        => Query
            .Select(selector)
            .Where(x => x.PhoneNumber == phoneNumber);
}
