using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Inventory;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.InterModuleRequestHandlers;

public class GetStockLevelRequestHandler(IInventoryDbContext dbContext)
    : InterModuleRequestHandler<GetStockLevelRequest, GetStockLevelResponse>
{
    public override async Task<GetStockLevelResponse> HandleAsync(GetStockLevelRequest request, CancellationToken cancellationToken)
    {
        var quantityOnHand = await dbContext.StockLevels
            .AsNoTracking()
            .Where(s => s.ProductId == request.ProductId)
            .Select(s => (int?)s.QuantityOnHand)
            .SingleOrDefaultAsync(cancellationToken) ?? 0;

        var activeReservedQuantity = await dbContext.StockReservations
            .AsNoTracking()
            .Where(r => r.ProductId == request.ProductId && r.Status == ReservationStatus.Active)
            .SumAsync(r => (int?)r.Quantity, cancellationToken) ?? 0;

        return new GetStockLevelResponse(quantityOnHand, quantityOnHand - activeReservedQuantity);
    }
}
