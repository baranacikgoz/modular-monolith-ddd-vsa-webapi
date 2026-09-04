using Common.Application.Auth;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     This query is for seeding other modules requiring some basic seed users, where a userId is required.
///     Users live in Keycloak, so this reads the realm's <c>basic</c> role members.
/// </summary>
public class GetSeedUserIdsRequestHandler(IKeycloakAdminClient adminClient)
    : InterModuleRequestHandler<GetSeedUserIdsRequest, GetSeedUserIdsResponse>
{
    public override async Task<GetSeedUserIdsResponse> HandleAsync(
        GetSeedUserIdsRequest request, CancellationToken cancellationToken)
    {
        var userIds = await adminClient.GetUserIdsInRoleAsync(KeycloakRoles.Basic, request.Count, cancellationToken);

        return new GetSeedUserIdsResponse(userIds.ToList());
    }
}
