using System.Net;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Infrastructure.InterModuleRequestHandlers;
using IAM.Infrastructure.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

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
        Assert.NotEmpty(result.Error.SubErrors!);
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

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public async Task CreateUserAsync_WithAPhoneNumber_RecordsWhetherTheCallerVerifiedIt(bool verified, string expectedAttribute)
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                PhoneNumberVerified: verified),
            CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(expectedAttribute, await KeycloakUserAttributeReader.PhoneNumberVerifiedAsync(factory, Scope.ServiceProvider, result.Value));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CreateUserAsync_WithAnEmail_RecordsWhetherTheCallerVerifiedIt(bool verified)
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                Email: $"{Guid.NewGuid():N}@example.com",
                EmailVerified: verified),
            CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(verified, await KeycloakUserAttributeReader.EmailVerifiedAsync(factory, Scope.ServiceProvider, result.Value));
    }

    [Fact]
    public async Task CreateUserAsync_WithAnEmailAndNoFlag_LeavesTheEmailUnverified()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(phone, "Ayşe", "Yılmaz", phone, Email: $"{Guid.NewGuid():N}@example.com"),
            CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.False(await KeycloakUserAttributeReader.EmailVerifiedAsync(factory, Scope.ServiceProvider, result.Value));
    }

    private static async Task<KeycloakUser> ReadUserAsync(IKeycloakAdminClient adminClient, ApplicationUserId id)
    {
        var result = await adminClient.GetUserAsync(id, CancellationToken.None);
        Assert.False(result.IsFailure);
        return result.Value!;
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
    public async Task AssignRealmRoleAsync_UnknownUser_ReturnsNotFound()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();

        var result = await adminClient.AssignRealmRoleAsync(
            new ApplicationUserId(Guid.NewGuid()), KeycloakRoles.StoreOwner, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
    }

    [Fact]
    public async Task DeleteUserAsync_EvictsTheCachedDisplayEntry()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var user = await CreateStaffUserAsync(adminClient);
        var key = CacheKeys.For.UserDisplay(user.Id);
        await cache.SetAsync(key, new UserDisplayCacheEntry(true, user.Email, "Ada", "Lovelace", null));

        await adminClient.DeleteUserAsync(user.Id, CancellationToken.None);

        var cached = await cache.TryGetAsync<UserDisplayCacheEntry>(key);
        Assert.False(cached.HasValue);
    }

    [Fact]
    public async Task UpdateUserAsync_ChangesTheNameAndAnAttribute_KeepsEverythingElse()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await CreateStaffUserAsync(adminClient);
        var before = await ReadUserAsync(adminClient, user.Id);

        var result = await adminClient.UpdateUserAsync(
            user.Id,
            new UpdateKeycloakUser(
                FirstName: "Zeynep",
                Attributes: new Dictionary<string, string?> { [UserAttributes.PhoneNumberVerified] = "true" }),
            CancellationToken.None);

        Assert.False(result.IsFailure);
        var after = await ReadUserAsync(adminClient, user.Id);
        Assert.Equal("Zeynep", after.FirstName);
        Assert.Equal(before.LastName, after.LastName);
        Assert.Equal(before.Email, after.Email);
        Assert.Equal(before.PhoneNumber, after.PhoneNumber);
        Assert.Equal(before.Enabled, after.Enabled);
        Assert.Equal("true", await KeycloakUserAttributeReader.PhoneNumberVerifiedAsync(factory, Scope.ServiceProvider, user.Id));
    }

    [Fact]
    public async Task UpdateUserAsync_AttributeMappedToNull_RemovesIt()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();
        var created = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(phone, "Ayşe", "Yılmaz", phone, new DateOnly(1990, 5, 1)), CancellationToken.None);
        Assert.False(created.IsFailure);

        var result = await adminClient.UpdateUserAsync(
            created.Value,
            new UpdateKeycloakUser(Attributes: new Dictionary<string, string?> { [UserAttributes.BirthDate] = null }),
            CancellationToken.None);

        Assert.False(result.IsFailure);
        var after = await ReadUserAsync(adminClient, created.Value);
        Assert.Null(after.BirthDate);
        Assert.Equal("Ayşe", after.FirstName);
        Assert.Equal(phone, after.PhoneNumber);
    }

    [Fact]
    public async Task UpdateUserAsync_UnknownUser_ReturnsNotFound()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();

        var result = await adminClient.UpdateUserAsync(
            new ApplicationUserId(Guid.NewGuid()), new UpdateKeycloakUser(FirstName: "Zeynep"), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
    }

    [Fact]
    public async Task UpdateUserAsync_ValueTheUserProfileRejects_ReturnsTheRejectedField()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await CreateStaffUserAsync(adminClient);

        var result = await adminClient.UpdateUserAsync(
            user.Id,
            new UpdateKeycloakUser(Attributes: new Dictionary<string, string?> { [UserAttributes.PhoneNumber] = "not-a-phone" }),
            CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error!.StatusCode);
        Assert.Equal(UserAttributes.PhoneNumber, result.Error.ParameterName);
    }

    [Fact]
    public async Task UpdateUserAsync_EvictsTheCachedDisplayEntry()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var user = await CreateStaffUserAsync(adminClient);
        var key = CacheKeys.For.UserDisplay(user.Id);
        await cache.SetAsync(key, new UserDisplayCacheEntry(true, user.Email, "Ada", "Lovelace", null));

        await adminClient.UpdateUserAsync(user.Id, new UpdateKeycloakUser(FirstName: "Zeynep"), CancellationToken.None);

        var cached = await cache.TryGetAsync<UserDisplayCacheEntry>(key);
        Assert.False(cached.HasValue);
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
        // The handler keeps a role's members for a window, so a list another test built earlier would hide the users above.
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        await cache.RemoveAsync(CacheKeys.For.KeycloakRoleMembers(KeycloakRoles.Staff));
        var handler = new GetUsersInRolePageRequestHandler(adminClient, cache, Scope.ServiceProvider.GetRequiredService<IOptions<KeycloakOptions>>());

        var firstPage = await handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 1), CancellationToken.None);
        var everyone = await handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 200), CancellationToken.None);

        Assert.Single(firstPage.Users);
        Assert.True(firstPage.HasMore);
        Assert.False(everyone.HasMore);
        Assert.Contains(everyone.Users, u => u.Id == user.Id && u.Email == user.Email);
    }
}

#pragma warning restore CA1707
