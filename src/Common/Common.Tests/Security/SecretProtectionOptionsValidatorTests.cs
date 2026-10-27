using System.Security.Cryptography;
using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Security;

public sealed class SecretProtectionOptionsValidatorTests
{
    private static string KeyOfLength(int bytes) => Convert.ToBase64String(new byte[bytes]);

    private static string RandomKey() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

    private static SecretProtectionOptions NewOptions(string key, params string[] previousKeys)
    {
        return new SecretProtectionOptions { EncryptionKey = key, PreviousEncryptionKeys = previousKeys };
    }

    private static ValidationContext<SecretProtectionOptions> BuildContext(SecretProtectionOptions options, string environmentName)
    {
        var context = new ValidationContext<SecretProtectionOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void ThirtyTwoByteKey_PassesValidation()
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(KeyOfLength(32)));

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(16)]
    [InlineData(24)]
    [InlineData(31)]
    [InlineData(33)]
    [InlineData(64)]
    public void KeyThatDoesNotDecodeToThirtyTwoBytes_IsInvalid(int length)
    {
        var options = NewOptions(KeyOfLength(length));

        var result = new SecretProtectionOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.EncryptionKey));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not base64 at all!")]
    [InlineData("bG9jYWw")]
    public void KeyThatIsNotBase64_IsInvalid(string key)
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(key));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.EncryptionKey));
    }

    [Theory]
    [InlineData("Production")]
    [InlineData("Staging")]
    public void DevelopmentPlaceholderKey_OutsideDevelopment_IsInvalid(string environmentName)
    {
        var options = NewOptions(SecretProtectionOptionsValidator.DevelopmentPlaceholderKey);

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, environmentName));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.EncryptionKey));
    }

    [Fact]
    public void DevelopmentPlaceholderKey_InDevelopment_IsValid()
    {
        var options = NewOptions(SecretProtectionOptionsValidator.DevelopmentPlaceholderKey);

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void EmptyPreviousKeyList_IsValid()
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(RandomKey()));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void PreviousKeysOfThirtyTwoBytes_AreValid()
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(RandomKey(), RandomKey(), RandomKey()));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void NullPreviousKeyList_IsInvalid()
    {
        var options = new SecretProtectionOptions { EncryptionKey = RandomKey(), PreviousEncryptionKeys = null! };

        var result = new SecretProtectionOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.PreviousEncryptionKeys));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(16)]
    [InlineData(31)]
    [InlineData(33)]
    public void PreviousKeyThatDoesNotDecodeToThirtyTwoBytes_IsInvalid(int length)
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(RandomKey(), KeyOfLength(length)));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(SecretProtectionOptions.PreviousEncryptionKeys), StringComparison.Ordinal));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not base64 at all!")]
    public void PreviousKeyThatIsNotBase64_IsInvalid(string key)
    {
        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(RandomKey(), key));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(SecretProtectionOptions.PreviousEncryptionKeys), StringComparison.Ordinal));
    }

    [Fact]
    public void PreviousKeyEqualToTheCurrentKey_IsInvalid()
    {
        var key = RandomKey();

        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(key, key));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.PreviousEncryptionKeys));
    }

    [Fact]
    public void RepeatedPreviousKey_IsInvalid()
    {
        var previous = RandomKey();

        var result = new SecretProtectionOptionsValidator().Validate(NewOptions(RandomKey(), previous, previous));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.PreviousEncryptionKeys));
    }

    [Theory]
    [InlineData("Production")]
    [InlineData("Staging")]
    public void DevelopmentPlaceholderKeyAsPreviousKey_OutsideDevelopment_IsInvalid(string environmentName)
    {
        var options = NewOptions(RandomKey(), SecretProtectionOptionsValidator.DevelopmentPlaceholderKey);

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, environmentName));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(SecretProtectionOptions.PreviousEncryptionKeys), StringComparison.Ordinal));
    }

    [Fact]
    public void DevelopmentPlaceholderKeyAsPreviousKey_InDevelopment_IsValid()
    {
        var options = NewOptions(RandomKey(), SecretProtectionOptionsValidator.DevelopmentPlaceholderKey);

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }
}
