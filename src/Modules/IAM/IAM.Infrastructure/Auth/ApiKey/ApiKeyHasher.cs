using System.Security.Cryptography;
using System.Text;

namespace IAM.Infrastructure.Auth.ApiKey;

internal static class ApiKeyHasher
{
    public static string Hash(string rawKey)
        => Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(rawKey)));

    // Constant-time so an attacker probing keys can't learn anything from response timing.
    public static bool Matches(string rawKey, string expectedHashHex)
        => CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(Encoding.UTF8.GetBytes(rawKey)),
            Convert.FromHexString(expectedHashHex));
}
