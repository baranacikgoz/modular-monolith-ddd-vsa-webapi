using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using IAM.Endpoints.Tokens.VersionNeutral.Create;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class CreateTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateTokens_WithValidOtp_ReturnsKeycloakTokensBoundToSession()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        Assert.False(string.IsNullOrWhiteSpace(tokens.AccessToken));
        Assert.False(string.IsNullOrWhiteSpace(tokens.RefreshToken));
        Assert.False(string.IsNullOrWhiteSpace(tokens.SessionId));
        Assert.True(tokens.AccessTokenExpiresAt > DateTimeOffset.UtcNow);
        Assert.True(tokens.RefreshTokenExpiresAt > tokens.AccessTokenExpiresAt);
    }

    [Fact]
    public async Task CreateTokens_WithWrongOtp_Returns400()
    {
        using var response = await IamTestClient.LoginByPhoneRawAsync(Factory, SeedUsers.BasicPhone, otp: "000000");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateTokens_UnknownPhoneWithValidOtp_Returns401()
    {
        // OTP delivery does not reveal registration state, so an unregistered number can still hold a valid
        // code; Keycloak then rejects the trusted login as invalid credentials.
        using var response = await IamTestClient.LoginByPhoneRawAsync(Factory, IamTestClient.NewPhoneNumber());

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTokens_UnknownClientId_Returns400()
    {
        using var response = await IamTestClient.LoginByPhoneRawAsync(Factory, SeedUsers.BasicPhone, clientId: "not-allowed");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateTokens_WithoutOtp_Returns400()
    {
        var client = Factory.CreateClient();

        using var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), new Request
        {
            PhoneNumber = SeedUsers.BasicPhone, Otp = string.Empty, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
