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
///     A saga-shaped hold against stock for a pending order, structurally ported from Payments'
///     DepositPayment escrow lifecycle onto a generic "reserve stock" domain: a header-level state machine
///     driven by different callers over time (the creating endpoint, a warehouse-system webhook, a sweep
///     job), with idempotent no-ops for retry safety and a duplicate-vs-conflicting-webhook distinction.
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
    ///     conflict - never silently swallowed - flagged for manual reconciliation instead of rejected,
    ///     mirroring DepositPayment.MarkCaptured's duplicate-delivery vs double-charge branches.
    /// </summary>
    public Result Commit(string providerReference, DateTimeOffset utcNow)
    {
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

        if (Status != ReservationStatus.Active)
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
        if (Status == ReservationStatus.Released)
        {
            return Result.Success;
        }

        if (Status != ReservationStatus.Active)
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
    ///     Released, already Expired), there is nothing left to expire. Mirrors DepositPayment.Expire
    ///     exactly - callable safely by a sweep job hitting a row that already moved on.
    /// </summary>
    public Result Expire(DateTimeOffset utcNow)
    {
        if (Status != ReservationStatus.Active)
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
    ///     rejection (never after an ambiguous timeout) so a genuine retry can proceed. Mirrors
    ///     DepositPayment.BeginItemOutcomeAttempt.
    /// </summary>
    public Result<string> BeginReleaseAttempt(DateTimeOffset utcNow)
    {
        if (Status != ReservationStatus.Active)
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
        var @event = new V1StockReservationReleaseAttemptAbandonedDomainEvent(Id, utcNow);

        LastReleaseAttemptReference = null;
        RaiseEvent(@event);
        return Result.Success;
    }
}
