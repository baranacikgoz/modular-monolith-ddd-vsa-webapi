namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     The outbound connection to a warehouse host was refused because the host is, or resolves to, a non public address
///     (see <c>WarehouseAddressPolicy</c>). Nothing was sent. The HTTP handler wraps what its connect callback throws in an
///     <see cref="HttpRequestException" />, so a caller recognises this cause with <see cref="IsCauseOf" />.
/// </summary>
internal sealed class WarehouseAddressRefusedException : InvalidOperationException
{
    public WarehouseAddressRefusedException()
    {
    }

    public WarehouseAddressRefusedException(string message) : base(message)
    {
    }

    public WarehouseAddressRefusedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static WarehouseAddressRefusedException ForHost(string host)
    {
        return new WarehouseAddressRefusedException($"The warehouse host '{host}' is, or resolves to, a non public address.");
    }

    public static bool IsCauseOf(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current is WarehouseAddressRefusedException)
            {
                return true;
            }
        }

        return false;
    }
}
