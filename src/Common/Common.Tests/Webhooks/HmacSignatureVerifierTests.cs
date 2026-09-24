using System.Security.Cryptography;
using System.Text;
using Common.Endpoints.Webhooks;
using Xunit;

namespace Common.Tests.Webhooks;

public class HmacSignatureVerifierTests
{
    private const string Secret = "webhook-secret";
    private static readonly byte[] Body = Encoding.UTF8.GetBytes("{\"orderNumber\":\"42\"}");

    private static byte[] Digest()
    {
        return HMACSHA256.HashData(Encoding.UTF8.GetBytes(Secret), Body);
    }

    [Fact]
    public void VerifySha256_LowercaseHex_IsValid()
    {
        Assert.True(HmacSignatureVerifier.VerifySha256(Secret, Body, Convert.ToHexStringLower(Digest())));
    }

    [Fact]
    public void VerifySha256_PrefixedUppercaseHex_IsValid()
    {
        Assert.True(HmacSignatureVerifier.VerifySha256(Secret, Body, "sha256=" + Convert.ToHexString(Digest())));
    }

    [Fact]
    public void VerifySha256_Base64_IsValid()
    {
        Assert.True(HmacSignatureVerifier.VerifySha256(Secret, Body, Convert.ToBase64String(Digest())));
    }

    [Fact]
    public void VerifySha256_TamperedBody_IsInvalid()
    {
        var tampered = Encoding.UTF8.GetBytes("{\"orderNumber\":\"43\"}");
        Assert.False(HmacSignatureVerifier.VerifySha256(Secret, tampered, Convert.ToHexStringLower(Digest())));
    }

    [Fact]
    public void VerifySha256_WrongSecret_IsInvalid()
    {
        Assert.False(HmacSignatureVerifier.VerifySha256("other", Body, Convert.ToHexStringLower(Digest())));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-a-digest")]
    [InlineData("zz")]
    // Digest-length (64 chars) but not hex: must not reach a throwing hex decoder.
    [InlineData("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz")]
    [InlineData("sha256=0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdeg")]
    public void VerifySha256_MalformedHeader_IsInvalidWithoutThrowing(string? header)
    {
        Assert.False(HmacSignatureVerifier.VerifySha256(Secret, Body, header));
    }
}
