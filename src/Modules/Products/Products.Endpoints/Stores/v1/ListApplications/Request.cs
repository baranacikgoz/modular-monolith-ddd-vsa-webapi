using Common.Application.Localization.Resources;
using Common.Application.Pagination;

namespace Products.Endpoints.Stores.v1.ListApplications;

public sealed record Request : PaginationRequest;

public sealed class RequestValidator(IResxLocalizer localizer) : PaginationRequestValidator<Request>(localizer);
