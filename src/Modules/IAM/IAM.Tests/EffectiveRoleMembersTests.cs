using Common.Application.Auth;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Infrastructure.InterModuleRequestHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     A realm role's effective members against the real Keycloak Testcontainer: its direct members plus the members of
///     every realm role that includes it, directly or through other composite roles. Each test builds its own uniquely
///     named roles, so the shared realm's other members never leak into the assertions.
/// </summary>
[Collection("IntegrationTestCollection")]
public class EffectiveRoleMembersTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IKeycloakAdminClient AdminClient => Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();

    private GetUsersInRolePageRequestHandler Handler => new(AdminClient);

    private Task<KeycloakRealmAdmin> ConnectRealmAsync()
    {
        return KeycloakRealmAdmin.ConnectAsync(Scope.ServiceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value.Realm);
    }

    /// <summary>Composes the roles in Keycloak and drops the cached role graph, which would otherwise hide the change for its window.</summary>
    private async Task AddCompositeAsync(KeycloakRealmAdmin realm, string parent, string child)
    {
        await realm.AddCompositeAsync(parent, child);
        await Scope.ServiceProvider.GetRequiredService<IFusionCache>().RemoveAsync(CacheKeys.For.KeycloakRoleGraph);
    }

    private static string NewRoleName(string prefix) => $"{prefix}-{Guid.NewGuid():N}";

    private async Task<ApplicationUserId> CreateUserWithRolesAsync(params string[] roles)
    {
        var phone = IamTestClient.NewPhoneNumber();
        var created = await AdminClient.CreateUserAsync(
            new CreateKeycloakUser(phone, "Ayşe", "Yılmaz", phone, null, $"{Guid.NewGuid():N}@example.com"), CancellationToken.None);
        Assert.False(created.IsFailure);

        foreach (var role in roles)
        {
            var assigned = await AdminClient.AssignRealmRoleAsync(created.Value, role, CancellationToken.None);
            Assert.False(assigned.IsFailure);
        }

        return created.Value;
    }

    private async Task<List<ApplicationUserId>> ReadEveryPageAsync(string role, int pageSize)
    {
        var ids = new List<ApplicationUserId>();
        var skip = 0;
        while (true)
        {
            var page = await Handler.HandleAsync(new GetUsersInRolePageRequest(role, skip, pageSize), CancellationToken.None);
            ids.AddRange(page.Users.Select(u => u.Id));
            if (!page.HasMore)
            {
                return ids;
            }

            skip += pageSize;
        }
    }

    [Fact]
    public async Task GetRolesContainingAsync_RoleIncludedByACompositeRole_ReturnsTheComposite()
    {
        var roles = await AdminClient.GetRolesContainingAsync(KeycloakRoles.Staff, CancellationToken.None);

        Assert.Contains(KeycloakRoles.SystemAdmin, roles);
        Assert.DoesNotContain(KeycloakRoles.Staff, roles);
    }

    [Fact]
    public async Task GetRolesContainingAsync_RoleNothingIncludes_ReturnsNothing()
    {
        var roles = await AdminClient.GetRolesContainingAsync(KeycloakRoles.SystemAdmin, CancellationToken.None);

        Assert.Empty(roles);
    }

    [Fact]
    public async Task GetRolesContainingAsync_UnknownRole_ReturnsNothing()
    {
        var roles = await AdminClient.GetRolesContainingAsync(NewRoleName("no-such-role"), CancellationToken.None);

        Assert.Empty(roles);
    }

    [Fact]
    public async Task GetRolesContainingAsync_ChainOfComposites_ReturnsEveryAncestorOnce()
    {
        using var realm = await ConnectRealmAsync();
        var leaf = NewRoleName("leaf");
        var middle = NewRoleName("middle");
        var top = NewRoleName("top");
        var sibling = NewRoleName("sibling");
        foreach (var role in new[] { leaf, middle, top, sibling })
        {
            await realm.CreateRoleAsync(role);
        }

        await AddCompositeAsync(realm, middle, leaf);
        await AddCompositeAsync(realm, top, middle);
        await AddCompositeAsync(realm, top, leaf);
        await AddCompositeAsync(realm, sibling, top);

        var roles = await AdminClient.GetRolesContainingAsync(leaf, CancellationToken.None);

        Assert.Equal(new[] { middle, sibling, top }.Order(StringComparer.Ordinal), roles.Order(StringComparer.Ordinal));
    }

    [Fact]
    public async Task GetUsersInRolePage_UserHoldingOnlyACompositeThatIncludesTheRole_IsListed()
    {
        var holder = await CreateUserWithRolesAsync(KeycloakRoles.SystemAdmin);

        var page = await Handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 1000), CancellationToken.None);

        var summary = Assert.Single(page.Users, u => u.Id == holder);
        Assert.False(string.IsNullOrEmpty(summary.Email));
    }

    [Fact]
    public async Task GetUsersInRolePage_UserHoldingTheRoleAndTheCompositeToo_IsListedOnce()
    {
        var both = await CreateUserWithRolesAsync(KeycloakRoles.Staff, KeycloakRoles.SystemAdmin);

        var page = await Handler.HandleAsync(new GetUsersInRolePageRequest(KeycloakRoles.Staff, 0, 1000), CancellationToken.None);

        Assert.Single(page.Users, u => u.Id == both);
    }

    [Fact]
    public async Task GetUsersInRolePage_RoleReachedThroughSeveralComposites_ListsEveryHolderOnce()
    {
        using var realm = await ConnectRealmAsync();
        var leaf = NewRoleName("leaf");
        var middle = NewRoleName("middle");
        var top = NewRoleName("top");
        foreach (var role in new[] { leaf, middle, top })
        {
            await realm.CreateRoleAsync(role);
        }

        await AddCompositeAsync(realm, middle, leaf);
        await AddCompositeAsync(realm, top, middle);
        var direct = await CreateUserWithRolesAsync(leaf);
        var viaMiddle = await CreateUserWithRolesAsync(middle);
        var viaTop = await CreateUserWithRolesAsync(top);
        var unrelated = await CreateUserWithRolesAsync(KeycloakRoles.Basic);

        var ids = await ReadEveryPageAsync(leaf, pageSize: 100);

        Assert.Equal(new[] { direct, viaMiddle, viaTop }.OrderBy(i => i.Value), ids.OrderBy(i => i.Value));
        Assert.DoesNotContain(unrelated, ids);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(100)]
    public async Task GetUsersInRolePage_AnyPageSize_WalksTheSameDeduplicatedSequence(int pageSize)
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        var parent = NewRoleName("parent");
        await realm.CreateRoleAsync(role);
        await realm.CreateRoleAsync(parent);
        await AddCompositeAsync(realm, parent, role);
        var directOnly = new[] { await CreateUserWithRolesAsync(role), await CreateUserWithRolesAsync(role) };
        var compositeOnly = new[] { await CreateUserWithRolesAsync(parent), await CreateUserWithRolesAsync(parent) };
        var both = await CreateUserWithRolesAsync(role, parent);
        var everyone = directOnly.Concat(compositeOnly).Append(both).ToList();

        var walked = await ReadEveryPageAsync(role, pageSize);
        var walkedAgain = await ReadEveryPageAsync(role, pageSize);
        var reference = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal(everyone.Count, walked.Count);
        Assert.Equal(everyone.OrderBy(i => i.Value), walked.OrderBy(i => i.Value));
        Assert.Equal(walked, walkedAgain);
        Assert.Equal(reference, walked);
    }

    [Fact]
    public async Task GetUsersInRolePage_HasMoreIsTrueOnlyWhileMembersRemainPastThePage()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        var parent = NewRoleName("parent");
        await realm.CreateRoleAsync(role);
        await realm.CreateRoleAsync(parent);
        await AddCompositeAsync(realm, parent, role);
        await CreateUserWithRolesAsync(role);
        await CreateUserWithRolesAsync(parent);
        await CreateUserWithRolesAsync(parent);

        var first = await Handler.HandleAsync(new GetUsersInRolePageRequest(role, 0, 2), CancellationToken.None);
        var last = await Handler.HandleAsync(new GetUsersInRolePageRequest(role, 2, 2), CancellationToken.None);
        var exact = await Handler.HandleAsync(new GetUsersInRolePageRequest(role, 0, 3), CancellationToken.None);

        Assert.Equal(2, first.Users.Count);
        Assert.True(first.HasMore);
        Assert.Single(last.Users);
        Assert.False(last.HasMore);
        Assert.Equal(3, exact.Users.Count);
        Assert.False(exact.HasMore);
    }

    [Fact]
    public async Task GetUsersInRolePage_DisabledHolderOfTheComposite_IsLeftOut()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        var parent = NewRoleName("parent");
        await realm.CreateRoleAsync(role);
        await realm.CreateRoleAsync(parent);
        await AddCompositeAsync(realm, parent, role);
        var enabled = await CreateUserWithRolesAsync(parent);
        var disabled = await CreateUserWithRolesAsync(parent);
        await realm.SetEnabledAsync(disabled, enabled: false);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Contains(enabled, ids);
        Assert.DoesNotContain(disabled, ids);
    }

    [Fact]
    public async Task GetUsersInRolePage_UserHoldingTheRoleOnlyThroughAGroup_IsListed()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var group = await realm.CreateGroupAsync(NewRoleName("team"));
        await realm.AddRealmRoleToGroupAsync(group, role);
        var member = await CreateUserWithRolesAsync();
        var outsider = await CreateUserWithRolesAsync();
        await realm.AddUserToGroupAsync(member, group);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal([member], ids);
        Assert.DoesNotContain(outsider, ids);
    }

    [Fact]
    public async Task GetUsersInRolePage_UserInASubgroupOfAGroupHoldingTheRole_IsListed()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var parent = await realm.CreateGroupAsync(NewRoleName("division"));
        var child = await realm.CreateGroupAsync(NewRoleName("team"), parent);
        var grandChild = await realm.CreateGroupAsync(NewRoleName("squad"), child);
        await realm.AddRealmRoleToGroupAsync(parent, role);
        var inParent = await CreateUserWithRolesAsync();
        var inChild = await CreateUserWithRolesAsync();
        var inGrandChild = await CreateUserWithRolesAsync();
        await realm.AddUserToGroupAsync(inParent, parent);
        await realm.AddUserToGroupAsync(inChild, child);
        await realm.AddUserToGroupAsync(inGrandChild, grandChild);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal(new[] { inParent, inChild, inGrandChild }.OrderBy(i => i.Value), ids.OrderBy(i => i.Value));
    }

    [Fact]
    public async Task GetUsersInRolePage_UserInAGroupHoldingACompositeThatIncludesTheRole_IsListed()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        var composite = NewRoleName("lead");
        await realm.CreateRoleAsync(role);
        await realm.CreateRoleAsync(composite);
        await AddCompositeAsync(realm, composite, role);
        var group = await realm.CreateGroupAsync(NewRoleName("leads"));
        await realm.AddRealmRoleToGroupAsync(group, composite);
        var member = await CreateUserWithRolesAsync();
        await realm.AddUserToGroupAsync(member, group);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal([member], ids);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public async Task GetUsersInRolePage_DirectHolderAndGroupMemberOverlap_WalksEachUserOnceInAFixedOrder(int pageSize)
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var group = await realm.CreateGroupAsync(NewRoleName("team"));
        await realm.AddRealmRoleToGroupAsync(group, role);
        var directOnly = await CreateUserWithRolesAsync(role);
        var groupOnly = await CreateUserWithRolesAsync();
        var both = await CreateUserWithRolesAsync(role);
        await realm.AddUserToGroupAsync(groupOnly, group);
        await realm.AddUserToGroupAsync(both, group);

        var walked = await ReadEveryPageAsync(role, pageSize);
        var walkedAgain = await ReadEveryPageAsync(role, pageSize);

        Assert.Equal(3, walked.Count);
        Assert.Equal(new[] { directOnly, groupOnly, both }.OrderBy(i => i.Value), walked.OrderBy(i => i.Value));
        Assert.Equal(walked, walkedAgain);
    }

    [Fact]
    public async Task GetUsersInRolePage_DisabledMemberOfAGroupHoldingTheRole_IsLeftOut()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var group = await realm.CreateGroupAsync(NewRoleName("team"));
        await realm.AddRealmRoleToGroupAsync(group, role);
        var enabled = await CreateUserWithRolesAsync();
        var disabled = await CreateUserWithRolesAsync();
        await realm.AddUserToGroupAsync(enabled, group);
        await realm.AddUserToGroupAsync(disabled, group);
        await realm.SetEnabledAsync(disabled, enabled: false);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal([enabled], ids);
    }

    [Fact]
    public async Task GetUsersInRolePage_MemberOfAGroupWithoutTheRole_IsNotListed()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var group = await realm.CreateGroupAsync(NewRoleName("team"));
        var holder = await CreateUserWithRolesAsync(role);
        var bystander = await CreateUserWithRolesAsync();
        await realm.AddUserToGroupAsync(bystander, group);

        var ids = await ReadEveryPageAsync(role, pageSize: 100);

        Assert.Equal([holder], ids);
    }

    [Fact]
    public async Task GetUsersInRolePage_RoleNoCompositeIncludes_StaysAPlainPassThrough()
    {
        using var realm = await ConnectRealmAsync();
        var role = NewRoleName("member");
        await realm.CreateRoleAsync(role);
        var first = await CreateUserWithRolesAsync(role);
        var second = await CreateUserWithRolesAsync(role);

        var ids = await ReadEveryPageAsync(role, pageSize: 1);

        Assert.Equal(new[] { first, second }.OrderBy(i => i.Value), ids.OrderBy(i => i.Value));
    }

    [Fact]
    public async Task GetUsersInRolePage_UnknownRole_IsAnEmptyPage()
    {
        var page = await Handler.HandleAsync(new GetUsersInRolePageRequest(NewRoleName("no-such-role"), 0, 10), CancellationToken.None);

        Assert.Empty(page.Users);
        Assert.False(page.HasMore);
    }
}

#pragma warning restore CA1707
