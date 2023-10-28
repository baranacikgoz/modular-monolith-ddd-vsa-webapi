using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha;

public static class CaptchaErrors
{
    public static readonly Error VerificationFailed = new(nameof(VerificationFailed));
}
