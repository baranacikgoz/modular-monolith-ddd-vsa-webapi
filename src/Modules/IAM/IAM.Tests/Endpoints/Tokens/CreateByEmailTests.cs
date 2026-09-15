using System.Net;
using System.Net.Http.Json;
using Common.Application.Auth;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;
using CreateByEmailRequest = IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail.Request;

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

    [Fact]
    public async Task CreateTokensByEmail_EmptyEmailVerificationToken_Returns400()
    {
        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(new Uri("/tokens/email", UriKind.Relative), new CreateByEmailRequest
        {
            Email = SeedUsers.StaffEmail,
            Password = SeedUsers.StaffPassword,
            EmailVerificationToken = string.Empty,
            DeviceId = Guid.NewGuid(),
            ClientId = "web-app-1"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>Security property the whole design rests on: purpose binding, not the token's content, stops replay.</summary>
    [Fact]
    public async Task CreateTokensByEmail_RegisterPurposeTokenReplayed_Returns400()
    {
        var token = await IamTestClient.SeedEmailVerificationTokenAsync(Factory, SeedUsers.StaffEmail, "email_verified_register");

        using var response = await IamTestClient.LoginByEmailRawAsync(
            Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword, emailVerificationToken: token);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    ///     The password grant runs before the token check, so Keycloak has already opened a session by the time a
    ///     bad token is rejected. That session must be revoked in the same request, or every wrong-token attempt
    ///     would leave a live refresh-token-bearing session behind.
    /// </summary>
    [Fact]
    public async Task CreateTokensByEmail_WrongToken_Returns400AndLeavesNoKeycloakSessionBehind()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var staff = await adminClient.FindUserByEmailAsync(SeedUsers.StaffEmail, CancellationToken.None);
        Assert.NotNull(staff);
        var sessionsBefore = (await adminClient.GetUserSessionsAsync(staff.Id, CancellationToken.None)).Count;

        using var response = await IamTestClient.LoginByEmailRawAsync(
            Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword, emailVerificationToken: "definitely-not-a-token");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var sessionsAfter = (await adminClient.GetUserSessionsAsync(staff.Id, CancellationToken.None)).Count;
        Assert.Equal(sessionsBefore, sessionsAfter);
    }

    /// <summary>
    ///     Proves the CreateByEmail pipeline checks the password before consuming the single-use token
    ///     (IAM.Endpoints/Tokens/VersionNeutral/CreateByEmail/Endpoint.cs): a wrong-password retry must not
    ///     burn the token, or the caller would be forced back to a fresh /otp/email round trip.
    /// </summary>
    [Fact]
    public async Task CreateTokensByEmail_WrongPasswordThenCorrectPassword_SameTokenStillWorks()
    {
        var token = await IamTestClient.SeedEmailVerificationTokenAsync(Factory, SeedUsers.StaffEmail);

        using var wrongPassword = await IamTestClient.LoginByEmailRawAsync(
            Factory, SeedUsers.StaffEmail, "definitely-wrong", emailVerificationToken: token);
        Assert.Equal(HttpStatusCode.Unauthorized, wrongPassword.StatusCode);

        using var correctPassword = await IamTestClient.LoginByEmailRawAsync(
            Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword, emailVerificationToken: token);
        Assert.True(correctPassword.IsSuccessStatusCode);
    }
}
