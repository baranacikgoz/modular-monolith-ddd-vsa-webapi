using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace Common.Endpoints.Webhooks;

/// <summary>
///     Constant-time check of a partner webhook's HMAC-SHA256 body signature. Accepts the digest as lowercase or
///     uppercase hex or as base64, with or without a <c>sha256=</c> prefix, since every partner formats it
///     differently. Never throws on a malformed header: a signature that does not decode is simply invalid.
/// </summary>
public static class HmacSignatureVerifier
{
    private const string Sha256Prefix = "sha256=";

    public static bool VerifySha256(string secret, ReadOnlySpan<byte> body, string? presentedSignature)
    {
        ArgumentException.ThrowIfNullOrEmpty(secret);

        if (string.IsNullOrWhiteSpace(presentedSignature))
        {
            return false;
        }

        var candidate = presentedSignature.Trim();
        if (candidate.StartsWith(Sha256Prefix, StringComparison.OrdinalIgnoreCase))
        {
            candidate = candidate[Sha256Prefix.Length..];
        }

        if (!TryDecode(candidate, out var presented))
        {
            return false;
        }

        Span<byte> expected = stackalloc byte[HMACSHA256.HashSizeInBytes];
        HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), body, expected);

        return CryptographicOperations.FixedTimeEquals(expected, presented);
    }

    private static bool TryDecode(string candidate, out byte[] bytes)
    {
        var buffer = new byte[HMACSHA256.HashSizeInBytes + 2];

        // The span overload reports a non-hex character as a status instead of throwing FormatException.
        if (candidate.Length == HMACSHA256.HashSizeInBytes * 2
            && Convert.FromHexString(candidate.AsSpan(), buffer, out _, out var hexWritten) == OperationStatus.Done
            && hexWritten == HMACSHA256.HashSizeInBytes)
        {
            bytes = buffer[..hexWritten];
            return true;
        }

        if (Convert.TryFromBase64String(candidate, buffer, out var written) && written == HMACSHA256.HashSizeInBytes)
        {
            bytes = buffer[..written];
            return true;
        }

        bytes = [];
        return false;
    }
}
