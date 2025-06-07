using System.Security.Cryptography;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Domain.Errors;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class OtpService(IOptions<OtpOptions> otpOptionsProvider, ICacheService cache) : IOtpService
{
    public async Task<Result> VerifyThenRemoveOtpAsync(string phoneNumber, string otp, CancellationToken cancellationToken)
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
        var length = otpOptionsProvider.Value.Length;

        if (length <= 0)
        {
            throw new ArgumentException("Length must be greater than zero.");
        }

        // Allocate one buffer for all digits
        var buffer = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }

        // Build the OTP efficiently
        var otp = new char[length];
        for (var i = 0; i < length; i++)
        {
            otp[i] = (char)('0' + (buffer[i] % 10));
        }

        return new string(otp);
    }
}
