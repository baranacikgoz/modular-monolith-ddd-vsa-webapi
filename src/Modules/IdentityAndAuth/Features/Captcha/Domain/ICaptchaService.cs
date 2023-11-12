using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Captcha.Domain;

public interface ICaptchaService
{
    Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken);
}
