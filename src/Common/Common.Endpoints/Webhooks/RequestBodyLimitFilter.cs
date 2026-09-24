using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Common.Endpoints.Webhooks;

/// <summary>
///     Endpoint-filter form of the body cap. An endpoint filter runs after minimal API parameter binding, so this only
///     protects an endpoint that reads the raw body itself (<c>HttpContext</c> parameter, no bound body): for a
///     DTO-bound body the bytes are already read and the server limit is read-only by the time it runs. Prefer
///     <see cref="RequestBodyLimitEndpointExtensions.LimitRequestBody{TBuilder}" />, which enforces the cap in
///     <see cref="RequestBodyLimitMiddleware" /> before binding.
/// </summary>
public sealed class RequestBodyLimitFilter(Func<HttpContext, long> maxBodyBytes) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var limit = maxBodyBytes(httpContext);

        if (httpContext.Request.ContentLength > limit)
        {
            return Results.StatusCode(StatusCodes.Status413PayloadTooLarge);
        }

        var bodySizeFeature = httpContext.Features.Get<IHttpMaxRequestBodySizeFeature>();
        if (bodySizeFeature is { IsReadOnly: false })
        {
            bodySizeFeature.MaxRequestBodySize = limit;
        }

        return await next(context);
    }
}

public static class RequestBodyLimitEndpointExtensions
{
    /// <summary>
    ///     Caps the request body of every endpoint of the builder (a group or a single route) before binding:
    ///     adds <see cref="RequestBodyLimitMetadata" />, enforced by <see cref="RequestBodyLimitMiddleware" />.
    /// </summary>
    public static TBuilder LimitRequestBody<TBuilder>(this TBuilder builder, Func<HttpContext, long> maxBodyBytes)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithMetadata(new RequestBodyLimitMetadata(maxBodyBytes));
    }
}
