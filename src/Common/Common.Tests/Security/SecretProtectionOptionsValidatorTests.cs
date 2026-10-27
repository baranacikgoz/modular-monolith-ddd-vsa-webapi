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

    private static ValidationContext<SecretProtectionOptions> BuildContext(SecretProtectionOptions options, string environmentName)
    {
        var context = new ValidationContext<SecretProtectionOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void ThirtyTwoByteKey_PassesValidation()
    {
        var result = new SecretProtectionOptionsValidator().Validate(new SecretProtectionOptions { EncryptionKey = KeyOfLength(32) });

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
        var options = new SecretProtectionOptions { EncryptionKey = KeyOfLength(length) };

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
        var result = new SecretProtectionOptionsValidator().Validate(new SecretProtectionOptions { EncryptionKey = key });

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.EncryptionKey));
    }

    [Theory]
    [InlineData("Production")]
    [InlineData("Staging")]
    public void DevelopmentPlaceholderKey_OutsideDevelopment_IsInvalid(string environmentName)
    {
        var options = new SecretProtectionOptions { EncryptionKey = SecretProtectionOptionsValidator.DevelopmentPlaceholderKey };

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, environmentName));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(SecretProtectionOptions.EncryptionKey));
    }

    [Fact]
    public void DevelopmentPlaceholderKey_InDevelopment_IsValid()
    {
        var options = new SecretProtectionOptions { EncryptionKey = SecretProtectionOptionsValidator.DevelopmentPlaceholderKey };

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }
}
