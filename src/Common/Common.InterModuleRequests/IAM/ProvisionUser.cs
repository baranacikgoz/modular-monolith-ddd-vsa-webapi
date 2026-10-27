using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

public enum ProvisionUserFailureReason { None, PhoneNumberAlreadyRegistered, EmailAlreadyRegistered, Rejected, IdentityProviderUnavailable }

/// <summary>Creates an identity-provider user with no business role. The caller (a registration flow) assigns one later, once the registration is approved.</summary>
public sealed record ProvisionUserRequest(
    string Email,
    string PhoneNumber,
    string Password,
    string FirstName,
    string LastName
) : IInterModuleRequest<ProvisionUserResponse>;

public sealed record ProvisionUserResponse(
    ApplicationUserId? UserId,
    ProvisionUserFailureReason FailureReason,
    string? RejectionMessage = null);
