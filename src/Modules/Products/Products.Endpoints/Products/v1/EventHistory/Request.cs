using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.EventHistory;

public sealed record Request : PaginationRequest
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; set; }
}
