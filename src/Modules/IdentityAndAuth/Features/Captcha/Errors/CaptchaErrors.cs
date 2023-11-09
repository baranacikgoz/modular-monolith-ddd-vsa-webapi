using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha.Errors;

internal static class CaptchaErrors
{
    public static readonly Error ServiceUnavailable = new(nameof(ServiceUnavailable));
    public static readonly Error NotHuman = new(nameof(NotHuman));
}
