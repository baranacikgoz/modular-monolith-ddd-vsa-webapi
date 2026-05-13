using System.Security.Cryptography;
using System.Text;

namespace Common.Application.Caching;

public static class CacheKeys
{
    public static class For
    {
        public static string Otp(string phoneNumber)
        {
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(phoneNumber)));
            return $"otp:{hash}";
        }
    }
}
