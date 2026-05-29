using System.Security.Cryptography;
using System.Text;

namespace Common.Application.Caching;

public static class CacheKeys
{
    public static class For
    {
        public static string Otp(string phoneNumber, string purpose, string? contextId = null)
        {
            var input = string.IsNullOrEmpty(contextId) ? phoneNumber : $"{phoneNumber}|{contextId}";
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
            return $"otp:{purpose}:{hash}";
        }
    }
}
