using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class KeycloakOptionsValidatorTests
{
    private static ValidationContext<KeycloakOptions> BuildContext(KeycloakOptions options, string environmentName)
    {
        var context = new ValidationContext<KeycloakOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    private static KeycloakOptions Valid() => new()
    {
        BaseUrl = "http://localhost:8080",
        Realm = "modular-monolith",
        ResourceClientId = "backend-api",
        ResourceClientSecret = "secret",
        TrustedLoginClientId = "backend-trusted-login",
        TrustedLoginClientSecret = "secret",
        RequireHttpsMetadata = false,
        DecisionCacheMaxDurationSeconds = 300,
        ServiceAccountTokenRefreshSkewSeconds = 30,
        AttemptTimeoutSeconds = 5,
        TotalRequestTimeoutSeconds = 15
    };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new KeycloakOptionsValidator().Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Authority_IsRealmUrlWithoutTrailingSlash()
    {
        var options = Valid();
        options.BaseUrl = "http://localhost:8080/";

        Assert.Equal("http://localhost:8080/realms/modular-monolith", options.Authority);
    }

    [Theory]
    [InlineData("")]
    [InlineData("localhost:8080")]
    [InlineData("ftp://localhost")]
    public void BaseUrl_MustBeAbsoluteHttpUrl(string value)
    {
        var options = Valid();
        options.BaseUrl = value;

        var result = new KeycloakOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(KeycloakOptions.BaseUrl));
    }

    [Fact]
    public void TrustedLoginClient_MustDifferFromResourceClient()
    {
        var options = Valid();
        options.TrustedLoginClientId = options.ResourceClientId;

        var result = new KeycloakOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(KeycloakOptions.TrustedLoginClientId));
    }

    [Fact]
    public void TotalTimeout_MustCoverAttemptTimeout()
    {
        var options = Valid();
        options.AttemptTimeoutSeconds = 10;
        options.TotalRequestTimeoutSeconds = 5;

        var result = new KeycloakOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(KeycloakOptions.TotalRequestTimeoutSeconds));
    }

    [Fact]
    public void DecisionCacheMaxDuration_MustBePositive()
    {
        var options = Valid();
        options.DecisionCacheMaxDurationSeconds = 0;

        var result = new KeycloakOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(nameof(KeycloakOptions.ResourceClientSecret))]
    [InlineData(nameof(KeycloakOptions.TrustedLoginClientSecret))]
    public void ChangeMeSecret_OutsideDevelopment_IsInvalid(string property)
    {
        var options = Valid();
        if (property == nameof(KeycloakOptions.ResourceClientSecret))
        {
            options.ResourceClientSecret = "backend-api-dev-secret-change-me";
        }
        else
        {
            options.TrustedLoginClientSecret = "backend-trusted-login-dev-secret-change-me";
        }

        var result = new KeycloakOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == property);
    }

    [Theory]
    [InlineData(nameof(KeycloakOptions.ResourceClientSecret))]
    [InlineData(nameof(KeycloakOptions.TrustedLoginClientSecret))]
    public void ChangeMeSecret_InDevelopment_IsValid(string property)
    {
        var options = Valid();
        if (property == nameof(KeycloakOptions.ResourceClientSecret))
        {
            options.ResourceClientSecret = "backend-api-dev-secret-change-me";
        }
        else
        {
            options.TrustedLoginClientSecret = "backend-trusted-login-dev-secret-change-me";
        }

        var result = new KeycloakOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }
}
