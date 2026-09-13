using System.Security.Cryptography;
using Common.Application.Options;

namespace Notifications.Infrastructure.Otp;

/// <summary>
/// Single place that turns <see cref="OtpOptions"/> into a code, shared by the Redis and in-memory
/// OTP stores so the dummy-code switch is independent of where the OTP is stored.
/// </summary>
internal static class OtpCodeGenerator
{
    public static string Generate(OtpOptions options)
    {
        if (options.DummyCode is not null)
        {
            return options.DummyCode;
        }

        var otp = new char[options.Length];
        for (var i = 0; i < otp.Length; i++)
        {
            otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
        }

        return new string(otp);
    }
}
