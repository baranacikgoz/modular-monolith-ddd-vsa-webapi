using Ardalis.Specification;
using IAM.Domain.Identity;

namespace IAM.Application.Users.Specifications;

public sealed class UserByRefreshTokenHashSpec : SingleResultSpecification<ApplicationUser>
{
    public UserByRefreshTokenHashSpec(byte[] refreshTokenHash)
        => Query
            .Where(x => x.RefreshTokenHash == refreshTokenHash);
}
