using System.Net;
using System.Net.Http.Json;
using Common.Application.Auth;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class MeGetTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    internal sealed record MeResponse(
        Guid Id, string Username, string? FirstName, string? LastName, string? Email, string? PhoneNumber,
        DateOnly? BirthDate, DateTimeOffset CreatedOn, List<string> Roles, List<string> Permissions);

    [Fact]
    public async Task GetMe_BasicUser_ReturnsProfileRolesAndEffectivePermissions()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        var me = await IamTestClient.Authorized(Factory, tokens)
            .GetFromJsonAsync<MeResponse>(new Uri("/users/me", UriKind.Relative));

        Assert.NotNull(me);
        Assert.Equal(tokens.Subject, me.Id.ToString());
        Assert.Equal(SeedUsers.BasicPhone, me.Username);
        Assert.Equal(SeedUsers.BasicFirstName, me.FirstName);
        Assert.Equal(SeedUsers.BasicPhone, me.PhoneNumber);
        Assert.Equal([KeycloakRoles.Basic], me.Roles);
        Assert.Contains(KeycloakScopes.Stores.CreateOwn, me.Permissions);
        Assert.Contains(KeycloakScopes.Stores.View, me.Permissions);
        Assert.DoesNotContain(KeycloakScopes.Users.Search, me.Permissions);
        Assert.DoesNotContain(KeycloakScopes.Stores.Create, me.Permissions);
    }

    [Fact]
    public async Task GetMe_SystemAdmin_HoldsStaffRoleThroughComposite()
    {
        var tokens = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.AdminEmail, SeedUsers.AdminPassword);

        var me = await IamTestClient.Authorized(Factory, tokens)
            .GetFromJsonAsync<MeResponse>(new Uri("/users/me", UriKind.Relative));

        Assert.NotNull(me);
        Assert.Contains(KeycloakRoles.SystemAdmin, me.Roles);
        Assert.Contains(KeycloakRoles.Staff, me.Roles);
        Assert.Contains(KeycloakScopes.Hangfire.Manage, me.Permissions);
        Assert.Contains(KeycloakScopes.Users.Search, me.Permissions);
    }

    [Fact]
    public async Task GetMe_Anonymous_Returns401()
    {
        using var response = await Factory.CreateClient().GetAsync(new Uri("/users/me", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetMe_TamperedToken_Returns401()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var tampered = tokens.AccessToken[..^4] + "AAAA";

        using var response = await IamTestClient.Authorized(Factory, tampered).GetAsync(new Uri("/users/me", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
