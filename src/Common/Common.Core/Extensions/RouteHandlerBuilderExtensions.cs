using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Core.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder TransformResultToNoContentResponse(this RouteHandlerBuilder builder)
        => builder.AddEndpointFilter<ResultToResponseTransformer>();
    public static RouteHandlerBuilder TransformResultTo<T>(this RouteHandlerBuilder builder)
        => builder.AddEndpointFilter<ResultToResponseTransformer<T>>();
}
