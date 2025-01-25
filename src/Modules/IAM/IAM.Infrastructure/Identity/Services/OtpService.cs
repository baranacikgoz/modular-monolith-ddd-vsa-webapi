using System.Security.Cryptography;
using Common.Infrastructure.Options;
using IAM.Application.Users.Services;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class OtpService(IOptions<OtpOptions> otpOptionsProvider) : IOtpService
{
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
