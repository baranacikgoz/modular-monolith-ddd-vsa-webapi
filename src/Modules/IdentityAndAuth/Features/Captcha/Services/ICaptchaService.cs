using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha.Services;

public interface ICaptchaService
{
    Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken);
}
