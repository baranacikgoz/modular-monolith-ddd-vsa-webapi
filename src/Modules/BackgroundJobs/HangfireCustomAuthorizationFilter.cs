using Common.Application.Auth;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundJobs;
public class HangfireCustomAuthorizationFilter : IDashboardAsyncAuthorizationFilter
{
    public Task<bool> AuthorizeAsync([NotNull] DashboardContext context)
    {
        var currentUser = context.GetHttpContext().RequestServices.GetRequiredService<ICurrentUser>();

        var permissionName = CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire);

        var hasPermission = currentUser.HasPermission(permissionName);

        return Task.FromResult(hasPermission);
    }
}
