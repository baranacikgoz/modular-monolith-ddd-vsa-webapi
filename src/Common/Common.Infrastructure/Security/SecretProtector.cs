using System.Security.Cryptography;
using System.Text;
using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Security;

/// <summary>
///     AES-GCM encryption of secrets a module has to store and later read back in clear (third-party credentials, a
///     generated webhook secret). Output layout is nonce (12 bytes) || tag (16 bytes) || ciphertext, so one value is one
///     self-contained <c>byte[]</c> column. The master key comes from <see cref="SecretProtectionOptions.EncryptionKey" />
///     and is never stored alongside the ciphertext. A tampered value, a truncated value or a value encrypted under another
///     key fails <see cref="Decrypt" /> with a <see cref="CryptographicException" />. A secret that is only ever compared
///     (a password, an inbound token) must be hashed instead, never encrypted.
/// </summary>
public sealed class SecretProtector
{
    private const int NonceSizeBytes = 12;
    private const int TagSizeBytes = 16;

    private readonly byte[] _key;

    public SecretProtector(IOptions<SecretProtectionOptions> options)
    {
        var encoded = options.Value.EncryptionKey;
        var buffer = new byte[encoded.Length];

        if (!Convert.TryFromBase64String(encoded, buffer, out var written) || written != SecretProtectionOptionsValidator.KeySizeBytes)
        {
            throw new InvalidOperationException(
                $"{nameof(SecretProtectionOptions)}.{nameof(SecretProtectionOptions.EncryptionKey)} must be base64 that decodes to exactly " +
                $"{SecretProtectionOptionsValidator.KeySizeBytes} bytes.");
        }

        _key = buffer.AsSpan(0, written).ToArray();
    }

    public byte[] Encrypt(string plaintext)
    {
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        var output = new byte[NonceSizeBytes + TagSizeBytes + plaintextBytes.Length];
        var nonce = output.AsSpan(0, NonceSizeBytes);
        var tag = output.AsSpan(NonceSizeBytes, TagSizeBytes);
        var ciphertext = output.AsSpan(NonceSizeBytes + TagSizeBytes);

        RandomNumberGenerator.Fill(nonce);

        using var aesGcm = new AesGcm(_key, TagSizeBytes);
        aesGcm.Encrypt(nonce, plaintextBytes, ciphertext, tag);

        return output;
    }

    public string Decrypt(byte[] encrypted)
    {
        if (encrypted.Length < NonceSizeBytes + TagSizeBytes)
        {
            throw new CryptographicException("The encrypted value is shorter than its nonce and tag.");
        }

        var nonce = encrypted.AsSpan(0, NonceSizeBytes);
        var tag = encrypted.AsSpan(NonceSizeBytes, TagSizeBytes);
        var ciphertext = encrypted.AsSpan(NonceSizeBytes + TagSizeBytes);

        var plaintextBytes = new byte[ciphertext.Length];
        using var aesGcm = new AesGcm(_key, TagSizeBytes);
        aesGcm.Decrypt(nonce, ciphertext, tag, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
}
