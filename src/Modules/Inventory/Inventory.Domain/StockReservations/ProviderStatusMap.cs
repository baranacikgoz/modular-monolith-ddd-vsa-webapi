using System.Collections.Frozen;

namespace Inventory.Domain.StockReservations;

/// <summary>What <see cref="ProviderStatusMap.Resolve" /> decided about one raw provider status.</summary>
public enum ProviderStatusResolution
{
    /// <summary>A word of the provider's documented vocabulary, mapped to a <see cref="ReservationStatus" />.</summary>
    Mapped,

    /// <summary>
    ///     A word nobody mapped. The reservation's status must not change: the raw word goes to the audit trail and the
    ///     reservation turns read-only. Never an exception and never a failed delivery.
    /// </summary>
    Unrecognized
}

/// <summary>
///     Translates the warehouse system's raw status word into a <see cref="ReservationStatus" />. The single place the
///     provider's vocabulary is spelled; the rest of the module only ever sees the mapped status.
/// </summary>
public static class ProviderStatusMap
{
    private static readonly FrozenDictionary<string, ReservationStatus> Words =
        new Dictionary<string, ReservationStatus>(StringComparer.OrdinalIgnoreCase)
        {
            ["held"] = ReservationStatus.Active,
            ["confirmed"] = ReservationStatus.Committed,
            ["released"] = ReservationStatus.Released,
            ["expired"] = ReservationStatus.Expired
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    ///     A delivery without a status is the original confirmation delivery, the only kind a provider sent before statuses
    ///     were reported. A word outside the dictionary yields <see cref="ProviderStatusResolution.Unrecognized" /> and
    ///     <see cref="ReservationStatus.Unknown" />.
    /// </summary>
    public static ProviderStatusResolution Resolve(string? rawStatus, out ReservationStatus status)
    {
        if (string.IsNullOrWhiteSpace(rawStatus))
        {
            status = ReservationStatus.Committed;
            return ProviderStatusResolution.Mapped;
        }

        if (Words.TryGetValue(rawStatus.Trim(), out status))
        {
            return ProviderStatusResolution.Mapped;
        }

        status = ReservationStatus.Unknown;
        return ProviderStatusResolution.Unrecognized;
    }
}
