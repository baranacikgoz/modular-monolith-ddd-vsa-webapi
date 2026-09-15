using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Application.Auth;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Tests.Endpoints.Users;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.EmailScheme;

[Collection("EmailSchemeCollection")]
public class EmailSchemeTests(EmailSchemeWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Theory]
    [InlineData("/users/register/self")]
    [InlineData("/tokens")]
    [InlineData("/otp/login")]
    [InlineData("/otp/registration")]
    public async Task PhoneOnlyRoutes_EmailScheme_AreNotMapped(string route)
    {
        var client = Factory.CreateClient();

        using var response = await client.PostAsJsonAsync(new Uri(route, UriKind.Relative), new { });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task RegisterByEmail_ValidRequest_CreatesKeycloakUserWithBasicRoleAndSignsIn()
    {
        var email = IamTestClient.NewEmail();

        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, email, firstName: "Mehmet", lastName: "Kaya");
        var tokens = await IamTestClient.ReadTokensAsync(response);

        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await adminClient.FindUserByEmailAsync(email, CancellationToken.None);
        Assert.NotNull(user);
        Assert.Equal(email, user.Username);
        Assert.Equal("Mehmet", user.FirstName);
        Assert.Equal("Kaya", user.LastName);
        Assert.Null(user.PhoneNumber);
        Assert.Equal(new DateOnly(2001, 6, 20), user.BirthDate);
        Assert.Equal(user.Id.Value.ToString(), tokens.Subject);

        var me = await IamTestClient.Authorized(Factory, tokens)
            .GetFromJsonAsync<MeGetTests.MeResponse>(new Uri("/users/me", UriKind.Relative));
        Assert.NotNull(me);
        Assert.Contains(KeycloakRoles.Basic, me.Roles);
    }

    [Fact]
    public async Task RegisterByEmail_MixedCaseEmail_StoresAndLooksUpLowercased()
    {
        var email = IamTestClient.NewEmail();
        var mixedCase = email.ToUpperInvariant();
        // The verify step mints the token under the normalized address, so the register endpoint has to
        // normalize the incoming casing before it looks the token up.
        var token = await IamTestClient.SeedEmailVerificationTokenAsync(Factory, email, "email_verified_register");

        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, mixedCase, emailVerificationToken: token);

        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        Assert.NotNull(await adminClient.FindUserByEmailAsync(email, CancellationToken.None));
    }

    [Fact]
    public async Task RegisterByEmail_ThenLoginWithPasswordAndLoginToken_ReturnsTokens()
    {
        var email = IamTestClient.NewEmail();
        using var register = await IamTestClient.RegisterByEmailRawAsync(Factory, email);
        Assert.True(register.IsSuccessStatusCode, await register.Content.ReadAsStringAsync());

        var tokens = await IamTestClient.LoginByEmailAsync(Factory, email, IamTestClient.ValidPassword);

        Assert.False(string.IsNullOrWhiteSpace(tokens.SessionId));
    }

    /// <summary>Purpose binding: a token minted for an already-registered address (login) cannot register.</summary>
    [Fact]
    public async Task RegisterByEmail_LoginPurposeToken_Returns400()
    {
        var email = IamTestClient.NewEmail();
        var loginToken = await IamTestClient.SeedEmailVerificationTokenAsync(Factory, email, "email_verified_login");

        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, email, emailVerificationToken: loginToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterByEmail_SameEmailTwice_Returns409EmailAlreadyRegistered()
    {
        var email = IamTestClient.NewEmail();
        using var first = await IamTestClient.RegisterByEmailRawAsync(Factory, email);
        Assert.True(first.IsSuccessStatusCode, await first.Content.ReadAsStringAsync());

        using var second = await IamTestClient.RegisterByEmailRawAsync(Factory, email);

        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        using var doc = JsonDocument.Parse(await second.Content.ReadAsStringAsync());
        Assert.Equal("EmailAlreadyRegistered", doc.RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task RegisterByEmail_SeedStaffEmail_Returns409EmailAlreadyRegistered()
    {
        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, SeedUsers.StaffEmail);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("EmailAlreadyRegistered", doc.RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task RegisterByEmail_PasswordViolatesKeycloakPolicy_Returns400()
    {
        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, IamTestClient.NewEmail(), password: "short");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterByEmail_RoleAssignmentFails_RollsBackTheCreatedUser()
    {
        var email = IamTestClient.NewEmail();
        FaultInjectingKeycloakAdminClient.FailNextRoleAssignmentFor(email);

        using var response = await IamTestClient.RegisterByEmailRawAsync(Factory, email);

        Assert.False(response.IsSuccessStatusCode);
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        Assert.Null(await adminClient.FindUserByEmailAsync(email, CancellationToken.None));
    }

    [Theory]
    [InlineData("", "Yılmaz")]
    [InlineData("Ay5e", "Yılmaz")]
    [InlineData("Ayşe", "")]
    public async Task RegisterByEmail_InvalidNames_Returns400(string firstName, string lastName)
    {
        using var response = await IamTestClient.RegisterByEmailRawAsync(
            Factory, IamTestClient.NewEmail(), firstName: firstName, lastName: lastName);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
