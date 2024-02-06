using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha.Domain.Errors;

internal static class CaptchaErrors
{
    public static readonly Error CaptchaServiceUnavailable = new(nameof(CaptchaServiceUnavailable));
    public static readonly Error NotHuman = new(nameof(NotHuman));
}
