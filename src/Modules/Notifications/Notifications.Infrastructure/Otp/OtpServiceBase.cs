using Common.Application.Caching;
using Notifications.Application.Otp;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Otp;

internal abstract class OtpServiceBase(IFusionCache cache) : IOtpService
{
    private const int MaxFailedAttempts = 3;

    public async Task StoreAsync(string phoneNumber, string otp, string purpose, TimeSpan duration, string? contextId,
        CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber, purpose, contextId);
        var entry = new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow + duration);
        await cache.SetAsync(cacheKey, entry, new FusionCacheEntryOptions { Duration = duration },
            token: cancellationToken);
    }

    public async Task<OtpVerificationOutcome> VerifyThenRemoveAsync(
        string phoneNumber,
        string otp,
        string purpose,
        string? contextId,
        CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber, purpose, contextId);

        var entry = await cache.GetOrDefaultAsync<OtpCacheEntry>(cacheKey, token: cancellationToken);

        if (entry is null)
        {
            return OtpVerificationOutcome.InvalidOtp;
        }

        if (!string.Equals(entry.Otp, otp, StringComparison.Ordinal))
        {
            var updatedFailedAttempts = entry.FailedAttempts + 1;

            if (updatedFailedAttempts >= MaxFailedAttempts)
            {
                await cache.RemoveAsync(cacheKey, token: cancellationToken);
                return OtpVerificationOutcome.TooManyAttempts;
            }

            var remaining = entry.ExpiresAt - DateTimeOffset.UtcNow;
            if (remaining <= TimeSpan.Zero)
            {
                return OtpVerificationOutcome.InvalidOtp;
            }

            await cache.SetAsync(
                cacheKey,
                entry with { FailedAttempts = updatedFailedAttempts },
                new FusionCacheEntryOptions { Duration = remaining },
                token: cancellationToken);

            return OtpVerificationOutcome.InvalidOtp;
        }

        await cache.RemoveAsync(cacheKey, token: cancellationToken);

        return OtpVerificationOutcome.Success;
    }

    public abstract string Generate();
}
