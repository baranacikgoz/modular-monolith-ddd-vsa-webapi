using Common.Infrastructure.Security;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Security;

public sealed class SecretGeneratorTests
{
    [Theory]
    [InlineData(16, 22)]
    [InlineData(24, 32)]
    [InlineData(32, 43)]
    public void GenerateSecret_IsUrlSafeBase64WithoutPaddingOfTheRequestedStrength(int byteLength, int expectedLength)
    {
        var secret = SecretGenerator.GenerateSecret(byteLength);

        Assert.Equal(expectedLength, secret.Length);
        Assert.All(secret, c => Assert.True(char.IsAsciiLetterOrDigit(c) || c is '-' or '_'));
    }

    [Fact]
    public void GenerateSecret_ManyTimes_NeverRepeats()
    {
        var secrets = Enumerable.Range(0, 1_000)
            .Select(_ => SecretGenerator.GenerateSecret(SecretGenerator.DefaultSecretBytes))
            .ToHashSet(StringComparer.Ordinal);

        Assert.Equal(1_000, secrets.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(SecretGenerator.MinSecretBytes - 1)]
    public void GenerateSecret_BelowTheMinimumStrength_Throws(int byteLength)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => SecretGenerator.GenerateSecret(byteLength));
    }

    [Fact]
    public void GenerateIdentifier_IsThePrefixAndUppercaseHex()
    {
        var identifier = SecretGenerator.GenerateIdentifier("wh", 6);

        Assert.Matches("^wh_[0-9A-F]{12}$", identifier);
    }

    [Fact]
    public void GenerateIdentifier_ManyTimes_NeverRepeats()
    {
        var identifiers = Enumerable.Range(0, 1_000)
            .Select(_ => SecretGenerator.GenerateIdentifier("wh", 8))
            .ToHashSet(StringComparer.Ordinal);

        Assert.Equal(1_000, identifiers.Count);
    }

    [Theory]
    [InlineData("", 6)]
    [InlineData("  ", 6)]
    public void GenerateIdentifier_WithoutAPrefix_Throws(string prefix, int byteLength)
    {
        Assert.Throws<ArgumentException>(() => SecretGenerator.GenerateIdentifier(prefix, byteLength));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GenerateIdentifier_WithoutBytes_Throws(int byteLength)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => SecretGenerator.GenerateIdentifier("wh", byteLength));
    }
}
