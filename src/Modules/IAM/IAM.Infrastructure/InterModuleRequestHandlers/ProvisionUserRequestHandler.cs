using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Security;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Keycloak;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Creates a Keycloak user carrying only the platform baseline role, for another module's own approval-gated
///     registration flow. The caller assigns a business role later, once the registration is approved. The failure
///     reason mirrors the identity provider's outcome so the caller can tell a duplicate from an outage. The email is created
///     verified (the request contract says the caller proved ownership) and the phone number unverified. The password
///     arrives encrypted and is decrypted only here, to be handed to Keycloak; it is never logged.
/// </summary>
public class ProvisionUserRequestHandler(
    IKeycloakAdminClient adminClient,
    SecretProtector secretProtector,
    IOptions<IdentitySchemeOptions> identitySchemeOptions,
    ILogger<ProvisionUserRequestHandler> logger)
    : InterModuleRequestHandler<ProvisionUserRequest, ProvisionUserResponse>
{
    public override async Task<ProvisionUserResponse> HandleAsync(
        ProvisionUserRequest request, CancellationToken cancellationToken)
    {
        // The username source is a deployment-time consequence of the identity scheme, never per request.
        var username = identitySchemeOptions.Value.Scheme == IdentityScheme.Email
            ? request.Email
            : request.PhoneNumber;

        var result = await adminClient.CreateUserAsync(
                new CreateKeycloakUser(
                    Username: username,
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    PhoneNumber: request.PhoneNumber,
                    Email: request.Email,
                    Password: secretProtector.Decrypt(request.EncryptedPassword),
                    PhoneNumberVerified: false,
                    EmailVerified: true),
                cancellationToken)
            .BindAsync(userId => RegistrationCompletion.AssignBasicRoleOrRollbackAsync(userId, adminClient, logger, cancellationToken));

        return result.Match(
            userId => new ProvisionUserResponse(userId, ProvisionUserFailureReason.None),
            error => new ProvisionUserResponse(UserId: null, MapFailureReason(error), error.SubErrors?.FirstOrDefault()));
    }

    private static ProvisionUserFailureReason MapFailureReason(Error error)
    {
        return error.Key switch
        {
            nameof(IdentityErrors.PhoneNumberAlreadyRegistered) => ProvisionUserFailureReason.PhoneNumberAlreadyRegistered,
            nameof(IdentityErrors.EmailAlreadyRegistered) => ProvisionUserFailureReason.EmailAlreadyRegistered,
            nameof(IdentityErrors.IdentityProviderUnavailable) => ProvisionUserFailureReason.IdentityProviderUnavailable,
            _ => ProvisionUserFailureReason.Rejected
        };
    }
}
