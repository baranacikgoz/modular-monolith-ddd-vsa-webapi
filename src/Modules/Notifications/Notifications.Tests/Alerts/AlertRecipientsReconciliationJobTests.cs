using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Notifications.Application.Persistence;
using Notifications.Infrastructure.Alerts;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.Alerts;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>The local list of alert email addresses follows IAM's role members through a paged reconcile, since IAM
/// publishes no event when someone gains or loses the role.</summary>
[Collection("IntegrationTestCollection")]
public sealed class AlertRecipientsReconciliationJobTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    private const string RoleName = "staff";

    private IInterModuleRequestClient<GetUsersInRolePageRequest, GetUsersInRolePageResponse> Client =>
        Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<GetUsersInRolePageRequest, GetUsersInRolePageResponse>>();

    private INotificationsDbContext Db => Scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();

    private async Task<int> RunAsync(TimeProvider timeProvider, int pageSize = 200, int maxPages = 100)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var job = new AlertRecipientsReconciliationJob(
            scope.ServiceProvider.GetRequiredService<INotificationsDbContext>(),
            Client,
            timeProvider,
            Options.Create(new AlertRecipientsOptions
            {
                RoleName = RoleName,
                ReconcileCron = "15 3 * * *",
                EmailSubjectTemplate = "{0}",
                EmailHtmlBodyTemplate = "{1}",
                EmailTextBodyTemplate = "{1}"
            }),
            Options.Create(new ProjectionReconciliationOptions { PageSize = pageSize, MaxPages = maxPages }),
            NullLogger<AlertRecipientsReconciliationJob>.Instance);

        return await job.ReconcileAsync(CancellationToken.None);
    }

    private static RoleUserSummary User(string? email) => new(ApplicationUserId.New(), email);

    private void ServeRoleMembers(params RoleUserSummary[] members)
    {
        Client.SendAsync(Arg.Any<GetUsersInRolePageRequest>(), Arg.Any<CancellationToken>())
            .Returns(call =>
            {
                var request = call.Arg<GetUsersInRolePageRequest>();
                Assert.Equal(RoleName, request.RoleName);
                var page = members.Skip(request.Skip).Take(request.Take).ToList();
                return new GetUsersInRolePageResponse(page, request.Skip + request.Take < members.Length);
            });
    }

    [Fact]
    public async Task Reconcile_MembersAcrossPages_AreUpsertedAndOnesWithoutAnEmailSkipped()
    {
        var members = new[] { User("a@example.com"), User(null), User("b@example.com"), User("c@example.com") };
        ServeRoleMembers(members);
        Client.ClearReceivedCalls();

        var applied = await RunAsync(new ManualTimeProvider(), pageSize: 1);

        Assert.Equal(3, applied);
        await Client.Received(4).SendAsync(Arg.Any<GetUsersInRolePageRequest>(), Arg.Any<CancellationToken>());
        var rows = await Db.AlertRecipients.AsNoTracking().ToListAsync();
        Assert.Equal(["a@example.com", "b@example.com", "c@example.com"], rows.Select(r => r.Email).Order());
    }

    [Fact]
    public async Task Reconcile_ChangedEmail_IsRefreshed_AndAUserWhoLostTheRoleIsRemoved()
    {
        var time = new ManualTimeProvider();
        var stays = User("old@example.com");
        var leaves = User("gone@example.com");
        ServeRoleMembers(stays, leaves);
        await RunAsync(time);

        time.Advance(TimeSpan.FromDays(1));
        ServeRoleMembers(stays with { Email = "new@example.com" });
        await RunAsync(time);

        var row = Assert.Single(await Db.AlertRecipients.AsNoTracking().ToListAsync());
        Assert.Equal(stays.Id, row.UserId);
        Assert.Equal("new@example.com", row.Email);
    }

    /// <summary>An empty role is more likely an identity-provider fault than the truth: keep the stale addresses, the safe side for an alert.</summary>
    [Fact]
    public async Task Reconcile_RoleReadsAsEmpty_KeepsTheExistingRows()
    {
        var time = new ManualTimeProvider();
        ServeRoleMembers(User("keep@example.com"));
        await RunAsync(time);

        time.Advance(TimeSpan.FromDays(1));
        ServeRoleMembers();
        var applied = await RunAsync(time);

        Assert.Equal(0, applied);
        Assert.Single(await Db.AlertRecipients.AsNoTracking().ToListAsync());
    }

    [Fact]
    public async Task Reconcile_RunThatCouldNotReadTheWholeRole_RemovesNothing()
    {
        var time = new ManualTimeProvider();
        ServeRoleMembers(User("x@example.com"), User("y@example.com"));
        await RunAsync(time);

        time.Advance(TimeSpan.FromDays(1));
        Client.SendAsync(Arg.Any<GetUsersInRolePageRequest>(), Arg.Any<CancellationToken>())
            .Returns<GetUsersInRolePageResponse>(_ => throw new InvalidOperationException("The identity provider is down."));
        await Assert.ThrowsAsync<InvalidOperationException>(() => RunAsync(time));

        Assert.Equal(2, await Db.AlertRecipients.AsNoTracking().CountAsync());
    }

    /// <summary>A run stopped by the page cap has not seen the whole role, so it must not remove the members it never reached.</summary>
    [Fact]
    public async Task Reconcile_RunStoppedByThePageCap_RemovesNothing()
    {
        var time = new ManualTimeProvider();
        var members = new[] { User("first@example.com"), User("second@example.com"), User("third@example.com") };
        ServeRoleMembers(members);
        await RunAsync(time);

        time.Advance(TimeSpan.FromDays(1));
        var applied = await RunAsync(time, pageSize: 1, maxPages: 1);

        Assert.Equal(1, applied);
        Assert.Equal(3, await Db.AlertRecipients.AsNoTracking().CountAsync());
    }

    private sealed class ManualTimeProvider : TimeProvider
    {
        private DateTimeOffset _now = new(2026, 1, 1, 3, 15, 0, TimeSpan.Zero);

        public override DateTimeOffset GetUtcNow() => _now;

        public void Advance(TimeSpan by) => _now += by;
    }
}

#pragma warning restore CA1707
