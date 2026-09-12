using System.Net;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

public enum SmsOtpDispatchOutcome
{
    Sent,
    Throttled,
    ProviderUnavailable
}

public sealed record SendPhoneOtpRequest(
    string PhoneNumber,
    string Purpose,
    string? Language = null,
    string? ContextId = null
) : IInterModuleRequest<SendPhoneOtpResponse>;

public sealed record SendPhoneOtpResponse(SmsOtpDispatchOutcome Outcome);

/// <summary>Shared HTTP-facing mapping for <see cref="SmsOtpDispatchOutcome"/>: callers of
/// <see cref="SendPhoneOtpRequest"/> map the response through this instead of re-deriving status codes.</summary>
public static class OtpDispatchErrors
{
    // Key deliberately matches nameof(HttpStatusCode.TooManyRequests), same as the rate limiter's own
    // 429 (RateLimitingMiddleware.WriteTooManyRequestsToResponse): every 429 in the API carries the
    // same errorKey/title regardless of which layer (IP rate limiter vs this phone quota) rejected it.
    public static readonly Error Throttled =
        new() { Key = nameof(HttpStatusCode.TooManyRequests), StatusCode = HttpStatusCode.TooManyRequests };

    public static readonly Error ProviderUnavailable =
        new() { Key = nameof(ProviderUnavailable), StatusCode = HttpStatusCode.ServiceUnavailable };

    public static Result ToResult(this SmsOtpDispatchOutcome outcome) => outcome switch
    {
        SmsOtpDispatchOutcome.Sent => Result.Success,
        SmsOtpDispatchOutcome.Throttled => Throttled,
        SmsOtpDispatchOutcome.ProviderUnavailable => ProviderUnavailable,
        _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown SmsOtpDispatchOutcome.")
    };
}
