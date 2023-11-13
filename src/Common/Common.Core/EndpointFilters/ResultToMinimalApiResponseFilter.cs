using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Common.Core.EndpointFilters;

// In my opinion, success cases significantly outnumber failure cases.
// In this filters, no service needed for succes paths.
// So we don't have to eagerly load failure case's services. (on ctor as usual)
// That's why we're using the service provider to get the error translator and localizer if and only if failure case occur.
public sealed class ResultToMinimalApiResponseFilter(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        if (resultObj is not Result result)
        {
            throw new InvalidOperationException($"{nameof(ResultToMinimalApiResponseFilter)} can only be used with Result type.");
        }

        return result.Match(
            onSuccess: () => Results.Ok(),
            onFailure: error =>
            {
                var errorTranslator = serviceProvider.GetRequiredService<IErrorTranslator>();
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<IErrorTranslator>>();

                return new CustomProblemDetails()
                {
                    Status = (int)error.StatusCode,
                    Title = errorTranslator.Translate(error, localizer),
                    Type = error.Key,
                    Instance = context.HttpContext?.Request.Path ?? string.Empty,
                    RequestId = context.HttpContext?.TraceIdentifier ?? string.Empty,
                    Errors = error.Errors ?? Enumerable.Empty<string>()
                };
            }
        );
    }
}

public sealed class ResultToMinimalApiResponseFilter<T>(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        if (resultObj is not Result<T> result)
        {
            throw new InvalidOperationException($"{nameof(ResultToMinimalApiResponseFilter<T>)} can only be used with Result<{nameof(T)}> type.");
        }

        return result.Match(
            onSuccess: value => Results.Ok(value),
            onFailure: error =>
            {
                var errorTranslator = serviceProvider.GetRequiredService<IErrorTranslator>();
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<IErrorTranslator>>();

                return new CustomProblemDetails()
                {
                    Status = (int)error.StatusCode,
                    Title = errorTranslator.Translate(error, localizer),
                    Type = error.Key,
                    Instance = context.HttpContext?.Request.Path ?? string.Empty,
                    RequestId = context.HttpContext?.TraceIdentifier ?? string.Empty,
                    Errors = error.Errors ?? Enumerable.Empty<string>()
                };
            }
        );
    }
}
