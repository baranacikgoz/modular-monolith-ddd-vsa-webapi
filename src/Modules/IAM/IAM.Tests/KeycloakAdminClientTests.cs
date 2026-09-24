using System.Net;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Infrastructure.InterModuleRequestHandlers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     <see cref="IKeycloakAdminClient.CreateUserAsync" /> against the real Keycloak Testcontainer, exercising the
///     email + password path no endpoint uses yet (a fork wires it up, e.g. an admin-approval registration flow).
/// </summary>
[Collection("IntegrationTestCollection")]
public class KeycloakAdminClientTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateUserAsync_WithEmailAndPassword_CreatesUserThatCanLoginByEmail()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();
        var email = $"{Guid.NewGuid():N}@example.com";
        const string password = "A-Valid-Password-1";

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                BirthDate: new DateOnly(2001, 6, 20),
                Email: email,
                Password: password),
            CancellationToken.None);

        Assert.False(result.IsFailure);

        var tokens = await IamTestClient.LoginByEmailAsync(Factory, email, password);
        Assert.Equal(result.Value.ToString(), tokens.Subject);
    }

    [Fact]
    public async Task CreateUserAsync_PasswordViolatesPolicy_ReturnsRejectedUser()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                BirthDate: new DateOnly(2001, 6, 20),
                Email: $"{Guid.NewGuid():N}@example.com",
                Password: "short"),
            CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
    }

    [Fact]
    public async Task CreateUserAsync_NoBirthDate_CreatesUserWithNullBirthDate()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                BirthDate: null,
                Email: $"{Guid.NewGuid():N}@example.com"),
            CancellationToken.None);

        Assert.False(result.IsFailure);

        var user = await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None);
        Assert.NotNull(user);
        Assert.Null(user.BirthDate);
    }

    private static async Task<(ApplicationUserId Id, string Email)> CreateStaffUserAsync(IKeycloakAdminClient adminClient)
    {
        var phone = IamTestClient.NewPhoneNumber();
        var email = $"{Guid.NewGuid():N}@example.com";
        var created = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(phone, "Ayşe", "Yılmaz", phone, null, email), CancellationToken.None);
        Assert.False(created.IsFailure);
        var assigned = await adminClient.AssignRealmRoleAsync(created.Value, KeycloakRoles.Staff, CancellationToken.None);
        Assert.False(assigned.IsFailure);
        return (created.Value, email);
    }

    [Fact]
    public async Task GetUsersInRoleAsync_ReturnsRoleMembersWithTheirEmail_AndPagesThroughThem()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var first = await CreateStaffUserAsync(adminClient);
        var second = await CreateStaffUserAsync(adminClient);

        var all = await adminClient.GetUsersInRoleAsync(KeycloakRoles.Staff, 0, 200, CancellationToken.None);
        Assert.Equal(first.Email, all.Single(u => u.Id == first.Id).Email);
        Assert.Equal(second.Email, all.Single(u => u.Id == second.Id).Email);

        var pageOne = await adminClient.GetUsersInRoleAsync(KeycloakRoles.Staff, 0, 1, CancellationToken.None);
        var pageTwo = await adminClient.GetUsersInRoleAsync(KeycloakRoles.Staff, 1, 1, CancellationToken.None);
        Assert.Single(pageOne);
        Assert.Single(pageTwo);
        Assert.NotEqual(pageOne[0].Id, pageTwo[0].Id);
    }

    [Fact]
    public async Task GetUsersInRoleAsync_UnknownRole_ReturnsAnEmptyPage()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();

        var users = await adminClient.GetUsersInRoleAsync($"no-such-role-{Guid.NewGuid():N}", 0, 10, CancellationToken.None);

        Assert.Empty(users);
    }

    [Fact]
    public async Task GetUsersInRolePageHandler_ReturnsSummariesAndSaysWhetherAnotherPageExists()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await CreateStaffUserAsync(adminClient);
        await CreateStaffUserAsync(adminClient);
        var handler = new GetUsersInRolePageRequestHandler(adminClient);

        var firstPage = await handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 1), CancellationToken.None);
        var everyone = await handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 200), CancellationToken.None);

        Assert.Single(firstPage.Users);
        Assert.True(firstPage.HasMore);
        Assert.False(everyone.HasMore);
        Assert.Contains(everyone.Users, u => u.Id == user.Id && u.Email == user.Email);
    }
}

#pragma warning restore CA1707
