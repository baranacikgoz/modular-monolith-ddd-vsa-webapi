using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Notifications.Domain.Alerts;

/// <summary>
///     One user whose email receives the critical alerts addressed to the configured role. A read-model row, not an
///     aggregate: IAM owns the users and publishes no event when a user gains or loses a role, so the only writer is the
///     nightly AlertRecipientsReconciliationJob, which stamps <see cref="LastSeenOn" /> on every member it sees and removes
///     the rows a complete run did not see.
/// </summary>
#pragma warning disable S3453, S1144 // EF materializes the row through the private constructor.
public sealed class AlertRecipient : AuditableEntity
{
    public const int EmailMaxLength = 320;

    private AlertRecipient()
    {
    } // EF Core needs a parameterless ctor

    public ApplicationUserId UserId { get; private init; }
    public string Email { get; private set; } = string.Empty;
    public DateTimeOffset LastSeenOn { get; private set; }

    public static AlertRecipient Create(ApplicationUserId userId, string email, DateTimeOffset now)
    {
        return new AlertRecipient { UserId = userId, Email = email, LastSeenOn = now };
    }

    public void Refresh(string email, DateTimeOffset now)
    {
        Email = email;
        LastSeenOn = now;
    }
}
#pragma warning restore S3453, S1144
