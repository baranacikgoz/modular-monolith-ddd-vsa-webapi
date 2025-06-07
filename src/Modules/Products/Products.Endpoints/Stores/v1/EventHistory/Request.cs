using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.EventHistory;

public sealed record Request : PaginationRequest
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public StoreId Id { get; set; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
    }
}
