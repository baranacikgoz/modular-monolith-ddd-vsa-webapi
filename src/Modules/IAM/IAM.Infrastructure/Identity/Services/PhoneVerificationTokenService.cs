using System.Security.Cryptography;
using IAM.Application.Users.Services;

namespace IAM.Infrastructure.Identity.Services;

internal class PhoneVerificationTokenService : IPhoneVerificationTokenService
{
    private const int Length = 32;
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string Generate()
    {
        var buffer = new byte[Length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }

        var otp = new char[Length];
        for (var i = 0; i < Length; i++)
        {
            otp[i] = Characters[buffer[i] % Characters.Length];
        }

        return new string(otp);
    }
}
