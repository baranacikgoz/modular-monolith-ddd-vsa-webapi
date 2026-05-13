using Common.Application.Caching;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Domain.Errors;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Identity.Services;

internal abstract class OtpServiceBase(IFusionCache cache) : IOtpService
{
    private const int MaxFailedAttempts = 3;

    public async Task StoreOtpAsync(string phoneNumber, string otp, TimeSpan duration, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber);
        var entry = new OtpCacheEntry(otp, 0);
        await cache.SetAsync(cacheKey, entry, options: new FusionCacheEntryOptions { Duration = duration }, token: cancellationToken);
    }

    public async Task<Result> VerifyThenRemoveOtpAsync(
        string phoneNumber,
        string otp,
        CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber);

        var entry = await cache.GetOrDefaultAsync<OtpCacheEntry>(cacheKey, token: cancellationToken);

        if (entry is null)
        {
            return OtpErrors.InvalidOtp;
        }

        if (!string.Equals(entry.Otp, otp, StringComparison.Ordinal))
        {
            var updatedFailedAttempts = entry.FailedAttempts + 1;

            if (updatedFailedAttempts >= MaxFailedAttempts)
            {
                await cache.RemoveAsync(cacheKey, token: cancellationToken);
                return OtpErrors.TooManyFailedAttempts;
            }

            await cache.SetAsync(
                cacheKey,
                entry with { FailedAttempts = updatedFailedAttempts },
                token: cancellationToken);

            return OtpErrors.InvalidOtp;
        }

        await cache.RemoveAsync(cacheKey, token: cancellationToken);

        return Result.Success;
    }

    public abstract string Generate();
}
