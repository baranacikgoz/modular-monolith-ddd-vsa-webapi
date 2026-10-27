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
    private const int VersionSizeBytes = 1;
    private const int KeyIdSizeBytes = 4;
    private const int HeaderSizeBytes = VersionSizeBytes + KeyIdSizeBytes;
    private const int NonceSizeBytes = 12;
    private const int TagSizeBytes = 16;
    private const int OverheadBytes = HeaderSizeBytes + NonceSizeBytes + TagSizeBytes;

    private static SecretProtector NewProtector(string? key = null, params string[] previousKeys)
    {
        return new SecretProtector(Options.Create(new SecretProtectionOptions
        {
            EncryptionKey = key ?? SecretProtectionOptionsValidator.DevelopmentPlaceholderKey,
            PreviousEncryptionKeys = previousKeys
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
    public void Encrypt_Output_IsHeaderThenNonceThenTagThenCiphertextAndHidesThePlaintext()
    {
        var protector = NewProtector();
        const string plaintext = "super-secret-value";
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        var encrypted = protector.Encrypt(plaintext);

        Assert.Equal(OverheadBytes + plaintextBytes.Length, encrypted.Length);
        Assert.NotEqual(plaintextBytes, encrypted.AsSpan(OverheadBytes).ToArray());
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
            .Select(_ => Convert.ToHexString(protector.Encrypt("value").AsSpan(HeaderSizeBytes, NonceSizeBytes)))
            .ToHashSet(StringComparer.Ordinal);

        Assert.Equal(count, nonces.Count);
    }

    [Fact]
    public void Encrypt_SameKeyTwice_WritesTheSameKeyId()
    {
        var protector = NewProtector();

        var first = protector.Encrypt("one").AsSpan(VersionSizeBytes, KeyIdSizeBytes).ToArray();
        var second = protector.Encrypt("two").AsSpan(VersionSizeBytes, KeyIdSizeBytes).ToArray();

        Assert.Equal(first, second);
    }

    [Fact]
    public void Encrypt_DifferentKeys_WriteDifferentKeyIds()
    {
        var first = NewProtector(NewKey()).Encrypt("value").AsSpan(VersionSizeBytes, KeyIdSizeBytes).ToArray();
        var second = NewProtector(NewKey()).Encrypt("value").AsSpan(VersionSizeBytes, KeyIdSizeBytes).ToArray();

        Assert.NotEqual(first, second);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(VersionSizeBytes)]
    [InlineData(HeaderSizeBytes - 1)]
    [InlineData(HeaderSizeBytes)]
    [InlineData(HeaderSizeBytes + NonceSizeBytes)]
    [InlineData(OverheadBytes)]
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
    [InlineData(HeaderSizeBytes)]
    [InlineData(OverheadBytes - 1)]
    public void Decrypt_InputShorterThanHeaderNonceAndTag_Throws(int length)
    {
        var protector = NewProtector();

        Assert.ThrowsAny<CryptographicException>(() => protector.Decrypt(new byte[length]));
    }

    [Fact]
    public void Decrypt_UnsupportedFormatVersion_ThrowsAndNamesTheVersion()
    {
        var protector = NewProtector();
        var encrypted = protector.Encrypt("value");
        encrypted[0] = 0x7F;

        var exception = Assert.ThrowsAny<CryptographicException>(() => protector.Decrypt(encrypted));

        Assert.Contains("version", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Decrypt_UnknownKeyId_ThrowsAClearErrorInsteadOfTryingEveryKey()
    {
        var encrypted = NewProtector(NewKey()).Encrypt("value");
        var other = NewProtector(NewKey(), NewKey());

        var exception = Assert.ThrowsAny<CryptographicException>(() => other.Decrypt(encrypted));

        Assert.IsNotType<AuthenticationTagMismatchException>(exception);
        Assert.Contains("key id", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(nameof(SecretProtectionOptions.PreviousEncryptionKeys), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Decrypt_KeyIdSwappedToAnotherConfiguredKey_FailsAuthentication()
    {
        var previousKey = NewKey();
        var protector = NewProtector(NewKey(), previousKey);
        var encrypted = protector.Encrypt("value");
        var previousId = NewProtector(previousKey).Encrypt("value").AsSpan(VersionSizeBytes, KeyIdSizeBytes).ToArray();
        previousId.CopyTo(encrypted.AsSpan(VersionSizeBytes));

        Assert.ThrowsAny<AuthenticationTagMismatchException>(() => protector.Decrypt(encrypted));
    }

    [Fact]
    public void Decrypt_ValueFromThePreviousKey_RoundTripsAfterRotation()
    {
        var oldKey = NewKey();
        var encryptedBeforeRotation = NewProtector(oldKey).Encrypt("value");

        var rotated = NewProtector(NewKey(), oldKey);

        Assert.Equal("value", rotated.Decrypt(encryptedBeforeRotation));
    }

    [Fact]
    public void Decrypt_ValueFromAKeyThatWasDroppedFromTheList_Throws()
    {
        var encryptedBeforeRotation = NewProtector(NewKey()).Encrypt("value");

        var rotated = NewProtector(NewKey());

        Assert.ThrowsAny<CryptographicException>(() => rotated.Decrypt(encryptedBeforeRotation));
    }

    [Fact]
    public void Decrypt_WithSeveralPreviousKeys_PicksTheOneThatProtectedTheValue()
    {
        var first = NewKey();
        var second = NewKey();
        var third = NewKey();
        var fromFirst = NewProtector(first).Encrypt("one");
        var fromSecond = NewProtector(second).Encrypt("two");
        var fromThird = NewProtector(third).Encrypt("three");

        var rotated = NewProtector(NewKey(), first, second, third);

        Assert.Equal("one", rotated.Decrypt(fromFirst));
        Assert.Equal("two", rotated.Decrypt(fromSecond));
        Assert.Equal("three", rotated.Decrypt(fromThird));
    }

    [Fact]
    public void Encrypt_AfterRotation_UsesTheCurrentKeyOnly()
    {
        var oldKey = NewKey();
        var currentKey = NewKey();
        var rotated = NewProtector(currentKey, oldKey);

        var encrypted = rotated.Encrypt("value");

        Assert.Equal("value", NewProtector(currentKey).Decrypt(encrypted));
        Assert.ThrowsAny<CryptographicException>(() => NewProtector(oldKey).Decrypt(encrypted));
    }

    [Fact]
    public void Decrypt_TamperedValueUnderAPreviousKey_StillThrows()
    {
        var oldKey = NewKey();
        var encrypted = NewProtector(oldKey).Encrypt("value");
        encrypted[^1] ^= 0xFF;

        var rotated = NewProtector(NewKey(), oldKey);

        Assert.ThrowsAny<CryptographicException>(() => rotated.Decrypt(encrypted));
    }

    [Fact]
    public void NeedsReprotect_ValueUnderTheCurrentKey_IsFalse()
    {
        var protector = NewProtector(NewKey(), NewKey());

        Assert.False(protector.NeedsReprotect(protector.Encrypt("value")));
    }

    [Fact]
    public void NeedsReprotect_ValueUnderAPreviousKey_IsTrue()
    {
        var oldKey = NewKey();
        var encrypted = NewProtector(oldKey).Encrypt("value");

        Assert.True(NewProtector(NewKey(), oldKey).NeedsReprotect(encrypted));
    }

    [Fact]
    public void NeedsReprotect_ValueUnderAnUnconfiguredKey_IsTrue()
    {
        var encrypted = NewProtector(NewKey()).Encrypt("value");

        Assert.True(NewProtector(NewKey()).NeedsReprotect(encrypted));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(HeaderSizeBytes)]
    [InlineData(OverheadBytes - 1)]
    public void NeedsReprotect_InputShorterThanHeaderNonceAndTag_Throws(int length)
    {
        var protector = NewProtector();

        Assert.ThrowsAny<CryptographicException>(() => protector.NeedsReprotect(new byte[length]));
    }

    [Fact]
    public void NeedsReprotect_UnsupportedFormatVersion_Throws()
    {
        var protector = NewProtector();
        var encrypted = protector.Encrypt("value");
        encrypted[0] = 0x7F;

        Assert.ThrowsAny<CryptographicException>(() => protector.NeedsReprotect(encrypted));
    }

    [Fact]
    public void Reprotect_ValueUnderAPreviousKey_ReturnsAValueUnderTheCurrentKeyWithTheSamePlaintext()
    {
        var oldKey = NewKey();
        var currentKey = NewKey();
        var old = NewProtector(oldKey).Encrypt("value");
        var rotated = NewProtector(currentKey, oldKey);

        var reprotected = rotated.Reprotect(old);

        Assert.False(rotated.NeedsReprotect(reprotected));
        Assert.Equal("value", NewProtector(currentKey).Decrypt(reprotected));
        Assert.NotEqual(old, reprotected);
    }

    [Fact]
    public void Reprotect_ValueUnderTheCurrentKey_StaysDecryptableAndCurrent()
    {
        var protector = NewProtector(NewKey(), NewKey());
        var encrypted = protector.Encrypt("value");

        var reprotected = protector.Reprotect(encrypted);

        Assert.False(protector.NeedsReprotect(reprotected));
        Assert.Equal("value", protector.Decrypt(reprotected));
    }

    [Fact]
    public void Reprotect_ValueUnderAnUnconfiguredKey_Throws()
    {
        var encrypted = NewProtector(NewKey()).Encrypt("value");

        Assert.ThrowsAny<CryptographicException>(() => NewProtector(NewKey()).Reprotect(encrypted));
    }

    [Fact]
    public void Reprotect_TamperedValue_Throws()
    {
        var oldKey = NewKey();
        var encrypted = NewProtector(oldKey).Encrypt("value");
        encrypted[^1] ^= 0xFF;

        Assert.ThrowsAny<CryptographicException>(() => NewProtector(NewKey(), oldKey).Reprotect(encrypted));
    }

    [Fact]
    public void Reprotect_EmptyPlaintext_RoundTrips()
    {
        var oldKey = NewKey();
        var old = NewProtector(oldKey).Encrypt(string.Empty);
        var rotated = NewProtector(NewKey(), oldKey);

        Assert.Equal(string.Empty, rotated.Decrypt(rotated.Reprotect(old)));
    }

    [Fact]
    public void Reprotect_DoesNotChangeTheInputValue()
    {
        var oldKey = NewKey();
        var old = NewProtector(oldKey).Encrypt("value");
        var copy = old.ToArray();

        _ = NewProtector(NewKey(), oldKey).Reprotect(old);

        Assert.Equal(copy, old);
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

    [Fact]
    public void Constructor_PreviousKeyEqualToTheCurrentKey_Throws()
    {
        var key = NewKey();

        Assert.Throws<InvalidOperationException>(() => NewProtector(key, key));
    }

    [Fact]
    public void Constructor_RepeatedPreviousKey_Throws()
    {
        var previous = NewKey();

        Assert.Throws<InvalidOperationException>(() => NewProtector(NewKey(), previous, previous));
    }

    [Theory]
    [InlineData(16)]
    [InlineData(33)]
    public void Constructor_PreviousKeyThatIsNotThirtyTwoBytes_Throws(int keyLength)
    {
        var exception = Assert.Throws<InvalidOperationException>(
            () => NewProtector(NewKey(), Convert.ToBase64String(new byte[keyLength])));

        Assert.Contains(nameof(SecretProtectionOptions.PreviousEncryptionKeys), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Constructor_PreviousKeyThatIsNotBase64_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => NewProtector(NewKey(), "not base64!"));
    }
}
