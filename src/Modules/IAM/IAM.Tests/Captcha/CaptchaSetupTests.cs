using Common.Application.Options;
using Common.Tests;
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
    private static IConfiguration BuildConfiguration(string provider)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { $"{nameof(CaptchaOptions)}:Provider", provider },
                { $"{nameof(CaptchaOptions)}:BaseUrl", "https://www.google.com/recaptcha/api" },
                { $"{nameof(CaptchaOptions)}:CaptchaEndpoint", "siteverify" },
                { $"{nameof(CaptchaOptions)}:ClientKey", "clientKey" },
                { $"{nameof(CaptchaOptions)}:SecretKey", "secretKey" },
                { $"{nameof(CaptchaOptions)}:AttemptTimeoutSeconds", "5" },
                { $"{nameof(CaptchaOptions)}:TotalRequestTimeoutSeconds", "15" }
            })
            .Build();
    }

    private static ServiceCollection BuildServices(string environmentName)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IHostEnvironment>(new FakeHostEnvironment(environmentName));
        return services;
    }

    [Fact]
    public void AddCaptchaInfrastructure_DummyProviderInProduction_Throws()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration("Dummy");

        Assert.Throws<InvalidOperationException>(() => services.AddCaptchaInfrastructure(configuration));
    }

    [Fact]
    public void AddCaptchaInfrastructure_DummyProviderInDevelopment_RegistersDummy()
    {
        var services = BuildServices(Environments.Development);
        var configuration = BuildConfiguration("Dummy");

        services.AddCaptchaInfrastructure(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(ICaptchaService) &&
            descriptor.ImplementationType == typeof(DummyCaptchaService));
    }

    [Fact]
    public void AddCaptchaInfrastructure_ReCaptchaProvider_RegistersTypedHttpClient()
    {
        var services = BuildServices(Environments.Development);
        var configuration = BuildConfiguration("ReCaptcha");

        services.AddCaptchaInfrastructure(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(ICaptchaService));
        Assert.DoesNotContain(services, descriptor => descriptor.ImplementationType == typeof(DummyCaptchaService));
    }
}
