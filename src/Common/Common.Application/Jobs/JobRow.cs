using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Jobs;

/// <summary>
///     Base row for accept-queue-poll work: an endpoint writes it and enqueues one message, a consumer claims it with
///     <see cref="JobClaimExtensions.TryClaimAsync{TJob,TId}" /> and reports the outcome, clients poll it. Event-free by
///     design: it is bookkeeping, not an aggregate, so it never writes <c>AuditLog</c> or outbox rows.
/// </summary>
public abstract class JobRow<TId>(TId id) : AuditableEntity<TId>(id)
    where TId : IStronglyTypedId
{
    public const int FailureKeyMaxLength = 128;

    public JobStatus Status { get; protected set; } = JobStatus.Queued;
    public ApplicationUserId? RequestedBy { get; protected set; }
    public DateTimeOffset QueuedOn { get; protected set; }
    public DateTimeOffset? StartedOn { get; protected set; }
    public DateTimeOffset? FinishedOn { get; protected set; }

    /// <summary>Stable, machine-readable reason (a localization key, never an exception message).</summary>
    public string? FailureKey { get; protected set; }

    /// <summary>Consumer-defined progress counter (items done, percent, ...).</summary>
    public int Progress { get; protected set; }

    protected JobRow(TId id, ApplicationUserId? requestedBy, DateTimeOffset queuedOn) : this(id)
    {
        RequestedBy = requestedBy;
        QueuedOn = queuedOn;
    }

    public void MarkRunning(DateTimeOffset now)
    {
        Status = JobStatus.Running;
        StartedOn = now;
    }

    public void MarkSucceeded(DateTimeOffset now)
    {
        Status = JobStatus.Succeeded;
        FinishedOn = now;
        FailureKey = null;
    }

    public void MarkFailed(DateTimeOffset now, string failureKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(failureKey);

        Status = JobStatus.Failed;
        FinishedOn = now;
        FailureKey = failureKey.Length <= FailureKeyMaxLength ? failureKey : failureKey[..FailureKeyMaxLength];
    }

    public void ReportProgress(int progress)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(progress);
        Progress = progress;
    }
}
