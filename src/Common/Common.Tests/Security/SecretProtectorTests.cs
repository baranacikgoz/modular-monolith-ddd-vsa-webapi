using System.Security.Cryptography;
using System.Text;
using Common.Application.Options;
using Common.Infrastructure.Security;
using Microsoft.Extensions.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Security;

public sealed class SecretProtectorTests
{
    private const int NonceSizeBytes = 12;
    private const int TagSizeBytes = 16;

    private static SecretProtector NewProtector(string? key = null)
    {
        return new SecretProtector(Options.Create(new SecretProtectionOptions
        {
            EncryptionKey = key ?? SecretProtectionOptionsValidator.DevelopmentPlaceholderKey
        }));
    }

    private static string NewKey() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(SecretProtectionOptionsValidator.KeySizeBytes));

    [Theory]
    [InlineData("super-secret-value")]
    [InlineData("""{"ApiKey":"key","ApiSecret":"secret"}""")]
    [InlineData("")]
    [InlineData("päßwörd with ünïcode and emoji \U0001F511")]
    public void Encrypt_ThenDecrypt_RoundTrips(string plaintext)
    {
        var protector = NewProtector();

        var decrypted = protector.Decrypt(protector.Encrypt(plaintext));

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Encrypt_Output_IsNonceThenTagThenCiphertextAndHidesThePlaintext()
    {
        var protector = NewProtector();
        const string plaintext = "super-secret-value";
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        var encrypted = protector.Encrypt(plaintext);

        Assert.Equal(NonceSizeBytes + TagSizeBytes + plaintextBytes.Length, encrypted.Length);
        Assert.NotEqual(plaintextBytes, encrypted.AsSpan(NonceSizeBytes + TagSizeBytes).ToArray());
    }

    [Fact]
    public void Encrypt_SamePlaintextTwice_ProducesDifferentCiphertext()
    {
        var protector = NewProtector();

        var first = protector.Encrypt("value");
        var second = protector.Encrypt("value");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Encrypt_ManyTimes_NeverReusesANonce()
    {
        var protector = NewProtector();
        const int count = 2_000;

        var nonces = Enumerable.Range(0, count)
            .Select(_ => Convert.ToHexString(protector.Encrypt("value").AsSpan(0, NonceSizeBytes)))
            .ToHashSet(StringComparer.Ordinal);

        Assert.Equal(count, nonces.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NonceSizeBytes)]
    [InlineData(NonceSizeBytes + TagSizeBytes)]
    public void Decrypt_AnyByteFlipped_Throws(int index)
    {
        var protector = NewProtector();
        var encrypted = protector.Encrypt("value");
        encrypted[index] ^= 0xFF;

        Assert.ThrowsAny<CryptographicException>(() => protector.Decrypt(encrypted));
    }

    [Fact]
    public void Decrypt_LastCiphertextByteFlipped_Throws()
    {
        var protector = NewProtector();
        var encrypted = protector.Encrypt("value");
        encrypted[^1] ^= 0xFF;

        Assert.ThrowsAny<CryptographicException>(() => protector.Decrypt(encrypted));
    }

    [Fact]
    public void Decrypt_UnderADifferentKey_Throws()
    {
        var encrypted = NewProtector(NewKey()).Encrypt("value");

        Assert.ThrowsAny<CryptographicException>(() => NewProtector(NewKey()).Decrypt(encrypted));
    }

    [Fact]
    public void Decrypt_UnderTheSameKeyFromAnotherInstance_RoundTrips()
    {
        var key = NewKey();
        var encrypted = NewProtector(key).Encrypt("value");

        Assert.Equal("value", NewProtector(key).Decrypt(encrypted));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NonceSizeBytes + TagSizeBytes - 1)]
    public void Decrypt_InputShorterThanNonceAndTag_Throws(int length)
    {
        var protector = NewProtector();

        Assert.ThrowsAny<CryptographicException>(() => protector.Decrypt(new byte[length]));
    }

    [Theory]
    [InlineData(16)]
    [InlineData(24)]
    [InlineData(33)]
    public void Constructor_KeyThatIsNotThirtyTwoBytes_Throws(int keyLength)
    {
        Assert.Throws<InvalidOperationException>(() => NewProtector(Convert.ToBase64String(new byte[keyLength])));
    }

    [Fact]
    public void Constructor_KeyThatIsNotBase64_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => NewProtector("not base64!"));
    }
}
