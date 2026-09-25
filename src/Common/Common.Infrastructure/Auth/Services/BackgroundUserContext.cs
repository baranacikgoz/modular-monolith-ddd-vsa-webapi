using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;

namespace Common.Infrastructure.Auth.Services;

internal sealed class BackgroundUserContext : IBackgroundUserContext
{
    public ApplicationUserId? UserId { get; private set; }

    public void Set(ApplicationUserId? userId)
    {
        UserId = userId;
    }
}
