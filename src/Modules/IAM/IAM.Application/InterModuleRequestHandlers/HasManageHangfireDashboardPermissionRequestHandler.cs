using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Auth;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Identity.Services;

namespace IAM.Application.InterModuleRequestHandlers;
public class HasManageHangfireDashboardPermissionRequestHandler(
    IUserService userService
    ) : InterModuleRequestHandler<HasManageHangfireDashboardPermissonRequest, HasManageHangfireDashboardPermissonResponse>
{
    protected override async Task<HasManageHangfireDashboardPermissonResponse> HandleAsync(
        HasManageHangfireDashboardPermissonRequest request,
        CancellationToken cancellationToken)
    {
        var hasPermission = await userService.HasPermissionAsync(
                                request.UserId,
                                CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire),
                                cancellationToken);

        return new(hasPermission);
    }
}
