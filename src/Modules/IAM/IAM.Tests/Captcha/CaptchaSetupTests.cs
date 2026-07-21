using Common.Application.Options;
using Common.Application.Validation;
using Common.Tests;
using FluentValidation;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace IAM.Tests.Captcha;

public class CaptchaSetupTests
{
    private static IConfiguration BuildConfiguration(CaptchaProvider provider)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { $"{nameof(CaptchaOptions)}:Provider", provider.ToString() },
                { $"{nameof(CaptchaOptions)}:BaseUrl", "https://www.google.com/recaptcha/api" },
                { $"{nameof(CaptchaOptions)}:CaptchaEndpoint", "siteverify" },
                { $"{nameof(CaptchaOptions)}:ClientKey", "clientKey" },
                { $"{nameof(CaptchaOptions)}:SecretKey", "secretKey" },
                { $"{nameof(CaptchaOptions)}:AttemptTimeoutSeconds", "5" },
                { $"{nameof(CaptchaOptions)}:TotalRequestTimeoutSeconds", "15" }
            })
            .Build();
    }

    private static CaptchaOptions BuildOptions(CaptchaProvider provider) => new()
    {
        Provider = provider,
        BaseUrl = "https://www.google.com/recaptcha/api",
        CaptchaEndpoint = "siteverify",
        ClientKey = "clientKey",
        SecretKey = "secretKey",
        AttemptTimeoutSeconds = 5,
        TotalRequestTimeoutSeconds = 15,
    };

    private static ValidationContext<CaptchaOptions> BuildContext(CaptchaOptions options, string environmentName)
    {
        var context = new ValidationContext<CaptchaOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void Validate_DummyProviderInProduction_Invalid()
    {
        var result = new CaptchaOptionsValidator().Validate(BuildContext(BuildOptions(CaptchaProvider.Dummy), Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Dummy", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_DummyProviderInDevelopment_Valid()
    {
        var result = new CaptchaOptionsValidator().Validate(BuildContext(BuildOptions(CaptchaProvider.Dummy), Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void AddCaptchaInfrastructure_DummyProvider_RegistersDummy()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(CaptchaProvider.Dummy);

        services.AddCaptchaInfrastructure(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(ICaptchaService) &&
            descriptor.ImplementationType == typeof(DummyCaptchaService));
    }

    [Fact]
    public void AddCaptchaInfrastructure_ReCaptchaProvider_RegistersTypedHttpClient()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(CaptchaProvider.ReCaptcha);

        services.AddCaptchaInfrastructure(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(ICaptchaService));
        Assert.DoesNotContain(services, descriptor => descriptor.ImplementationType == typeof(DummyCaptchaService));
    }
}
