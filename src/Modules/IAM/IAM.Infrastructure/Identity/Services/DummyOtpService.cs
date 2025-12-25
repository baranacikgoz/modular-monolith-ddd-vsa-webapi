using Common.Application.Caching;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Domain.Errors;

namespace IAM.Infrastructure.Identity.Services;

internal class DummyOtpService(ICacheService cache) : IOtpService
{
    private const string DummyOtp = "123456";

    public async Task<Result> VerifyThenRemoveOtpAsync(string phoneNumber, string otp,
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

    public string Generate()
    {
        return DummyOtp;
    }
}
