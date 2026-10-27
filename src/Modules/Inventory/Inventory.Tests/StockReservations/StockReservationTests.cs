using Common.Domain.ResultMonad;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.DomainEvents.v1;
using Inventory.Domain.StockReservations.Errors;
using Xunit;

namespace Inventory.Tests.StockReservations;

public class StockReservationTests
{
    private static readonly DateTimeOffset Now = new(2026, 10, 27, 12, 0, 0, TimeSpan.Zero);

    private static StockReservation NewReservation()
    {
        var reservation = StockReservation.Reserve(DefaultIdType.CreateVersion7(), 5, Now.AddDays(1));
        reservation.ClearEvents();
        return reservation;
    }

    private static StockReservation ReadOnlyReservation(bool markerFirst = false)
    {
        var reservation = NewReservation();
        if (markerFirst)
        {
            reservation.BeginReleaseAttempt(Now);
        }

        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now);
        reservation.ClearEvents();
        return reservation;
    }

    /// <summary>Everything a command can change on the reservation, compared before and after it runs.</summary>
    private static (ReservationStatus Status, string? ProviderReference, string? ReleaseMarker, DateTimeOffset? ReleaseMarkerOn,
        bool IsReadOnly, ReservationStatus? ProviderStatus, DateTimeOffset? LastProviderEventOn) StateOf(StockReservation reservation)
    {
        return (reservation.Status, reservation.ProviderReference, reservation.LastReleaseAttemptReference,
            reservation.LastReleaseAttemptOn, reservation.IsReadOnly, reservation.ProviderStatus, reservation.LastProviderEventOn);
    }

    /// <summary>
    ///     The lifecycle lives in the AuditLog: a command raises an event exactly when it changed a field, and none otherwise.
    ///     A change of the status is always a move the transition table allows.
    /// </summary>
    private static void AssertRaisesAnEventExactlyWhenAFieldChanged(StockReservation reservation, Action<StockReservation> command)
    {
        var before = StateOf(reservation);
        var versionBefore = reservation.Version;
        reservation.ClearEvents();

        command(reservation);

        var after = StateOf(reservation);
        Assert.Equal(after != before, reservation.Events.Count > 0);
        Assert.Equal(versionBefore + reservation.Events.Count, reservation.Version);
        Assert.True(
            after.Status == before.Status || ReservationStatusTransitions.CanTransition(before.Status, after.Status),
            $"{before.Status} -> {after.Status} is not in the transition table.");
    }

    [Fact]
    public void Reserve_NewReservation_IsActiveAndWritableWithNothingMirrored()
    {
        var reservation = NewReservation();

        Assert.Equal(ReservationStatus.Active, reservation.Status);
        Assert.False(reservation.IsReadOnly);
        Assert.Null(reservation.ProviderStatus);
    }

    [Fact]
    public void Commit_ActiveReservation_RecordsTheReferenceAndRaisesOneEvent()
    {
        var reservation = NewReservation();

        var result = reservation.Commit("ref-1", Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Committed, reservation.Status);
        Assert.Equal("ref-1", reservation.ProviderReference);
        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationCommittedDomainEvent>());
        Assert.Equal("ref-1", @event.ProviderReference);
    }

    [Fact]
    public void Commit_SameReferenceAgain_IsANoOpWithoutAnEvent()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.Commit("ref-1", Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void Commit_DifferentReferenceAgain_MovesToRequiresReconciliationAndRaisesTheConflictEvent()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.Commit("ref-2", Now).IsFailure));

        Assert.Equal(ReservationStatus.RequiresReconciliation, reservation.Status);
        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationCommitConflictDetectedDomainEvent>());
        Assert.Equal("ref-1", @event.ExistingProviderReference);
        Assert.Equal("ref-2", @event.ConflictingProviderReference);
    }

    [Fact]
    public void Release_AlreadyReleased_IsANoOpWithoutAnEvent()
    {
        var reservation = NewReservation();
        reservation.Release(Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.Release(Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void Release_CommittedReservation_IsRefusedBecauseTheTableHasNoSuchMove()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);

        var result = reservation.Release(Now);

        Assert.Equal(StockReservationErrors.CannotRelease.Key, result.Error!.Key);
        Assert.Equal(ReservationStatus.Committed, reservation.Status);
    }

    [Fact]
    public void Expire_ReservationThatAlreadyMovedOn_IsANoOpWithoutAnEvent()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.Expire(Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void AbandonReleaseAttempt_WithoutAMarker_IsANoOpWithoutAnEvent()
    {
        var reservation = NewReservation();

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.AbandonReleaseAttempt(Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void BeginReleaseAttempt_SetsTheMarkerAndWhenItWasSet()
    {
        var reservation = NewReservation();

        var result = reservation.BeginReleaseAttempt(Now);

        Assert.False(result.IsFailure);
        Assert.Equal(result.Value, reservation.LastReleaseAttemptReference);
        Assert.Equal(Now, reservation.LastReleaseAttemptOn);
    }

    [Fact]
    public void AbandonReleaseAttempt_WithAMarker_ClearsItAndRaisesTheEvent()
    {
        var reservation = NewReservation();
        reservation.BeginReleaseAttempt(Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.AbandonReleaseAttempt(Now).IsFailure));

        Assert.Null(reservation.LastReleaseAttemptReference);
        Assert.Null(reservation.LastReleaseAttemptOn);
        Assert.Single(reservation.Events.OfType<V1StockReservationReleaseAttemptAbandonedDomainEvent>());
    }

    [Fact]
    public void ConfirmReleaseAttempt_WritableReservation_ReleasesItLikeRelease()
    {
        var reservation = NewReservation();
        reservation.BeginReleaseAttempt(Now);

        var result = reservation.ConfirmReleaseAttempt(Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Released, reservation.Status);
        Assert.Single(reservation.Events.OfType<V1StockReservationReleasedDomainEvent>());
    }

    [Fact]
    public void ConfirmReleaseAttempt_ReadOnlyReservationWithAMarker_ClearsTheMarkerMirrorsTheReleaseAndKeepsItsStatus()
    {
        var reservation = ReadOnlyReservation(markerFirst: true);

        var result = reservation.ConfirmReleaseAttempt(Now);

        Assert.False(result.IsFailure);
        Assert.True(reservation.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reservation.Status);
        Assert.Equal(ReservationStatus.Released, reservation.ProviderStatus);
        Assert.Null(reservation.LastReleaseAttemptReference);
        Assert.Null(reservation.LastReleaseAttemptOn);
        Assert.Single(reservation.Events.OfType<V1StockReservationReleaseAttemptConfirmedDomainEvent>());
        Assert.Single(reservation.Events.OfType<V1StockReservationProviderStatusMirroredDomainEvent>());
        Assert.Empty(reservation.Events.OfType<V1StockReservationReleasedDomainEvent>());
    }

    [Fact]
    public void ConfirmReleaseAttempt_ReadOnlyReservationConfirmedAgain_IsANoOpWithoutAnEvent()
    {
        var reservation = ReadOnlyReservation(markerFirst: true);
        reservation.ConfirmReleaseAttempt(Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(reservation, r => Assert.False(r.ConfirmReleaseAttempt(Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void RecordUnrecognizedProviderStatus_ActiveReservation_EntersReadOnlyKeepsItsStatusAndRecordsTheRawText()
    {
        var reservation = NewReservation();

        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now);

        Assert.True(reservation.IsReadOnly);
        Assert.Equal(ReservationStatus.Active, reservation.Status);
        Assert.Equal(ReservationStatus.Unknown, reservation.ProviderStatus);
        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationProviderStatusUnrecognizedDomainEvent>());
        Assert.Equal("partially_picked", @event.RawStatus);
        Assert.Equal(reservation.Id, @event.StockReservationId);
        Assert.Equal(Now, @event.ReceivedAt);
    }

    [Fact]
    public void RecordUnrecognizedProviderStatus_RawTextOverTheBound_IsCutToTheBoundNotRejected()
    {
        var reservation = NewReservation();

        reservation.RecordUnrecognizedProviderStatus("  " + new string('x', Constants.ProviderStatusMaxLength + 50), Now);

        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationProviderStatusUnrecognizedDomainEvent>());
        Assert.Equal(new string('x', Constants.ProviderStatusMaxLength), @event.RawStatus);
    }

    [Fact]
    public void RecordUnrecognizedProviderStatus_OnAnAlreadyReadOnlyReservation_RecordsTheNewRawTextAgain()
    {
        var reservation = ReadOnlyReservation();

        reservation.RecordUnrecognizedProviderStatus("another_word", Now);

        Assert.True(reservation.IsReadOnly);
        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationProviderStatusUnrecognizedDomainEvent>());
        Assert.Equal("another_word", @event.RawStatus);
    }

    [Fact]
    public void RecordUnrecognizedProviderStatus_OnACommittedReservation_NeverChangesItsStatus()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);

        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now);

        Assert.Equal(ReservationStatus.Committed, reservation.Status);
        Assert.True(reservation.IsReadOnly);
    }

    public static TheoryData<string, Func<StockReservation, Error>> LocalCommands() => new()
    {
        { "Commit", r => r.Commit("ref-1", Now).Error! },
        { "Release", r => r.Release(Now).Error! },
        { "Expire", r => r.Expire(Now).Error! },
        { "BeginReleaseAttempt", r => r.BeginReleaseAttempt(Now).Error! },
        { "AbandonReleaseAttempt", r => r.AbandonReleaseAttempt(Now).Error! }
    };

    [Theory]
    [MemberData(nameof(LocalCommands))]
    public void LocalCommand_OnAReadOnlyReservation_ReturnsReservationReadOnlyAndChangesNothing(
        string command, Func<StockReservation, Error> run)
    {
        var reservation = ReadOnlyReservation();
        var before = StateOf(reservation);

        var error = run(reservation);

        Assert.True(
            error.Key == StockReservationErrors.ReservationReadOnly.Key,
            $"{command} on a read-only reservation answered {error.Key}.");
        Assert.Equal(before, StateOf(reservation));
        Assert.Empty(reservation.Events);
    }

    [Theory]
    [InlineData(ReservationStatus.Released)]
    [InlineData(ReservationStatus.Expired)]
    public void ResolveReadOnly_ActiveReservation_LiftsTheFlagAppliesTheTargetAndRaisesTheResolutionEvent(ReservationStatus target)
    {
        var reservation = ReadOnlyReservation();

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ResolveReadOnly(target, null, Now).IsFailure));

        Assert.False(reservation.IsReadOnly);
        Assert.Null(reservation.ProviderStatus);
        Assert.Equal(target, reservation.Status);
        var resolved = Assert.Single(reservation.Events.OfType<V1StockReservationReadOnlyResolvedDomainEvent>());
        Assert.Equal(V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Unknown, resolved.ProviderStatus);
        Assert.Equal(Enum.Parse<V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot>(target.ToString()), resolved.ResolvedStatus);
        Assert.Equal(2, reservation.Events.Count);
    }

    [Fact]
    public void ResolveReadOnly_ToCommitted_CommitsWithTheReferenceAndRaisesTheCommittedEvent()
    {
        var reservation = ReadOnlyReservation();

        var result = reservation.ResolveReadOnly(ReservationStatus.Committed, "ref-9", Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Committed, reservation.Status);
        Assert.Equal("ref-9", reservation.ProviderReference);
        Assert.False(reservation.IsReadOnly);
        Assert.Single(reservation.Events.OfType<V1StockReservationCommittedDomainEvent>());
        Assert.Single(reservation.Events.OfType<V1StockReservationReadOnlyResolvedDomainEvent>());
    }

    [Fact]
    public void ResolveReadOnly_ToCommittedWithoutAReference_IsRefusedAndLeavesTheReservationReadOnly()
    {
        var reservation = ReadOnlyReservation();
        var before = StateOf(reservation);

        var result = reservation.ResolveReadOnly(ReservationStatus.Committed, null, Now);

        Assert.Equal(StockReservationErrors.ProviderReferenceRequired.Key, result.Error!.Key);
        Assert.Equal(before, StateOf(reservation));
        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ResolveReadOnly_ToCommittedAfterTheDeadline_IsRefusedLikeACommitAndLeavesTheReservationReadOnly()
    {
        var reservation = ReadOnlyReservation();
        var before = StateOf(reservation);

        var result = reservation.ResolveReadOnly(ReservationStatus.Committed, "ref-9", Now.AddDays(2));

        Assert.Equal(StockReservationErrors.DeadlinePassed.Key, result.Error!.Key);
        Assert.Equal(before, StateOf(reservation));
        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ResolveReadOnly_TargetEqualToTheCurrentStatus_OnlyLiftsTheFlag()
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);
        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now);
        reservation.ClearEvents();

        var result = reservation.ResolveReadOnly(ReservationStatus.Committed, null, Now);

        Assert.False(result.IsFailure);
        Assert.False(reservation.IsReadOnly);
        Assert.Equal(ReservationStatus.Committed, reservation.Status);
        Assert.Equal("ref-1", reservation.ProviderReference);
        Assert.Single(reservation.Events);
        Assert.Single(reservation.Events.OfType<V1StockReservationReadOnlyResolvedDomainEvent>());
    }

    [Theory]
    [InlineData(ReservationStatus.Released)]
    [InlineData(ReservationStatus.Expired)]
    public void ResolveReadOnly_MoveTheTransitionTableForbids_IsRefusedAndLeavesTheReservationReadOnly(ReservationStatus target)
    {
        var reservation = NewReservation();
        reservation.Commit("ref-1", Now);
        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now);
        reservation.ClearEvents();
        var before = StateOf(reservation);

        var result = reservation.ResolveReadOnly(target, null, Now);

        Assert.Equal(StockReservationErrors.ResolutionNotAllowed.Key, result.Error!.Key);
        Assert.Equal(before, StateOf(reservation));
        Assert.Empty(reservation.Events);
    }

    [Theory]
    [InlineData(ReservationStatus.Active)]
    [InlineData(ReservationStatus.RequiresReconciliation)]
    [InlineData(ReservationStatus.Unknown)]
    public void ResolveReadOnly_TargetThatIsNotAResolution_IsRefused(ReservationStatus target)
    {
        var reservation = ReadOnlyReservation();

        var result = reservation.ResolveReadOnly(target, "ref-1", Now);

        Assert.Equal(StockReservationErrors.ResolutionNotAllowed.Key, result.Error!.Key);
        Assert.True(reservation.IsReadOnly);
        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ResolveReadOnly_WritableReservation_IsRefusedAsNotReadOnly()
    {
        var reservation = NewReservation();

        var result = reservation.ResolveReadOnly(ReservationStatus.Released, null, Now);

        Assert.Equal(StockReservationErrors.NotReadOnly.Key, result.Error!.Key);
        Assert.Equal(ReservationStatus.Active, reservation.Status);
        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ResolveReadOnly_ResolvedReservation_TakesLocalCommandsAgain()
    {
        var reservation = ReadOnlyReservation();
        reservation.ResolveReadOnly(ReservationStatus.Released, null, Now);

        var again = reservation.Release(Now);

        Assert.False(again.IsFailure);
        Assert.Equal(ReservationStatus.Released, reservation.Status);
    }

    [Fact]
    public void ReceiveProviderStatus_WithATimestamp_KeepsItAndRaisesTheTimestampEventAfterTheStatusEvent()
    {
        var reservation = NewReservation();

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ReceiveProviderStatus(ReservationStatus.Released, null, Now, Now.AddMinutes(-3)).IsFailure));

        Assert.Equal(Now.AddMinutes(-3), reservation.LastProviderEventOn);
        Assert.IsType<V1StockReservationReleasedDomainEvent>(reservation.Events.First());
        var recorded = Assert.IsType<V1StockReservationProviderEventTimeRecordedDomainEvent>(reservation.Events.Last());
        Assert.Null(recorded.PreviousOccurredOn);
        Assert.Equal(Now.AddMinutes(-3), recorded.OccurredOn);
    }

    [Fact]
    public void ReceiveProviderStatus_StatusThatChangesNothingButCarriesANewerTimestamp_StillKeepsTheTimestamp()
    {
        var reservation = NewReservation();
        reservation.ReceiveProviderStatus(ReservationStatus.Active, null, Now, Now.AddMinutes(-3));

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ReceiveProviderStatus(ReservationStatus.Active, null, Now, Now.AddMinutes(-2)).IsFailure));

        Assert.Equal(Now.AddMinutes(-2), reservation.LastProviderEventOn);
        Assert.Single(reservation.Events);
    }

    [Fact]
    public void ReceiveProviderStatus_WithoutATimestamp_NeverChangesTheKeptOne()
    {
        var reservation = NewReservation();
        reservation.ReceiveProviderStatus(ReservationStatus.Active, null, Now, Now.AddMinutes(-3));

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ReceiveProviderStatus(ReservationStatus.Active, null, Now).IsFailure));

        Assert.Equal(Now.AddMinutes(-3), reservation.LastProviderEventOn);
        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ReceiveProviderStatus_RefusedByTheCommand_KeepsNoTimestamp()
    {
        var reservation = NewReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Committed, null, Now, Now.AddMinutes(-3));

        Assert.True(result.IsFailure);
        Assert.Null(reservation.LastProviderEventOn);
    }

    [Fact]
    public void RecordUnrecognizedProviderStatus_WithATimestamp_KeepsIt()
    {
        var reservation = NewReservation();

        reservation.RecordUnrecognizedProviderStatus("partially_picked", Now, Now.AddMinutes(-3));

        Assert.Equal(Now.AddMinutes(-3), reservation.LastProviderEventOn);
        Assert.Single(reservation.Events.OfType<V1StockReservationProviderEventTimeRecordedDomainEvent>());
    }

    [Theory]
    [InlineData(-4, true)]
    [InlineData(-3, false)]
    [InlineData(-2, false)]
    public void IsStale_ComparesTheTimestampWithTheLatestApplied(int minutesFromNow, bool expected)
    {
        var reservation = NewReservation();
        reservation.ReceiveProviderStatus(ReservationStatus.Active, null, Now, Now.AddMinutes(-3));

        Assert.Equal(expected, reservation.IsStale(Now.AddMinutes(minutesFromNow)));
    }

    [Fact]
    public void IsStale_WithoutATimestampOrBeforeAnyStampedDelivery_IsNeverStale()
    {
        var reservation = NewReservation();
        Assert.False(reservation.IsStale(Now.AddYears(-1)));

        reservation.ReceiveProviderStatus(ReservationStatus.Active, null, Now, Now.AddMinutes(-3));

        Assert.False(reservation.IsStale(null));
    }

    [Fact]
    public void ReceiveProviderStatus_ReadOnlyReservation_OnlyMirrorsTheStatusWithNoLocalEffect()
    {
        var reservation = ReadOnlyReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Committed, "ref-1", Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Active, reservation.Status);
        Assert.Null(reservation.ProviderReference);
        Assert.Equal(ReservationStatus.Committed, reservation.ProviderStatus);
        var @event = Assert.Single(reservation.Events.OfType<V1StockReservationProviderStatusMirroredDomainEvent>());
        Assert.Equal(V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Committed, @event.ProviderStatus);
        Assert.Single(reservation.Events);
    }

    [Fact]
    public void ReceiveProviderStatus_ReadOnlyReservationAlreadyMirroringThatStatus_IsANoOpWithoutAnEvent()
    {
        var reservation = ReadOnlyReservation();
        reservation.ReceiveProviderStatus(ReservationStatus.Released, null, Now);

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ReceiveProviderStatus(ReservationStatus.Released, null, Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Fact]
    public void ReceiveProviderStatus_ConfirmedOnAWritableReservation_CommitsIt()
    {
        var reservation = NewReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Committed, "ref-1", Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Committed, reservation.Status);
        Assert.Equal("ref-1", reservation.ProviderReference);
        Assert.Null(reservation.ProviderStatus);
    }

    [Fact]
    public void ReceiveProviderStatus_ConfirmedWithoutAReference_ReturnsProviderReferenceRequired()
    {
        var reservation = NewReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Committed, null, Now);

        Assert.Equal(StockReservationErrors.ProviderReferenceRequired.Key, result.Error!.Key);
        Assert.Equal(ReservationStatus.Active, reservation.Status);
    }

    [Fact]
    public void ReceiveProviderStatus_ReleasedOnAWritableReservation_ReleasesIt()
    {
        var reservation = NewReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Released, null, Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Released, reservation.Status);
    }

    [Fact]
    public void ReceiveProviderStatus_ExpiredOnAWritableReservation_ExpiresIt()
    {
        var reservation = NewReservation();

        var result = reservation.ReceiveProviderStatus(ReservationStatus.Expired, null, Now);

        Assert.False(result.IsFailure);
        Assert.Equal(ReservationStatus.Expired, reservation.Status);
    }

    [Fact]
    public void ReceiveProviderStatus_HeldOnAWritableReservation_ChangesNothingAndRaisesNothing()
    {
        var reservation = NewReservation();

        AssertRaisesAnEventExactlyWhenAFieldChanged(
            reservation, r => Assert.False(r.ReceiveProviderStatus(ReservationStatus.Active, null, Now).IsFailure));

        Assert.Empty(reservation.Events);
    }

    [Theory]
    [InlineData(ReservationStatus.Unknown)]
    [InlineData(ReservationStatus.RequiresReconciliation)]
    public void ReceiveProviderStatus_StatusTheProviderCannotReport_IsACallerBugAndThrows(ReservationStatus reported)
    {
        var reservation = NewReservation();

        Assert.Throws<ArgumentOutOfRangeException>(() => reservation.ReceiveProviderStatus(reported, null, Now));
    }
}
