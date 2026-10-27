using Common.Application.Localization.Resources;
using Common.Application.Pagination;

namespace Inventory.Endpoints.WarehouseConnections.v1.My.Search;

public sealed record Request : PaginationRequest;

/// <summary>Paging only: page number or cursor (<c>After</c>), page size, optional total.</summary>
public sealed class RequestValidator(IResxLocalizer localizer) : PaginationRequestValidator<Request>(localizer);
