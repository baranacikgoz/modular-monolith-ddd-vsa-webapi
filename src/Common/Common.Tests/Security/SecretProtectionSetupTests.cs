using Common.Application.Options;
using Common.Application.Validation;
using Common.Infrastructure.Security;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Security;

public sealed class SecretProtectionSetupTests
{
    private static SecretProtectionOptions ShippedOptions()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(TestPaths.RepositoryRoot, "src", "Host", "Host", "Configurations", "secretProtection.json"))
            .Build()
            .GetSection(nameof(SecretProtectionOptions))
            .Get<SecretProtectionOptions>()!;
    }

    private static ValidationContext<SecretProtectionOptions> BuildContext(SecretProtectionOptions options, string environmentName)
    {
        var context = new ValidationContext<SecretProtectionOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void AddCommonSecretProtection_RegistersOneSharedProtectorThatRoundTrips()
    {
        var services = new ServiceCollection();
        services.AddOptions<SecretProtectionOptions>().Configure(o => o.EncryptionKey = ShippedOptions().EncryptionKey);
        services.AddCommonSecretProtection();
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<SecretProtector>();
        var second = provider.GetRequiredService<SecretProtector>();

        Assert.Same(first, second);
        Assert.Equal("value", first.Decrypt(second.Encrypt("value")));
    }

    [Fact]
    public void ShippedConfiguration_IsAValidDevelopmentKey()
    {
        var options = ShippedOptions();

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ShippedConfiguration_IsRejectedInProduction()
    {
        var options = ShippedOptions();

        var result = new SecretProtectionOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.False(result.IsValid);
    }
}
