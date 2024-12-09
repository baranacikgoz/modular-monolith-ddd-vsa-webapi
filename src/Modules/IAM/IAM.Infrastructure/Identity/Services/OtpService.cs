using System.Globalization;
using System.Security.Cryptography;
using Common.Application.Caching;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using IAM.Application.Identity.Services;
using IAM.Domain.Identity.Errors;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class OtpService(
    ICacheService cacheService,
    IOptions<OtpOptions> otpOptionsProvider
    ) : IOtpService
{
    private readonly OtpOptions _otpOptions = otpOptionsProvider.Value;
    public Task<string> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString(CultureInfo.InvariantCulture);
        return cacheService.GetOrCreateAsync(
            key: CacheKey(phoneNumber),
            factory: _ => new ValueTask<string>(otp),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(_otpOptions.ExpirationInMinutes),
            cancellationToken: cancellationToken);
    }

    public async Task<Result> ValidateAsync(string otp, string phoneNumber, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKey(phoneNumber);

        var code = await cacheService.GetAsync<string>(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(code))
        {
            return OtpErrors.InvalidOtp;
        }

        return Result.Success;
    }

    private static string CacheKey(string phoneNumber) => $"otp:{phoneNumber}";
}
