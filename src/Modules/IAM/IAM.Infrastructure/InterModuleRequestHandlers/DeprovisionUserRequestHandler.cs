using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>Compensation for a registration whose local write failed after the Keycloak user was created. A user Keycloak no longer has is not an error.</summary>
public class DeprovisionUserRequestHandler(IKeycloakAdminClient adminClient)
    : InterModuleRequestHandler<DeprovisionUserRequest, DeprovisionUserResponse>
{
    public override async Task<DeprovisionUserResponse> HandleAsync(
        DeprovisionUserRequest request, CancellationToken cancellationToken)
    {
        await adminClient.DeleteUserAsync(request.UserId, cancellationToken);

        return new DeprovisionUserResponse();
    }
}
