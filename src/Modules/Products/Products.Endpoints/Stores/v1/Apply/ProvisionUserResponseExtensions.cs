using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using Products.Domain.Stores.Errors;

namespace Products.Endpoints.Stores.v1.Apply;

internal static class ProvisionUserResponseExtensions
{
    /// <summary>One distinct error per reason, so a client can tell a duplicate (change the input) from a refusal (fix the password) from an outage (retry later).</summary>
    internal static Result<ApplicationUserId> ToResult(this ProvisionUserResponse response)
    {
        return response.FailureReason switch
        {
            ProvisionUserFailureReason.None => response.UserId!.Value,
            ProvisionUserFailureReason.PhoneNumberAlreadyRegistered => StoreErrors.PhoneNumberAlreadyRegistered,
            ProvisionUserFailureReason.EmailAlreadyRegistered => StoreErrors.EmailAlreadyRegistered,
            ProvisionUserFailureReason.Rejected => StoreErrors.IdentityProviderRejectedUser(response.RejectionMessage),
            ProvisionUserFailureReason.IdentityProviderUnavailable => StoreErrors.IdentityProviderUnavailable,
            _ => throw new ArgumentOutOfRangeException(
                nameof(response), response.FailureReason, $"Unknown ProvisionUserFailureReason: {response.FailureReason}")
        };
    }
}
