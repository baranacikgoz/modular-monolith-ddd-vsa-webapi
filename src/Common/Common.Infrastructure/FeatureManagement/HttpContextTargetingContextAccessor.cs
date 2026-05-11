using Common.Application.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement.FeatureFilters;

namespace Common.Infrastructure.FeatureManagement;

internal sealed class HttpContextTargetingContextAccessor(IHttpContextAccessor httpContextAccessor)
    : ITargetingContextAccessor
{
    public ValueTask<TargetingContext> GetContextAsync()
    {
        var currentUser = httpContextAccessor.HttpContext?.RequestServices.GetService<ICurrentUser>();

        var context = new TargetingContext
        {
            UserId = currentUser?.IdAsString,
            Groups = currentUser?.Roles ?? []
        };

        return new ValueTask<TargetingContext>(context);
    }
}
