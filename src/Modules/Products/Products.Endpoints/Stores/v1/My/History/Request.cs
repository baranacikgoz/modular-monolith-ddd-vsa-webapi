using Common.Application.Localization;
using Common.Application.Pagination;
using Microsoft.Extensions.Localization;

namespace Products.Endpoints.Stores.v1.My.History;

public sealed record Request : PaginationRequest
{
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
    }
}
