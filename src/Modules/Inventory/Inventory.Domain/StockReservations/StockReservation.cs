using Common.Domain.Aggregates;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StockReservations.DomainEvents.v1;
using Inventory.Domain.StockReservations.Errors;

namespace Inventory.Domain.StockReservations;

public readonly record struct StockReservationId(DefaultIdType Value) : IStronglyTypedId
{
    public static StockReservationId New()
    {
        return new StockReservationId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StockReservationId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     A saga-shaped hold against stock for a pending order: a header-level state machine
///     driven by different callers over time (the creating endpoint, a warehouse-system webhook, a sweep
///     job), with idempotent no-ops for retry safety and a duplicate-vs-conflicting-webhook distinction.
///     <para>
///         The status is also reported by an external system. Which changes of <see cref="Status" /> are legal is decided
///         only by <see cref="ReservationStatusTransitions" />. A status the provider reports in a word nobody mapped never
///         fails the delivery: the word is recorded, and the reservation turns <see cref="IsReadOnly" />, refusing every
///         local command while <see cref="ProviderStatus" /> keeps mirroring what the provider says. A person resolves it
///         with <see cref="ResolveReadOnly" />.
///     </para>
///     <para>
///         Every change of a field raises an event, because the lifecycle lives in the AuditLog. A command that finds nothing
///         to change returns without one.
///     </para>
/// </summary>
public class StockReservation : AggregateRoot<StockReservationId>
{
#pragma warning disable CS8618
    private StockReservation() : base(new StockReservationId(DefaultIdType.Empty)) { }
#pragma warning restore CS8618

    public DefaultIdType ProductId { get; private set; }
    public int Quantity { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTimeOffset ReservationDeadline { get; private set; }
    public string? ProviderReference { get; private set; }
    public string? LastReleaseAttemptReference { get; private set; }

    /// <summary>
    ///     True once the provider reported a status this system could not map. From then on no local command applies and
    ///     <see cref="Status" /> no longer follows the provider, until a person resolves it with <see cref="ResolveReadOnly" />.
    /// </summary>
    public bool IsReadOnly { get; private set; }

    /// <summary>
    ///     The last status the provider reported, kept only for a read-only reservation, whose own <see cref="Status" /> no
    ///     longer follows it. <see cref="ReservationStatus.Unknown" /> when the last word could not be mapped. Null while the
    ///     reservation is writable: its <see cref="Status" /> is then the provider's.
    /// </summary>
    public ReservationStatus? ProviderStatus { get; private set; }

    /// <summary>
    ///     The provider's own timestamp of the latest delivery applied to this reservation, when the provider sends one. A
    ///     delivery stamped earlier than this is stale (<see cref="IsStale" />): it describes a moment the reservation has
    ///     moved past. Null until a stamped delivery was applied; a delivery without a stamp never changes it.
    /// </summary>
    public DateTimeOffset? LastProviderEventOn { get; private set; }

    public static StockReservation Reserve(DefaultIdType productId, int quantity, DateTimeOffset deadline)
    {
        var id = StockReservationId.New();

        var @event = new V1StockReservationReservedDomainEvent(id, productId, quantity, deadline);

        var reservation = new StockReservation
        {
            Id = id,
            ProductId = productId,
            Quantity = quantity,
            Status = ReservationStatus.Active,
            ReservationDeadline = deadline
        };

        reservation.RaiseEvent(@event);
        return reservation;
    }

    /// <summary>
    ///     Records the warehouse system's confirmation. A second Commit with the SAME reference is a benign
    ///     duplicate webhook delivery (no-op). A second Commit with a DIFFERENT reference is a genuine
    ///     conflict - never silently swallowed - flagged for manual reconciliation instead of rejected.
    /// </summary>
    public Result Commit(string providerReference, DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return StockReservationErrors.ReservationReadOnly;
        }

        if (Status == ReservationStatus.Committed)
        {
            if (string.Equals(ProviderReference, providerReference, StringComparison.Ordinal))
            {
                return Result.Success;
            }

            var conflictEvent = new V1StockReservationCommitConflictDetectedDomainEvent(
                Id, ProductId, ProviderReference!, providerReference, utcNow);

            Status = ReservationStatus.RequiresReconciliation;
            RaiseEvent(conflictEvent);
            return Result.Success;
        }

        if (!CanMoveTo(ReservationStatus.Committed))
        {
            return StockReservationErrors.NotReserved;
        }

        if (utcNow > ReservationDeadline)
        {
            return StockReservationErrors.DeadlinePassed;
        }

        // Always build the event before mutating (house style, not just for old-value reads).
        var @event = new V1StockReservationCommittedDomainEvent(Id, ProductId, Quantity, providerReference, utcNow);

        Status = ReservationStatus.Committed;
        ProviderReference = providerReference;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>Idempotent/retry-safe: already-Released returns Success as a no-op.</summary>
    public Result Release(DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return StockReservationErrors.ReservationReadOnly;
        }

        if (Status == ReservationStatus.Released)
        {
            return Result.Success;
        }

        if (!CanMoveTo(ReservationStatus.Released))
        {
            return StockReservationErrors.CannotRelease;
        }

        var @event = new V1StockReservationReleasedDomainEvent(Id, ProductId, Quantity, utcNow);

        Status = ReservationStatus.Released;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>
    ///     Idempotent/retry-safe: if the reservation already resolved one way or another (Committed,
    ///     Released, already Expired), there is nothing left to expire, so a sweep job can safely hit a row
    ///     that already moved on.
    /// </summary>
    public Result Expire(DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return StockReservationErrors.ReservationReadOnly;
        }

        if (!CanMoveTo(ReservationStatus.Expired))
        {
            return Result.Success;
        }

        var @event = new V1StockReservationExpiredDomainEvent(Id, ProductId, Quantity, utcNow);

        Status = ReservationStatus.Expired;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>
    ///     Persists an attempt marker BEFORE calling the external warehouse system to release the hold.
    ///     If a marker already exists, an earlier attempt was interrupted between the call and recording its
    ///     outcome; its true state at the provider is unknown, so this refuses a new attempt rather than risk
    ///     a blind double-release. Callers must call <see cref="AbandonReleaseAttempt"/> after a DEFINITE
    ///     rejection (never after an ambiguous timeout) so a genuine retry can proceed.
    /// </summary>
    public Result<string> BeginReleaseAttempt(DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return StockReservationErrors.ReservationReadOnly;
        }

        if (!CanMoveTo(ReservationStatus.Released))
        {
            return StockReservationErrors.CannotRelease;
        }

        if (LastReleaseAttemptReference is not null)
        {
            return StockReservationErrors.AmbiguousPriorAttempt;
        }

        var reference = $"rel:{Id}:{utcNow.Ticks}";
        var @event = new V1StockReservationReleaseAttemptStartedDomainEvent(Id, reference, utcNow);

        LastReleaseAttemptReference = reference;
        RaiseEvent(@event);
        return reference;
    }

    /// <summary>
    ///     Clears the attempt marker so a release is retryable again. Only safe after a DEFINITE rejection
    ///     from the external warehouse system - never after an ambiguous timeout, where the marker must stay
    ///     in place to force manual reconciliation instead of risking a blind double-release.
    /// </summary>
    public Result AbandonReleaseAttempt(DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return StockReservationErrors.ReservationReadOnly;
        }

        if (LastReleaseAttemptReference is null)
        {
            return Result.Success;
        }

        var @event = new V1StockReservationReleaseAttemptAbandonedDomainEvent(Id, utcNow);

        LastReleaseAttemptReference = null;
        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>
    ///     The warehouse system confirmed the release this reservation's attempt marker stands for: the release call answered,
    ///     or a reconcile found it done. A writable reservation is released exactly as <see cref="Release" /> does. A read-only
    ///     one takes no local command (it turned read-only after the marker was saved, while the provider was releasing), so
    ///     the confirmation only clears the marker and is mirrored into <see cref="ProviderStatus" />; the reservation's own
    ///     status stays for a person to resolve.
    /// </summary>
    public Result ConfirmReleaseAttempt(DateTimeOffset utcNow)
    {
        if (!IsReadOnly)
        {
            return Release(utcNow);
        }

        if (LastReleaseAttemptReference is not null)
        {
            var @event = new V1StockReservationReleaseAttemptConfirmedDomainEvent(Id, utcNow);

            LastReleaseAttemptReference = null;
            RaiseEvent(@event);
        }

        return MirrorProviderStatus(ReservationStatus.Released, utcNow);
    }

    /// <summary>
    ///     Applies a status the provider reported in a word that maps (<see cref="ProviderStatusMap" />). A writable
    ///     reservation follows it through the same commands a local caller uses, so every rule of those commands holds;
    ///     <see cref="ReservationStatus.Active" /> says the provider still holds the reservation and changes nothing. A
    ///     read-only reservation only mirrors it into <see cref="ProviderStatus" />, with no local effect and no failure.
    ///     <see cref="ReservationStatus.Unknown" /> and <see cref="ReservationStatus.RequiresReconciliation" /> are never
    ///     something a provider reports, so passing them is a caller bug.
    /// </summary>
    public Result ReceiveProviderStatus(
        ReservationStatus reported, string? providerReference, DateTimeOffset utcNow, DateTimeOffset? occurredOn = null)
    {
        if (reported is ReservationStatus.Unknown or ReservationStatus.RequiresReconciliation)
        {
            throw new ArgumentOutOfRangeException(nameof(reported), reported, "A provider cannot report this status.");
        }

        var applied = ApplyProviderStatus(reported, providerReference, utcNow);
        if (!applied.IsFailure)
        {
            RecordProviderEventTime(occurredOn, utcNow);
        }

        return applied;
    }

    /// <summary>
    ///     True when a delivery stamped <paramref name="occurredOn" /> is older than the latest stamped delivery applied: the
    ///     caller acknowledges it and applies nothing. A delivery without a stamp, and one stamped the same instant, is not
    ///     stale.
    /// </summary>
    public bool IsStale(DateTimeOffset? occurredOn)
    {
        return occurredOn is { } stamp && LastProviderEventOn is { } last && stamp.ToUniversalTime() < last;
    }

    private Result ApplyProviderStatus(ReservationStatus reported, string? providerReference, DateTimeOffset utcNow)
    {
        if (IsReadOnly)
        {
            return MirrorProviderStatus(reported, utcNow);
        }

        return reported switch
        {
            ReservationStatus.Committed => providerReference is null
                ? Result.Failure(StockReservationErrors.ProviderReferenceRequired)
                : Commit(providerReference, utcNow),
            ReservationStatus.Released => Release(utcNow),
            ReservationStatus.Expired => Expire(utcNow),
            ReservationStatus.Active => Result.Success,
            _ => throw new ArgumentOutOfRangeException(nameof(reported), reported, null)
        };
    }

    /// <summary>
    ///     Records a status word the provider sent that has no mapping. Never fails and never changes <see cref="Status" />:
    ///     the reservation turns read-only and its <see cref="ProviderStatus" /> becomes <see cref="ReservationStatus.Unknown" />.
    ///     Every such word is recorded, also on a reservation that is read-only already, because the word itself is what an
    ///     operator needs to find.
    /// </summary>
    public void RecordUnrecognizedProviderStatus(string rawStatus, DateTimeOffset utcNow, DateTimeOffset? occurredOn = null)
    {
        var word = rawStatus.Trim();
        var @event = new V1StockReservationProviderStatusUnrecognizedDomainEvent(
            Id, word.Length > Constants.ProviderStatusMaxLength ? word[..Constants.ProviderStatusMaxLength] : word, utcNow);

        IsReadOnly = true;
        ProviderStatus = ReservationStatus.Unknown;
        RaiseEvent(@event);
        RecordProviderEventTime(occurredOn, utcNow);
    }

    /// <summary>
    ///     A person's decision on a read-only reservation, taken after checking with the provider what it really is: it takes
    ///     local commands again and is brought to <paramref name="target" />, which is <see cref="ReservationStatus.Released" />,
    ///     <see cref="ReservationStatus.Committed" /> or <see cref="ReservationStatus.Expired" />. The move is made by the
    ///     command a local caller would use, so its rules hold (a commit needs the provider reference and a deadline that has
    ///     not passed), and <see cref="ReservationStatusTransitions" /> decides whether the move exists at all. A target equal to
    ///     the status the reservation already has only lifts the flag. A refusal changes nothing and the reservation stays
    ///     read-only.
    /// </summary>
    public Result ResolveReadOnly(ReservationStatus target, string? providerReference, DateTimeOffset utcNow)
    {
        if (!IsReadOnly)
        {
            return StockReservationErrors.NotReadOnly;
        }

        if (target is not (ReservationStatus.Released or ReservationStatus.Committed or ReservationStatus.Expired)
            || (target != Status && !CanMoveTo(target)))
        {
            return StockReservationErrors.ResolutionNotAllowed;
        }

        var previousProviderStatus = ProviderStatus;
        var @event = new V1StockReservationReadOnlyResolvedDomainEvent(
            Id, (previousProviderStatus ?? ReservationStatus.Unknown).ToResolvedSnapshot(), target.ToResolvedSnapshot(), utcNow);

        // The commands refuse a read-only reservation, so the flag goes first and comes back if the move is refused.
        IsReadOnly = false;
        ProviderStatus = null;

        var moved = target == Status ? Result.Success : MoveTo(target, providerReference, utcNow);

        if (moved.IsFailure)
        {
            IsReadOnly = true;
            ProviderStatus = previousProviderStatus;
            return moved;
        }

        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>Keeps the provider's timestamp of an applied delivery; one that is not newer than the kept stamp changes nothing.</summary>
    private void RecordProviderEventTime(DateTimeOffset? occurredOn, DateTimeOffset utcNow)
    {
        if (occurredOn is not { } stamp)
        {
            return;
        }

        var instant = stamp.ToUniversalTime();
        if (LastProviderEventOn is { } last && instant <= last)
        {
            return;
        }

        var @event = new V1StockReservationProviderEventTimeRecordedDomainEvent(Id, LastProviderEventOn, instant, utcNow);

        LastProviderEventOn = instant;
        RaiseEvent(@event);
    }

    /// <summary>The command a local caller would use to reach <paramref name="target" />, one of Released, Committed and Expired.</summary>
    private Result MoveTo(ReservationStatus target, string? providerReference, DateTimeOffset utcNow)
    {
        if (target == ReservationStatus.Committed)
        {
            return providerReference is null
                ? StockReservationErrors.ProviderReferenceRequired
                : Commit(providerReference, utcNow);
        }

        return target == ReservationStatus.Released ? Release(utcNow) : Expire(utcNow);
    }

    private Result MirrorProviderStatus(ReservationStatus reported, DateTimeOffset utcNow)
    {
        if (ProviderStatus == reported)
        {
            return Result.Success;
        }

        var @event = new V1StockReservationProviderStatusMirroredDomainEvent(Id, reported.ToMirroredSnapshot(), utcNow);

        ProviderStatus = reported;
        RaiseEvent(@event);
        return Result.Success;
    }

    private bool CanMoveTo(ReservationStatus next)
    {
        return ReservationStatusTransitions.CanTransition(Status, next);
    }
}
