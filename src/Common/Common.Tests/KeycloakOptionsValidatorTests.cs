using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class KeycloakOptionsValidatorTests
{
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
}
