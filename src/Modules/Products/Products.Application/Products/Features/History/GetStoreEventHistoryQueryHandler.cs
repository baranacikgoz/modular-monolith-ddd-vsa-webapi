using Common.Application.DTOs;
using Common.Application.Persistence;
using Common.Application.Queries.EventHistory;
using Common.Application.Queries.Pagination;
using Common.Domain.ResultMonad;
using MediatR;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed class GetProductEventHistoryQueryHandler(ProductsDbContext dbContext) : IRequestHandler<GetProductEventHistoryQuery, Result<PaginationResult<EventDto>>>
{
    public async Task<Result<PaginationResult<EventDto>>> Handle(GetProductEventHistoryQuery request, CancellationToken cancellationToken)
        => await dbContext.GetEventHistoryAsync(request, cancellationToken);
}
