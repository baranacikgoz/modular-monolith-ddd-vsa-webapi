using Common.Application.Caching;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using IdentityAndAuth.Application.Captcha.Services;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Infrastructure.Captcha.Services;
public class CachedCaptchaService(
    ICaptchaService decoree,
    ICacheService cacheService,
    IOptions<OtpOptions> otpOptionsProvider
    ) : ICaptchaService
{
    private readonly int _cacheCaptchaForMinutes = otpOptionsProvider.Value.ExpirationInMinutes;
    public Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        return cacheService.GetOrSetAsync(
            CacheKey(captchaToken),
            () => decoree.ValidateAsync(captchaToken, cancellationToken),
            TimeSpan.FromMinutes(_cacheCaptchaForMinutes), cancellationToken: cancellationToken);
    }

    public string GetClientKey() => decoree.GetClientKey();
    private static string CacheKey(string captchaToken) => $"captcha:{captchaToken}";
}
