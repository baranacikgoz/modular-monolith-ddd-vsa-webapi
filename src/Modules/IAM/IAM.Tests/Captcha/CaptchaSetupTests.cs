using Common.Application.Options;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                { $"{nameof(CaptchaOptions)}:SecretKey", "secretKey" }
            })
            .Build();
    }

    [Fact]
    public void AddCaptchaInfrastructure_DummyProviderInProduction_Throws()
    {
        var previousEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
        try
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration("Dummy");

            Assert.Throws<InvalidOperationException>(() => services.AddCaptchaInfrastructure(configuration));
        }
        finally
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previousEnvironment);
        }
    }

    [Fact]
    public void AddCaptchaInfrastructure_DummyProviderInDevelopment_RegistersDummy()
    {
        var previousEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        try
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration("Dummy");

            services.AddCaptchaInfrastructure(configuration);

            Assert.Contains(services, descriptor =>
                descriptor.ServiceType == typeof(ICaptchaService) &&
                descriptor.ImplementationType == typeof(DummyCaptchaService));
        }
        finally
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previousEnvironment);
        }
    }

    [Fact]
    public void AddCaptchaInfrastructure_ReCaptchaProvider_RegistersTypedHttpClient()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration("ReCaptcha");

        services.AddCaptchaInfrastructure(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(ICaptchaService));
        Assert.DoesNotContain(services, descriptor => descriptor.ImplementationType == typeof(DummyCaptchaService));
    }
}
