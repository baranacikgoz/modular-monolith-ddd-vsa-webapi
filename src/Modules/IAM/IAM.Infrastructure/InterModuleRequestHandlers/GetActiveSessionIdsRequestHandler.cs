using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

public class GetActiveSessionIdsRequestHandler(IKeycloakAdminClient adminClient)
    : InterModuleRequestHandler<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse>
{
    public override async Task<GetActiveSessionIdsResponse> HandleAsync(
        GetActiveSessionIdsRequest request, CancellationToken cancellationToken)
    {
        var users = new List<UserSessionIds>(request.UserIds.Count);

        foreach (var userId in request.UserIds)
        {
            var sessions = await adminClient.GetUserSessionsAsync(userId, cancellationToken);
            users.Add(new UserSessionIds(userId, sessions.Select(s => s.Id).ToList()));
        }

        return new GetActiveSessionIdsResponse(users);
    }
}
