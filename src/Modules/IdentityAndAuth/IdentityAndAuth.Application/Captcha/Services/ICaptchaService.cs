using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Application.Captcha.Services;

public interface ICaptchaService
{
    string GetClientKey();
    Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken);
}
