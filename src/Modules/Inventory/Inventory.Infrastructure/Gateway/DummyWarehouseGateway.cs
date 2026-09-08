using Common.Domain.ResultMonad;
using Inventory.Application.Gateway;

namespace Inventory.Infrastructure.Gateway;

/// <summary>In-memory fake, always succeeds. Non-production only - enforced via <c>InventoryOptionsValidator</c>.</summary>
internal sealed class DummyWarehouseGateway : IWarehouseGateway
{
    public Task<Result<string>> RequestReleaseAsync(string productReference, int quantity, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result<string>.Success($"dummy-release:{Guid.NewGuid()}"));
    }

    public Task<Result<string>> ReconcileReleaseAsync(string productReference, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result<string>.Success($"dummy-reconcile:{Guid.NewGuid()}"));
    }
}
