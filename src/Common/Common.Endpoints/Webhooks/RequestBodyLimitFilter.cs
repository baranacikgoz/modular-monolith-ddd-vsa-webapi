using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Common.Endpoints.Webhooks;

/// <summary>
///     Caps the request body of an anonymous, internet-facing receiver (a partner webhook) before the first
///     byte is read. A declared <c>Content-Length</c> over the cap is refused with 413 outright; a chunked body is
///     cut off by the server at the cap. The limit is resolved per request so it can come from options.
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
    /// <summary>Applies <see cref="RequestBodyLimitFilter" /> to every endpoint of the builder (a group or a single route).</summary>
    public static TBuilder LimitRequestBody<TBuilder>(this TBuilder builder, Func<HttpContext, long> maxBodyBytes)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.AddEndpointFilter(new RequestBodyLimitFilter(maxBodyBytes));
    }
}
