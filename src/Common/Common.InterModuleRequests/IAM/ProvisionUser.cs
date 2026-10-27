using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

public enum ProvisionUserFailureReason { None, PhoneNumberAlreadyRegistered, EmailAlreadyRegistered, Rejected, IdentityProviderUnavailable }

/// <summary>
///     Creates an identity-provider user with no business role. The caller (a registration flow) assigns one later, once the
///     registration is approved. The email was verified by the caller (a verification token from the email OTP flow); the phone
///     number was not, so the user is created with an unverified phone. <paramref name="EncryptedPassword" /> is the password
///     encrypted with <c>SecretProtector.Encrypt</c>: a message on the bus is visible in broker queues, error queues and logs, so
///     the password never travels in clear. Both modules run in one process and share the key.
/// </summary>
#pragma warning disable CA1819 // The ciphertext crosses the bus as one self-contained byte array (base64 in JSON); a wrapper buys nothing.
public sealed record ProvisionUserRequest(
    string Email,
    string PhoneNumber,
    byte[] EncryptedPassword,
    string FirstName,
    string LastName
) : IInterModuleRequest<ProvisionUserResponse>;
#pragma warning restore CA1819

public sealed record ProvisionUserResponse(
    ApplicationUserId? UserId,
    ProvisionUserFailureReason FailureReason,
    string? RejectionMessage = null);
