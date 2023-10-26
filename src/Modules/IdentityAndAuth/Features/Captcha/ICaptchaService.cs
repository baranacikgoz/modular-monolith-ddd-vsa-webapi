using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha;

public interface ICaptchaService
{
    Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken);
}
