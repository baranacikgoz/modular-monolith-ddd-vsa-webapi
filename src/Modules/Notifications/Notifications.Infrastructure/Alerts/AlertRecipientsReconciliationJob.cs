using Common.Application.Options;
using Common.Application.Persistence.Projections;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Persistence;
using Notifications.Domain.Alerts;

namespace Notifications.Infrastructure.Alerts;

/// <summary>Offset cursor over IAM's role members: how many members earlier pages already covered.</summary>
public sealed record AlertRecipientsCursor(int Skip);

/// <summary>
///     Recurring heal of <see cref="AlertRecipient" /> from IAM, page by page through <see cref="GetUsersInRolePageRequest" />.
///     IAM publishes no event when a user gains or loses the role, so this run is the only way the list follows it.
///     Members without an email are skipped (nothing to send to). After a run that read the whole role and found at least
///     one member, rows it did not see are removed. A run that finds nobody removes nothing: an empty role is more likely
///     an identity-provider fault than the truth, and keeping a stale address is the safe side for an alert.
/// </summary>
public sealed partial class AlertRecipientsReconciliationJob(
    INotificationsDbContext dbContext,
    IInterModuleRequestClient<GetUsersInRolePageRequest, GetUsersInRolePageResponse> usersClient,
    TimeProvider timeProvider,
    IOptions<AlertRecipientsOptions> alertOptionsProvider,
    IOptions<ProjectionReconciliationOptions> reconciliationOptionsProvider,
    ILogger<AlertRecipientsReconciliationJob> logger)
    : ProjectionReconciliationJob<RoleUserSummary, AlertRecipientsCursor>(dbContext, reconciliationOptionsProvider, logger)
{
    private DateTimeOffset _runStartedOn;
    private bool _readWholeRole;

    public async Task<int> ReconcileAsync(CancellationToken cancellationToken = default)
    {
        _runStartedOn = timeProvider.GetUtcNow();
        _readWholeRole = false;

        var applied = await RunAsync(cancellationToken);

        if (_readWholeRole && applied > 0)
        {
            var removed = await dbContext.AlertRecipients
                .TagWith(nameof(ReconcileAsync))
                .Where(r => r.LastSeenOn < _runStartedOn)
                .ExecuteDeleteAsync(cancellationToken);
            if (removed > 0)
            {
                LogRecipientsRemoved(logger, removed, alertOptionsProvider.Value.RoleName);
            }
        }

        return applied;
    }

    protected override async Task<(IReadOnlyList<RoleUserSummary> Items, AlertRecipientsCursor? Next)> FetchPageAsync(
        AlertRecipientsCursor? cursor, int pageSize, CancellationToken cancellationToken)
    {
        var roleName = alertOptionsProvider.Value.RoleName;
        var skip = cursor?.Skip ?? 0;

        while (true)
        {
            var response = await usersClient.SendAsync(new GetUsersInRolePageRequest(roleName, skip, pageSize), cancellationToken);
            skip += pageSize;

            var users = response.Users.Where(u => !string.IsNullOrWhiteSpace(u.Email)).ToList();

            if (!response.HasMore)
            {
                _readWholeRole = true;
                return (users, null);
            }

            // The base run ends on an empty page, so a page whose members are all disabled or without an email is skipped here.
            if (users.Count > 0)
            {
                return (users, new AlertRecipientsCursor(skip));
            }
        }
    }

    // Always reports 0 healed rows: no event feeds this list, so a changed row is the normal path, not a missed event.
    protected override async Task<int> ApplyPageAsync(IReadOnlyList<RoleUserSummary> items, CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow();
        var users = items.DistinctBy(u => u.Id).ToList();
        var userIds = users.Select(u => u.Id).ToList();

        var existing = await dbContext.AlertRecipients
            .TagWith(nameof(ApplyPageAsync))
            .Where(r => userIds.Contains(r.UserId))
            .ToDictionaryAsync(r => r.UserId, cancellationToken);

        foreach (var user in users)
        {
            var email = user.Email!.Trim();
            if (existing.TryGetValue(user.Id, out var row))
            {
                row.Refresh(email, now);
            }
            else
            {
                dbContext.AlertRecipients.Add(AlertRecipient.Create(user.Id, email, now));
            }
        }

        return 0;
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Alert recipients reconcile removed {Count} addresses of users that no longer hold the {RoleName} role.")]
    private static partial void LogRecipientsRemoved(ILogger logger, int count, string roleName);
}
