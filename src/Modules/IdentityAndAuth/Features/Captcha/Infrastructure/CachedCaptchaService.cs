using Common.Caching;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Captcha.Domain;

namespace IdentityAndAuth.Features.Captcha.Infrastructure;
internal class CachedCaptchaService(
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
    private static string CacheKey(string captchaToken) => $"captcha:{captchaToken}";
}
