namespace Inventory.Application.Gateway;

/// <summary>
///     A gateway call ended without an answer (timeout, dropped connection), so what the warehouse did with it is unknown. Not
///     a <c>Result</c> failure on purpose: a failure is a definite outcome the caller may act on by retrying, and this one is
///     not. The caller keeps whatever marker it persisted before the call and resolves the outcome with
///     <see cref="IWarehouseGateway.ReconcileReleaseAsync" />.
/// </summary>
public sealed class WarehouseOutcomeUnknownException : Exception
{
    public WarehouseOutcomeUnknownException()
    {
    }

    public WarehouseOutcomeUnknownException(string message) : base(message)
    {
    }

    public WarehouseOutcomeUnknownException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
