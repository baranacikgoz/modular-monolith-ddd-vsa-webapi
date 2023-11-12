using System.Globalization;
using System.Security.Cryptography;
using Common.Caching;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal sealed class OtpService(
    ICacheService cacheService,
    IOptions<OtpOptions> otpOptionsProvider
    ) : IOtpService
{
    private readonly OtpOptions _otpOptions = otpOptionsProvider.Value;
    public Task<string> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString(CultureInfo.InvariantCulture);
        var cacheKey = CacheKey(phoneNumber);
        return cacheService.GetOrSetAsync(cacheKey, otp, TimeSpan.FromMinutes(_otpOptions.ExpirationInMinutes), cancellationToken: cancellationToken);
    }

    public async Task<Result> ValidateAsync(string otp, string phoneNumber, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKey(phoneNumber);
        var code = await cacheService.GetAsync<string>(cacheKey, cancellationToken);

        if (code.IsNullOrEmpty())
        {
            return OtpErrors.InvalidOtp;
        }

        return Result.Success;
    }

    private static string CacheKey(string phoneNumber) => $"otp:{phoneNumber}";
}
