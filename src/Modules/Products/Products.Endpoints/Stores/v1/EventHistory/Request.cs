using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.EventHistory;

public sealed record Request : PaginationRequest
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public StoreId Id { get; set; }
}
