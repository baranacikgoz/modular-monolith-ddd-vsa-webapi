using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Inventory;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.InterModuleRequestHandlers;

public class GetStockLevelRequestHandler(IInventoryDbContext dbContext)
    : InterModuleRequestHandler<GetStockLevelRequest, GetStockLevelResponse>
{
    public override async Task<GetStockLevelResponse> HandleAsync(GetStockLevelRequest request, CancellationToken cancellationToken)
    {
        // A product nobody created a stock level for yet has no stock, which is an answer, not a failure.
        var quantityOnHand = (await dbContext.StockLevels
                .AsNoTracking()
                .TagWith(nameof(HandleAsync), request.ProductId)
                .Where(s => s.ProductId == request.ProductId)
                .Select(s => new { s.QuantityOnHand })
                .SingleAsResultAsync(nameof(StockLevel), cancellationToken))
            .Match(stock => stock.QuantityOnHand, _ => 0);

        var activeReservedQuantity = await dbContext.StockReservations
            .AsNoTracking()
            .TagWith(nameof(HandleAsync), request.ProductId)
            .Where(r => r.ProductId == request.ProductId && r.Status == ReservationStatus.Active)
            .SumAsync(r => (int?)r.Quantity, cancellationToken) ?? 0;

        return new GetStockLevelResponse(quantityOnHand, quantityOnHand - activeReservedQuantity);
    }
}
