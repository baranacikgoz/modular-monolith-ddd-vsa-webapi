using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Core.EndpointFilters;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder TransformResultToOkResponse(this RouteHandlerBuilder builder)
        => builder.AddEndpointFilter<ResultToResponseTransformer>();
    public static RouteHandlerBuilder TransformResultTo<T>(this RouteHandlerBuilder builder)
        => builder.AddEndpointFilter<ResultToResponseTransformer<T>>();
}
