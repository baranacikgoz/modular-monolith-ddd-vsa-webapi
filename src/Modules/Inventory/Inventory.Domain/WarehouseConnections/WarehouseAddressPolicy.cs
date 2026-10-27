using System.Net;
using System.Net.Sockets;

namespace Inventory.Domain.WarehouseConnections;

/// <summary>
///     Which addresses a warehouse connection may point at. The base address of a connection is typed in by a caller and the
///     server then calls it, so an address that leads into the server's own host or network (a loopback or private range, the
///     link-local range cloud metadata services answer on, unique-local IPv6) is refused.
///     <para>
///         Two places apply it: the create request checks the address as typed (<see cref="IsPublicHost" />), and the
///         outbound connection checks every address a host name resolves to (<see cref="IsPublicAddress" />), because a name
///         can point anywhere by the time it is called. Pure rules, no lookup.
///     </para>
/// </summary>
public static class WarehouseAddressPolicy
{
    private const string LocalhostLabel = "localhost";

    /// <summary>
    ///     False for <c>localhost</c> and its subdomains, and for an address literal in a non public range. Any other name
    ///     passes here; where it resolves to is checked on connect.
    /// </summary>
    public static bool IsPublicHost(Uri url)
    {
        ArgumentNullException.ThrowIfNull(url);

        var host = url.Host.TrimEnd('.');
        if (host.Length == 0
            || host.Equals(LocalhostLabel, StringComparison.OrdinalIgnoreCase)
            || host.EndsWith("." + LocalhostLabel, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return !IPAddress.TryParse(host.Trim('[', ']'), out var literal) || IsPublicAddress(literal);
    }

    /// <summary>
    ///     False for loopback, unspecified, private (10/8, 172.16/12, 192.168/16), shared address space (100.64/10),
    ///     link-local (169.254/16, fe80::/10), unique-local (fc00::/7), benchmarking, multicast and reserved ranges. An IPv4
    ///     address carried inside an IPv6 one (mapped, NAT64, 6to4) is judged as that IPv4 address.
    /// </summary>
    public static bool IsPublicAddress(IPAddress address)
    {
        ArgumentNullException.ThrowIfNull(address);

        if (address.IsIPv4MappedToIPv6)
        {
            address = address.MapToIPv4();
        }

        var bytes = address.GetAddressBytes();
        return address.AddressFamily switch
        {
            AddressFamily.InterNetwork => IsPublicIPv4(bytes),
            AddressFamily.InterNetworkV6 => IsPublicIPv6(bytes),
            _ => false
        };
    }

    private static bool IsPublicIPv4(ReadOnlySpan<byte> b)
    {
        return !(b[0] == 0                                  // 0.0.0.0/8, includes the unspecified address
                 || b[0] == 10                              // 10.0.0.0/8
                 || (b[0] == 100 && (b[1] & 0xC0) == 64)    // 100.64.0.0/10, shared address space
                 || b[0] == 127                             // 127.0.0.0/8
                 || (b[0] == 169 && b[1] == 254)            // 169.254.0.0/16, link-local
                 || (b[0] == 172 && (b[1] & 0xF0) == 16)    // 172.16.0.0/12
                 || (b[0] == 192 && b[1] == 0 && b[2] == 0) // 192.0.0.0/24
                 || (b[0] == 192 && b[1] == 168)            // 192.168.0.0/16
                 || (b[0] == 198 && (b[1] & 0xFE) == 18)    // 198.18.0.0/15, benchmarking
                 || b[0] >= 224);                           // multicast, reserved and broadcast
    }

    private static bool IsPublicIPv6(ReadOnlySpan<byte> b)
    {
        if (b[..12].IndexOfAnyExcept((byte)0) < 0)
        {
            return false; // ::/96: unspecified, loopback and the deprecated IPv4-compatible range
        }

        if ((b[0] & 0xFE) == 0xFC)
        {
            return false; // fc00::/7, unique-local
        }

        if (b[0] == 0xFE && (b[1] & 0x80) == 0x80)
        {
            return false; // fe80::/10 link-local and fec0::/10 site-local
        }

        if (b[0] == 0xFF)
        {
            return false; // ff00::/8, multicast
        }

        if (b[..8].SequenceEqual(stackalloc byte[] { 0x00, 0x64, 0xFF, 0x9B, 0, 0, 0, 0 }) && b[8..12].IndexOfAnyExcept((byte)0) < 0)
        {
            return IsPublicIPv4(b[12..]); // 64:ff9b::/96, NAT64: the last four bytes are the IPv4 address
        }

        if (b[..6].SequenceEqual(stackalloc byte[] { 0x00, 0x64, 0xFF, 0x9B, 0x00, 0x01 }))
        {
            return false; // 64:ff9b:1::/48, local use NAT64
        }

        if (b[0] == 0x20 && b[1] == 0x02)
        {
            return IsPublicIPv4(b[2..6]); // 2002::/16, 6to4: bytes 2 to 5 are the IPv4 address
        }

        if (b[0] == 0x20 && b[1] == 0x01 && b[2] == 0x00 && b[3] == 0x00)
        {
            return false; // 2001::/32, Teredo
        }

        return !(b[0] == 0x01 && b[1] == 0x00 && b[2..8].IndexOfAnyExcept((byte)0) < 0); // 100::/64, discard only
    }
}
