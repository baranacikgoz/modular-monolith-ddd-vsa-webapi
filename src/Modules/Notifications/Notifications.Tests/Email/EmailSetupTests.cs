using Common.Application.Options;
using Common.Application.Validation;
using Common.Tests;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Email;
using Notifications.Infrastructure.Email;
using Xunit;

namespace Notifications.Tests.Email;

public sealed class EmailSetupTests
{
    private static ValidationContext<EmailOptions> BuildContext(EmailOptions options, string environmentName)
    {
        var context = new ValidationContext<EmailOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    private static EmailOptions ValidOptions(EmailProvider provider) => new()
    {
        Provider = provider,
        BaseUrl = "https://api.brevo.com",
        ApiKey = "key",
        SenderEmail = "no-reply@example.com",
        AttemptTimeoutSeconds = 4,
        TotalRequestTimeoutSeconds = 8,
        MaxRetryAttempts = 1,
        MaxPerAddressPerDay = 10,
        MaxPerDay = 5000,
        ThrottleCounterTtlHours = 25,
        Templates = new EmailTemplatesOptions
        {
            Otp = { ["en"] = new EmailTemplate { Subject = "code {0}", HtmlBody = "<p>{0}</p>" } },
        },
    };

    [Fact]
    public void Validate_DummyProviderInProduction_Invalid()
    {
        var options = ValidOptions(EmailProvider.Dummy);

        var result = new EmailOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_DummyProviderInDevelopment_Valid()
    {
        var options = ValidOptions(EmailProvider.Dummy);

        var result = new EmailOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_BrevoMissingCredentials_Invalid()
    {
        var options = new EmailOptions
        {
            Provider = EmailProvider.Brevo,
            ThrottleCounterTtlHours = 25,
            Templates = new EmailTemplatesOptions
            {
                Otp = { ["en"] = new EmailTemplate { Subject = "code {0}", HtmlBody = "<p>{0}</p>" } },
            },
        };

        var result = new EmailOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_BrevoWithCredentials_Valid()
    {
        var options = ValidOptions(EmailProvider.Brevo);

        var result = new EmailOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.True(result.IsValid);
    }

    private static IConfiguration BuildConfiguration(EmailProvider provider, string? defaultCulture = null,
        params string[] templateLanguages)
    {
        var values = new Dictionary<string, string?>
        {
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.Provider)}"] = provider.ToString(),
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.BaseUrl)}"] = "https://api.brevo.com",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.ApiKey)}"] = "key",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.SenderEmail)}"] = "no-reply@example.com",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.AttemptTimeoutSeconds)}"] = "4",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.TotalRequestTimeoutSeconds)}"] = "8",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.MaxRetryAttempts)}"] = "1",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.MaxPerAddressPerDay)}"] = "10",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.MaxPerDay)}"] = "5000",
                [$"{nameof(EmailOptions)}:{nameof(EmailOptions.ThrottleCounterTtlHours)}"] = "25",
        };
        if (defaultCulture is not null)
        {
            values[$"{nameof(ResxLocalizationOptions)}:{nameof(ResxLocalizationOptions.DefaultCulture)}"] = defaultCulture;
        }

        foreach (var language in templateLanguages)
        {
            values[$"{nameof(EmailOptions)}:Templates:Otp:{language}:Subject"] = "code {0}";
            values[$"{nameof(EmailOptions)}:Templates:Otp:{language}:HtmlBody"] = "<p>{0}</p>";
        }

        return new ConfigurationBuilder().AddInMemoryCollection(values).Build();
    }

    [Fact]
    public void AddEmailServices_DummyProvider_RegistersDummyGateway()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(EmailProvider.Dummy);

        services.AddEmailServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IEmailGateway) && descriptor.ImplementationType == typeof(DummyEmailGateway));
    }

    [Fact]
    public void AddEmailServices_BrevoProvider_RegistersGatewayBehindThrottle()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(EmailProvider.Brevo);

        services.AddEmailServices(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(IEmailGateway));
        Assert.DoesNotContain(services, descriptor => descriptor.ImplementationType == typeof(DummyEmailGateway));
    }

    /// <summary>
    ///     A singleton gateway would pin one typed HttpClient for the process lifetime and defeat
    ///     HttpClientFactory's handler rotation; the throttle is stateless, so it must resolve per use.
    /// </summary>
    [Fact]
    public void AddEmailServices_BrevoProvider_GatewayIsNotSingleton()
    {
        var services = new ServiceCollection();

        services.AddEmailServices(BuildConfiguration(EmailProvider.Brevo));

        var descriptor = Assert.Single(services, d => d.ServiceType == typeof(IEmailGateway));
        Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
    }

    [Fact]
    public void AddEmailServices_DefaultCultureHasNoOtpTemplate_ThrowsAtRegistration()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(EmailProvider.Dummy, defaultCulture: "tr", "en");

        var ex = Assert.Throws<InvalidOperationException>(() => services.AddEmailServices(configuration));

        Assert.Contains("'tr'", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddEmailServices_DefaultCultureHasOtpTemplate_Registers()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(EmailProvider.Dummy, defaultCulture: "tr", "en", "tr");

        services.AddEmailServices(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(IEmailGateway));
    }
}
