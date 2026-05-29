using System.Security.Cryptography;
using Common.Application.Options;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Otp;

internal sealed class OtpService(IOptions<OtpOptions> otpOptionsProvider, IFusionCache cache)
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
            otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
        }

        return new string(otp);
    }
}
