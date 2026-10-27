using Inventory.Domain.StockReservations;
using Xunit;

namespace Inventory.Tests.StockReservations;

public class ReservationStatusTransitionsTests
{
    private static readonly HashSet<(ReservationStatus From, ReservationStatus To)> LegalMoves =
    [
        (ReservationStatus.Active, ReservationStatus.Committed),
        (ReservationStatus.Active, ReservationStatus.Released),
        (ReservationStatus.Active, ReservationStatus.Expired),
        (ReservationStatus.Committed, ReservationStatus.RequiresReconciliation)
    ];

    public static TheoryData<ReservationStatus, ReservationStatus> AllPairs()
    {
        var pairs = new TheoryData<ReservationStatus, ReservationStatus>();
        foreach (var from in Enum.GetValues<ReservationStatus>())
        {
            foreach (var to in Enum.GetValues<ReservationStatus>())
            {
                pairs.Add(from, to);
            }
        }

        return pairs;
    }

    [Theory]
    [MemberData(nameof(AllPairs))]
    public void CanTransition_EveryPairOfStatuses_AllowsExactlyTheDocumentedMoves(ReservationStatus from, ReservationStatus to)
    {
        Assert.Equal(LegalMoves.Contains((from, to)), ReservationStatusTransitions.CanTransition(from, to));
    }

    [Fact]
    public void CanTransition_UndefinedStatus_IsNeverALegalMove()
    {
        var undefined = (ReservationStatus)999;

        Assert.False(ReservationStatusTransitions.CanTransition(undefined, ReservationStatus.Committed));
        Assert.False(ReservationStatusTransitions.CanTransition(ReservationStatus.Active, undefined));
    }
}
