using Common.Application.Auth;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundJobs;
public class HangfireCustomAuthorizationFilter : IDashboardAsyncAuthorizationFilter
{
    public async Task<bool> AuthorizeAsync([NotNull] DashboardContext context)
    {
        var currentUserId = context.GetHttpContext().RequestServices.GetRequiredService<ICurrentUser>().Id;
        if (currentUserId.IsEmpty)
        {
            return false;
        }

        var requestClient = context.GetHttpContext().RequestServices.GetRequiredService<IInterModuleRequestClient<HasManageHangfireDashboardPermissonRequest, HasManageHangfireDashboardPermissonResponse>>();

        var hasPermissionRequest = new HasManageHangfireDashboardPermissonRequest(currentUserId);
        var response = await requestClient.SendAsync(hasPermissionRequest, default);

        return response.HasPermission;
    }
}
