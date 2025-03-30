using Common.Application.DTOs;
using Common.Application.Queries.EventHistory;
using Common.Application.Queries.Pagination;
using Common.Domain.ResultMonad;
using MediatR;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed class GetProductEventHistoryQueryHandler(ProductsDbContext dbContext) : EventHistoryQueryHandler<Product>(dbContext)
{
}
