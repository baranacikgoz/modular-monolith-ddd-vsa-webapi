using System.Net;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

public enum EmailOtpDispatchOutcome
{
    Sent,
    Throttled,
    ProviderUnavailable
}

/// <summary><paramref name="Email" /> must already be trimmed and lowercased by the caller.</summary>
public sealed record SendEmailOtpRequest(
    string Email,
    string Purpose,
    string? Language = null,
    string? ContextId = null
) : IInterModuleRequest<SendEmailOtpResponse>;

public sealed record SendEmailOtpResponse(EmailOtpDispatchOutcome Outcome);

/// <summary>Shared HTTP-facing mapping for <see cref="EmailOtpDispatchOutcome"/>: callers of
/// <see cref="SendEmailOtpRequest"/> map the response through this instead of re-deriving status codes.</summary>
public static class EmailOtpDispatchErrors
{
    // Key deliberately matches nameof(HttpStatusCode.TooManyRequests), same as the rate limiter's own
    // 429 (RateLimitingMiddleware.WriteTooManyRequestsToResponse): every 429 in the API carries the
    // same errorKey/title regardless of which layer (IP rate limiter vs this email quota) rejected it.
    public static readonly Error Throttled =
        new() { Key = nameof(HttpStatusCode.TooManyRequests), StatusCode = HttpStatusCode.TooManyRequests };

    public static readonly Error ProviderUnavailable =
        new() { Key = nameof(ProviderUnavailable), StatusCode = HttpStatusCode.ServiceUnavailable };

    public static Result ToResult(this EmailOtpDispatchOutcome outcome) => outcome switch
    {
        EmailOtpDispatchOutcome.Sent => Result.Success,
        EmailOtpDispatchOutcome.Throttled => Throttled,
        EmailOtpDispatchOutcome.ProviderUnavailable => ProviderUnavailable,
        _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown EmailOtpDispatchOutcome.")
    };
}
