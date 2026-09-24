using System.Linq.Expressions;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;

namespace Common.Application.Jobs;

public static class JobClaimExtensions
{
    /// <summary>Failure key stamped by housekeeping on a Running row nobody finished.</summary>
    public const string StaleFailureKey = "JobStale";

    /// <summary>
    ///     One atomic <c>UPDATE ... WHERE Status = Queued</c>: true for exactly one caller per job, so a redelivered
    ///     message finds the row already Running (or finished) and returns false instead of running the work twice.
    /// </summary>
    public static async Task<bool> TryClaimAsync<TJob, TId>(
        this DbSet<TJob> jobs, TId id, DateTimeOffset now, CancellationToken cancellationToken)
        where TJob : JobRow<TId>
        where TId : IStronglyTypedId
    {
        var claimed = await jobs
            .TagWith(nameof(TryClaimAsync))
            .Where(HasId<TJob, TId>(id))
            .Where(j => j.Status == JobStatus.Queued)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(j => j.Status, JobStatus.Running)
                    .SetProperty(j => j.StartedOn, now),
                cancellationToken);

        return claimed == 1;
    }

    /// <summary>Hands a Running job back to the queue (the consumer was cancelled before it could finish).</summary>
    public static async Task<bool> RequeueAsync<TJob, TId>(
        this DbSet<TJob> jobs, TId id, CancellationToken cancellationToken)
        where TJob : JobRow<TId>
        where TId : IStronglyTypedId
    {
        var requeued = await jobs
            .TagWith(nameof(RequeueAsync))
            .Where(HasId<TJob, TId>(id))
            .Where(j => j.Status == JobStatus.Running)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(j => j.Status, JobStatus.Queued)
                    .SetProperty(j => j.StartedOn, (DateTimeOffset?)null),
                cancellationToken);

        return requeued == 1;
    }

    /// <summary>Fails every Running row started before <paramref name="olderThan" /> (its worker died). Returns the count.</summary>
    public static Task<int> MarkStaleFailedAsync<TJob, TId>(
        this DbSet<TJob> jobs, DateTimeOffset olderThan, string failureKey, DateTimeOffset now,
        CancellationToken cancellationToken)
        where TJob : JobRow<TId>
        where TId : IStronglyTypedId
    {
        return jobs
            .TagWith(nameof(MarkStaleFailedAsync))
            .Where(j => j.Status == JobStatus.Running && j.StartedOn < olderThan)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(j => j.Status, JobStatus.Failed)
                    .SetProperty(j => j.FinishedOn, now)
                    .SetProperty(j => j.FailureKey, failureKey),
                cancellationToken);
    }

    /// <summary>
    ///     <c>j =&gt; j.Id == id</c> built by hand: a generic <c>TId</c> only offers <c>object.Equals</c>, which EF cannot
    ///     translate through the value converter, while the record struct's own equality operator translates as the
    ///     column compare. The bound is a closure member, not a bare constant, so EF parameterizes it.
    /// </summary>
    private static Expression<Func<TJob, bool>> HasId<TJob, TId>(TId id)
        where TJob : JobRow<TId>
        where TId : IStronglyTypedId
    {
        var job = Expression.Parameter(typeof(TJob), "j");
        var bound = Expression.Property(Expression.Constant(new IdBound<TId>(id)), nameof(IdBound<TId>.Value));
        var body = Expression.Equal(Expression.Property(job, nameof(JobRow<TId>.Id)), bound);
        return Expression.Lambda<Func<TJob, bool>>(body, job);
    }

    private sealed class IdBound<TId>(TId value)
    {
        public TId Value { get; } = value;
    }
}
