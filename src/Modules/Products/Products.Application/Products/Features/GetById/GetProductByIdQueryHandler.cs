using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Application.Products.Features.GetById;

public sealed class GetProductByIdQueryHandler<TDto>(ProductsDbContext dbContext) : IQueryHandler<GetProductByIdQuery<TDto>, TDto>
{
    public async Task<Result<TDto>> Handle(GetProductByIdQuery<TDto> request, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .Select(request.Selector)
            .SingleAsResultAsync(cancellationToken);
}
