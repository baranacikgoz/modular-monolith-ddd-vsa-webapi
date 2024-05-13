using Common.Application.Caching;
using Common.Domain.ResultMonad;
using IdentityAndAuth.Application.Captcha.Services;

namespace IdentityAndAuth.Infrastructure.Captcha.Services;
public class CachedCaptchaService(
    ICaptchaService decoree,
    ICacheService cacheService,
    int cacheCaptchaForMinutes
    ) : ICaptchaService
{
    public Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        return cacheService.GetOrSetAsync(
            CacheKey(captchaToken),
            () => decoree.ValidateAsync(captchaToken, cancellationToken),
            TimeSpan.FromMinutes(cacheCaptchaForMinutes), cancellationToken: cancellationToken);
    }

    public string GetClientKey() => decoree.GetClientKey();
    private static string CacheKey(string captchaToken) => $"captcha:{captchaToken}";
}
