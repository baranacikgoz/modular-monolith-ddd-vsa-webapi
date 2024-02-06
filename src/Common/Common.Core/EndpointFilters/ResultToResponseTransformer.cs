using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Interfaces;
using Common.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Common.Core.EndpointFilters;

// In my opinion, success cases significantly outnumber failure cases.
// In this filters, no service needed for succes paths.
// So we don't have to eagerly load failure case's services. (on ctor as usual)
// That's why we're using the service provider to get the error translator and localizer if and only if failure case occur.
internal sealed class ResultToResponseTransformer(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        if (resultObj is not Result result)
        {
            throw new InvalidOperationException($"{nameof(ResultToResponseTransformer)} can only be used with Result type.");
        }

        return result.Match(
            onSuccess: () => Results.NoContent(),
            onFailure: error =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                var problemDetailsFactory = serviceProvider.GetRequiredService<IProblemDetailsFactory>();

                return problemDetailsFactory.Create(
                    status: (int)error.StatusCode,
                    title: localizer.LocalizeFromError(error),
                    type: error.Key,
                    instance: context.HttpContext?.Request.Path ?? string.Empty,
                    requestId: context.HttpContext?.TraceIdentifier ?? string.Empty,
                    errors: error.Errors ?? Enumerable.Empty<string>()
                );
            }
        );
    }
}

internal sealed class ResultToResponseTransformer<T>(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        if (resultObj is not Result<T> result)
        {
            throw new InvalidOperationException($"{nameof(ResultToResponseTransformer<T>)} can only be used with Result<{nameof(T)}> type.");
        }

        return result.Match(
            onSuccess: value => Results.Ok(value),
            onFailure: error =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                var problemDetailsFactory = serviceProvider.GetRequiredService<IProblemDetailsFactory>();

                return problemDetailsFactory.Create(
                    status: (int)error.StatusCode,
                    title: localizer.LocalizeFromError(error),
                    type: error.Key,
                    instance: context.HttpContext?.Request.Path ?? string.Empty,
                    requestId: context.HttpContext?.TraceIdentifier ?? string.Empty,
                    errors: error.Errors ?? Enumerable.Empty<string>()
                );
            }
        );
    }
}
