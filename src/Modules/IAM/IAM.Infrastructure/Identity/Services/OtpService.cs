using System.Security.Cryptography;
using Common.Application.Caching;
using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class OtpService(IOptions<OtpOptions> otpOptionsProvider, ICacheService cache)
    : OtpServiceBase(cache)
{
    public override string Generate()
    {
        var length = otpOptionsProvider.Value.Length;

        if (length <= 0)
        {
            throw new ArgumentException("Length must be greater than zero.");
        }

        var otp = new char[length];
        for (var i = 0; i < length; i++)
        {
            // Use RandomNumberGenerator.GetInt32 to avoid modulo bias
            // (buffer[i] % 10 would give digits 0-5 a ~10.16% chance vs ~9.77% for 6-9
            // because 256 is not divisible by 10).
            otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
        }

        return new string(otp);
    }
}
