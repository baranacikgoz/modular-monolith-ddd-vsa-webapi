using Common.Application.Auth;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundJobs;

public class HangfireCustomAuthorizationFilter : IDashboardAsyncAuthorizationFilter
{
    private static readonly string PolicyName =
        KeycloakPermission.FromScope(KeycloakScopes.Hangfire.Manage).PolicyName();

    public async Task<bool> AuthorizeAsync([NotNull] DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();

        var result = await authorizationService.AuthorizeAsync(httpContext.User, PolicyName);

        return result.Succeeded;
    }
}
