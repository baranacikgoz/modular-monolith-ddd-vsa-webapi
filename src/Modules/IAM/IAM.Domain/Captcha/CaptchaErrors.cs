using Common.Domain.ResultMonad;

namespace IAM.Domain.Captcha;

public static class CaptchaErrors
{
    public static readonly Error CaptchaServiceUnavailable = new() { Key = nameof(CaptchaServiceUnavailable) };
    public static readonly Error NotHuman = new() { Key = nameof(NotHuman) };
}
