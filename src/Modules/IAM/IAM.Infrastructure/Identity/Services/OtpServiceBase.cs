using Common.Application.Caching;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Domain.Errors;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Identity.Services;

/// <summary>
///     Base class for OTP services. Contains the shared cache-based verification logic
///     so that concrete implementations only need to override <see cref="Generate" />.
/// </summary>
internal abstract class OtpServiceBase(IFusionCache cache) : IOtpService
{
    public async Task<Result> VerifyThenRemoveOtpAsync(
        string phoneNumber,
        string otp,
        CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber);

        var otpFromCache = await cache.GetOrDefaultAsync<string>(cacheKey, token: cancellationToken);

        if (!string.Equals(otpFromCache, otp, StringComparison.Ordinal))
        {
            return OtpErrors.InvalidOtp;
        }

        await cache.RemoveAsync(cacheKey, token: cancellationToken);

        return Result.Success;
    }

    public abstract string Generate();
}
