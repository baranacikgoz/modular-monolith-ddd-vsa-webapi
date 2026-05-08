using Common.Application.Options;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Captcha.Services;

public class CachedCaptchaService(
    ICaptchaService decoree,
    IFusionCache cache,
    IOptions<OtpOptions> otpOptionsProvider
) : ICaptchaService
{
    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        return await cache.GetOrSetAsync<Result>(
            CacheKey(captchaToken),
            async (_, ct) => await decoree.ValidateAsync(captchaToken, ct),
            options: new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(otpOptionsProvider.Value.ExpirationInMinutes) },
            token: cancellationToken);
    }

    public string GetClientKey()
    {
        return decoree.GetClientKey();
    }

    private static string CacheKey(string captchaToken)
    {
        return $"captcha:{captchaToken}";
    }
}
