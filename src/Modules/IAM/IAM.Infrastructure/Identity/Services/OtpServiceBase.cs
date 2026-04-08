using Common.Application.Caching;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Domain.Errors;

namespace IAM.Infrastructure.Identity.Services;

/// <summary>
///     Base class for OTP services. Contains the shared cache-based verification logic
///     so that concrete implementations only need to override <see cref="Generate" />.
/// </summary>
internal abstract class OtpServiceBase(ICacheService cache) : IOtpService
{
    public async Task<Result> VerifyThenRemoveOtpAsync(
        string phoneNumber,
        string otp,
        CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.For.Otp(phoneNumber);

        var otpFromCache = await cache.GetAsync<string>(cacheKey, cancellationToken);

        if (!string.Equals(otpFromCache, otp, StringComparison.Ordinal))
        {
            return OtpErrors.InvalidOtp;
        }

        await cache.RemoveAsync(cacheKey, cancellationToken);

        return Result.Success;
    }

    public abstract string Generate();
}
