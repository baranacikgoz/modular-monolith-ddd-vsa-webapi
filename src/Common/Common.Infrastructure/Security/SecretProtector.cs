using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;
using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Security;

/// <summary>
///     AES-GCM encryption of secrets a module has to store and later read back in clear (third-party credentials, a
///     generated webhook secret). Output layout is format version (1 byte) || key id (4 bytes) || nonce (12 bytes) ||
///     tag (16 bytes) || ciphertext, so one value is one self-contained <c>byte[]</c> column. The version and key id are
///     authenticated as associated data. The key id is derived from the key (never the key itself), so
///     <see cref="Decrypt" /> picks the key that protected the value by id and never tries the others.
///     <see cref="Encrypt" /> always uses <see cref="SecretProtectionOptions.EncryptionKey" />;
///     <see cref="SecretProtectionOptions.PreviousEncryptionKeys" /> only decrypt, which is what lets a key be rotated:
///     callers that store values find the ones still under an older key with <see cref="NeedsReprotect" /> and rewrite
///     them with <see cref="Reprotect" />. A tampered value, a truncated value, an unsupported version or a value whose
///     key is not configured fails with a <see cref="CryptographicException" />. A secret that is only ever compared
///     (a password, an inbound token) must be hashed instead, never encrypted.
/// </summary>
public sealed class SecretProtector
{
    private const byte FormatVersion = 1;
    private const int VersionSizeBytes = 1;
    private const int KeyIdSizeBytes = sizeof(uint);
    private const int HeaderSizeBytes = VersionSizeBytes + KeyIdSizeBytes;
    private const int NonceSizeBytes = 12;
    private const int TagSizeBytes = 16;
    private const int MinimumProtectedLengthBytes = HeaderSizeBytes + NonceSizeBytes + TagSizeBytes;

    private static readonly byte[] KeyIdLabel = "SecretProtector.KeyId"u8.ToArray();

    private readonly byte[] _currentKey;
    private readonly uint _currentKeyId;
    private readonly Dictionary<uint, byte[]> _keysById = [];

    public SecretProtector(IOptions<SecretProtectionOptions> options)
    {
        var value = options.Value;

        _currentKey = DecodeKey(value.EncryptionKey, nameof(SecretProtectionOptions.EncryptionKey));
        _currentKeyId = ComputeKeyId(_currentKey);
        _keysById.Add(_currentKeyId, _currentKey);

        for (var i = 0; i < value.PreviousEncryptionKeys.Count; i++)
        {
            var name = $"{nameof(SecretProtectionOptions.PreviousEncryptionKeys)}[{i}]";
            var key = DecodeKey(value.PreviousEncryptionKeys[i], name);

            if (!_keysById.TryAdd(ComputeKeyId(key), key))
            {
                throw new InvalidOperationException(
                    $"{nameof(SecretProtectionOptions)}.{name} repeats a key that is already configured.");
            }
        }
    }

    public byte[] Encrypt(string plaintext)
    {
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        try
        {
            return Seal(plaintextBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(plaintextBytes);
        }
    }

    public string Decrypt(byte[] encrypted)
    {
        var plaintextBytes = Open(encrypted);
        try
        {
            return Encoding.UTF8.GetString(plaintextBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(plaintextBytes);
        }
    }

    /// <summary>
    ///     True when the value was protected under a key other than the current one, so the caller should store
    ///     <see cref="Reprotect" />'s result. Only the header is read: nothing is decrypted or authenticated. Throws
    ///     <see cref="CryptographicException" /> when the value is too short or has an unsupported format version.
    /// </summary>
    public bool NeedsReprotect(byte[] protectedValue)
    {
        return ReadKeyId(protectedValue) != _currentKeyId;
    }

    /// <summary>
    ///     Decrypts the value with the key that protected it and encrypts the same plaintext again under the current
    ///     key with a fresh nonce. The input is not modified. Throws like <see cref="Decrypt" /> when the value is
    ///     tampered with or its key is not configured.
    /// </summary>
    public byte[] Reprotect(byte[] protectedValue)
    {
        var plaintextBytes = Open(protectedValue);
        try
        {
            return Seal(plaintextBytes);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(plaintextBytes);
        }
    }

    private byte[] Seal(ReadOnlySpan<byte> plaintext)
    {
        var output = new byte[MinimumProtectedLengthBytes + plaintext.Length];
        var header = output.AsSpan(0, HeaderSizeBytes);
        var nonce = output.AsSpan(HeaderSizeBytes, NonceSizeBytes);
        var tag = output.AsSpan(HeaderSizeBytes + NonceSizeBytes, TagSizeBytes);
        var ciphertext = output.AsSpan(MinimumProtectedLengthBytes);

        header[0] = FormatVersion;
        BinaryPrimitives.WriteUInt32BigEndian(header[VersionSizeBytes..], _currentKeyId);
        RandomNumberGenerator.Fill(nonce);

        using var aesGcm = new AesGcm(_currentKey, TagSizeBytes);
        aesGcm.Encrypt(nonce, plaintext, ciphertext, tag, header);

        return output;
    }

    private byte[] Open(byte[] protectedValue)
    {
        var keyId = ReadKeyId(protectedValue);

        if (!_keysById.TryGetValue(keyId, out var key))
        {
            throw new CryptographicException(
                $"The value was protected with key id {keyId:X8}, which is neither the current {nameof(SecretProtectionOptions.EncryptionKey)} " +
                $"nor one of the {nameof(SecretProtectionOptions.PreviousEncryptionKeys)}.");
        }

        var header = protectedValue.AsSpan(0, HeaderSizeBytes);
        var nonce = protectedValue.AsSpan(HeaderSizeBytes, NonceSizeBytes);
        var tag = protectedValue.AsSpan(HeaderSizeBytes + NonceSizeBytes, TagSizeBytes);
        var ciphertext = protectedValue.AsSpan(MinimumProtectedLengthBytes);

        var plaintextBytes = new byte[ciphertext.Length];
        using var aesGcm = new AesGcm(key, TagSizeBytes);
        aesGcm.Decrypt(nonce, ciphertext, tag, plaintextBytes, header);

        return plaintextBytes;
    }

    private static uint ReadKeyId(byte[] protectedValue)
    {
        if (protectedValue.Length < MinimumProtectedLengthBytes)
        {
            throw new CryptographicException("The protected value is shorter than its header, nonce and tag.");
        }

        if (protectedValue[0] != FormatVersion)
        {
            throw new CryptographicException($"The protected value has format version {protectedValue[0]}, expected {FormatVersion}.");
        }

        return BinaryPrimitives.ReadUInt32BigEndian(protectedValue.AsSpan(VersionSizeBytes, KeyIdSizeBytes));
    }

    private static uint ComputeKeyId(byte[] key)
    {
        return BinaryPrimitives.ReadUInt32BigEndian(HMACSHA256.HashData(key, KeyIdLabel));
    }

    private static byte[] DecodeKey(string encoded, string name)
    {
        return SecretProtectionOptionsValidator.TryDecodeKey(encoded)
               ?? throw new InvalidOperationException(
                   $"{nameof(SecretProtectionOptions)}.{name} must be base64 that decodes to exactly " +
                   $"{SecretProtectionOptionsValidator.KeySizeBytes} bytes.");
    }
}
