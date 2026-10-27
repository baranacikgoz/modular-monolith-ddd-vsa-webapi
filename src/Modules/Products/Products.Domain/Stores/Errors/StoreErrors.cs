using System.Net;
using Common.Domain.ResultMonad;

namespace Products.Domain.Stores.Errors;

public static class StoreErrors
{
    public static readonly Error NotPendingApproval = new() { Key = nameof(NotPendingApproval), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error NotActive = new() { Key = nameof(NotActive), StatusCode = HttpStatusCode.Conflict };

    public static readonly Error NotSuspended = new() { Key = nameof(NotSuspended), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The identity provider already holds a user with this phone number. Shares its key and message with IAM's own error of the same name.</summary>
    public static readonly Error PhoneNumberAlreadyRegistered = new() { Key = nameof(PhoneNumberAlreadyRegistered), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The identity provider already holds a user with this email. Shares its key and message with IAM's own error of the same name.</summary>
    public static readonly Error EmailAlreadyRegistered = new() { Key = nameof(EmailAlreadyRegistered), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The identity provider refused the user (for example its password policy); <paramref name="message" /> is its reason, when it gave one.</summary>
    public static Error IdentityProviderRejectedUser(string? message)
    {
        return new Error
        {
            Key = nameof(IdentityProviderRejectedUser),
            StatusCode = HttpStatusCode.BadRequest,
            SubErrors = string.IsNullOrEmpty(message) ? null : [message]
        };
    }

    /// <summary>The identity provider is unreachable, timed out or its circuit is open. Retryable, unlike a rejected user.</summary>
    public static readonly Error IdentityProviderUnavailable = new() { Key = nameof(IdentityProviderUnavailable), StatusCode = HttpStatusCode.BadGateway };
}
