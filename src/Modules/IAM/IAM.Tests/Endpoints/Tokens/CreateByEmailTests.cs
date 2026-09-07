using System.Net;
using Common.Application.Auth;
using Common.Tests;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class CreateByEmailTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateTokensByEmail_ValidPassword_ReturnsTokensWithStaffRole()
    {
        var tokens = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        var jwt = new JsonWebToken(tokens.AccessToken);
        Assert.Contains(KeycloakRoles.Staff, jwt.Claims.Where(c => c.Type == JwtClaimNames.Roles).Select(c => c.Value));
        Assert.False(string.IsNullOrWhiteSpace(tokens.SessionId));
    }

    [Fact]
    public async Task CreateTokensByEmail_WrongPassword_Returns401()
    {
        using var response = await IamTestClient.LoginByEmailRawAsync(Factory, SeedUsers.StaffEmail, "definitely-wrong");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTokensByEmail_UnknownEmail_Returns401()
    {
        using var response = await IamTestClient.LoginByEmailRawAsync(Factory, "nobody@modular-monolith.local", "whatever");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTokensByEmail_MalformedEmail_Returns400()
    {
        using var response = await IamTestClient.LoginByEmailRawAsync(Factory, "not-an-email", "whatever");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
