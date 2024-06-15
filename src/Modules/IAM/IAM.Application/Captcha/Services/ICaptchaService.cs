using Common.Domain.ResultMonad;

namespace IAM.Application.Captcha.Services;

public interface ICaptchaService
{
    string GetClientKey();
    Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken);
}
