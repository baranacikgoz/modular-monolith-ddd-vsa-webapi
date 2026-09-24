using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Common.Endpoints.Webhooks;

/// <summary>
///     Endpoint metadata added by <see cref="RequestBodyLimitEndpointExtensions.LimitRequestBody{TBuilder}" /> and
///     enforced by <see cref="RequestBodyLimitMiddleware" />. The limit is resolved per request so it can come from
///     options.
/// </summary>
public sealed class RequestBodyLimitMetadata(Func<HttpContext, long> maxBodyBytes)
{
    public Func<HttpContext, long> MaxBodyBytes { get; } = maxBodyBytes;
}

/// <summary>
///     Caps the request body of an endpoint carrying <see cref="RequestBodyLimitMetadata" /> before the endpoint runs,
///     so before minimal API parameter binding reads a single byte (an endpoint filter runs after binding and would be
///     too late for a DTO-bound body). A declared <c>Content-Length</c> over the cap is refused with 413 outright; a
///     chunked body is cut off by the server at the cap. Runs after routing: the Host registers it in
///     <c>UseInfrastructure</c>.
/// </summary>
public sealed class RequestBodyLimitMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<RequestBodyLimitMetadata>() is not { } metadata)
        {
            return next(context);
        }

        var limit = metadata.MaxBodyBytes(context);

        if (context.Request.ContentLength > limit)
        {
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            return Task.CompletedTask;
        }

        var bodySizeFeature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
        if (bodySizeFeature is { IsReadOnly: false })
        {
            bodySizeFeature.MaxRequestBodySize = limit;
        }

        return next(context);
    }
}
