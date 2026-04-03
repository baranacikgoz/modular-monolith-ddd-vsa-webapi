using Common.Application.Localization.Resources;
using Common.Application.Pagination;

namespace Products.Endpoints.Stores.v1.My.History;

public sealed record Request : PaginationRequest
{
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
    }
}
