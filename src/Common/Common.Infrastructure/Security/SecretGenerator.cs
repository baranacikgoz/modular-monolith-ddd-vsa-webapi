using System.Buffers.Text;
using System.Security.Cryptography;

namespace Common.Infrastructure.Security;

/// <summary>
///     Random credentials this system invents for a caller to present back to it (a webhook shared secret, an API key and
///     its public handle), drawn from the operating system's cryptographic generator. Not a way to derive anything:
///     the value has no structure and must be stored through <see cref="SecretProtector" /> or hashed.
/// </summary>
public static class SecretGenerator
{
    public const int DefaultSecretBytes = 32;
    public const int MinSecretBytes = 16;

    /// <summary>URL-safe base64 without padding of <paramref name="byteLength" /> random bytes, so it can sit in a header, a query string or a path unescaped.</summary>
    public static string GenerateSecret(int byteLength = DefaultSecretBytes)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(byteLength, MinSecretBytes);

        return Base64Url.EncodeToString(RandomNumberGenerator.GetBytes(byteLength));
    }

    /// <summary>A public, non-secret handle for a generated credential: <c>{prefix}_{HEX}</c>, e.g. the user name half of a generated user name and secret pair.</summary>
    public static string GenerateIdentifier(string prefix, int byteLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);
        ArgumentOutOfRangeException.ThrowIfLessThan(byteLength, 1);

        return $"{prefix}_{Convert.ToHexString(RandomNumberGenerator.GetBytes(byteLength))}";
    }
}
