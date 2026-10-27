using Inventory.Domain.StockReservations;
using Xunit;

namespace Inventory.Tests.StockReservations;

public class ProviderStatusMapTests
{
    private static readonly string[] KnownWords = ["held", "confirmed", "released", "expired"];

    [Theory]
    [InlineData("held", ReservationStatus.Active)]
    [InlineData("confirmed", ReservationStatus.Committed)]
    [InlineData("released", ReservationStatus.Released)]
    [InlineData("expired", ReservationStatus.Expired)]
    [InlineData("CONFIRMED", ReservationStatus.Committed)]
    [InlineData("  Released ", ReservationStatus.Released)]
    public void Resolve_KnownRawStatus_MapsItIgnoringCaseAndSurroundingSpace(string raw, ReservationStatus expected)
    {
        var resolution = ProviderStatusMap.Resolve(raw, out var status);

        Assert.Equal(ProviderStatusResolution.Mapped, resolution);
        Assert.Equal(expected, status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Resolve_NoStatus_IsTheOriginalConfirmationDelivery(string? raw)
    {
        var resolution = ProviderStatusMap.Resolve(raw, out var status);

        Assert.Equal(ProviderStatusResolution.Mapped, resolution);
        Assert.Equal(ReservationStatus.Committed, status);
    }

    [Theory]
    [InlineData("partially_picked")]
    [InlineData("0")]
    [InlineData("Committed")]
    public void Resolve_RawStatusOutsideTheDictionary_IsUnrecognizedAndNeverThrows(string raw)
    {
        // "Committed" is a status of this system, not a word of the provider's vocabulary.
        var resolution = ProviderStatusMap.Resolve(raw, out var status);

        Assert.Equal(ProviderStatusResolution.Unrecognized, resolution);
        Assert.Equal(ReservationStatus.Unknown, status);
    }

    [Fact]
    public void Resolve_KnownRawStatuses_NeverMapToAStatusTheProviderCannotReport()
    {
        var targets = KnownWords
            .Select(raw =>
            {
                ProviderStatusMap.Resolve(raw, out var status);
                return status;
            });

        Assert.DoesNotContain(ReservationStatus.Unknown, targets);
        Assert.DoesNotContain(ReservationStatus.RequiresReconciliation, targets);
    }
}
