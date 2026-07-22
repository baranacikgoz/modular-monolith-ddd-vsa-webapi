using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using AspNetResult = Microsoft.AspNetCore.Http.IResult;

namespace Common.Application.EndpointFilters;

// In my opinion, success cases significantly outnumber failure cases.
// In this filters, no service needed for succes paths.
// So we don't have to eagerly load failure case's services. (on ctor as usual)
// That's why we're using the service provider to get the error translator and localizer if and only if failure case occur.
internal sealed class ResultToResponseTransformer(IServiceProvider serviceProvider, IWebHostEnvironment env)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        // An inner filter or the framework itself (e.g. a disabled RequireFeature check,
        // minimal-API parameter binding) can short-circuit before a Result is produced,
        // yielding an ASP.NET IResult. Our Result implements only the domain IResult, so
        // it never matches here — pass the short-circuit response through untouched.
        if (resultObj is AspNetResult shortCircuit)
        {
            return shortCircuit;
        }

        if (resultObj is not Result result)
        {
            throw new InvalidOperationException(
                $"{nameof(ResultToResponseTransformer)} can only be used with Result type.");
        }

        return result.Match(
            () => Results.NoContent(),
            error =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();

                var problemDetails = new ProblemDetails
                {
                    Status = (int)error.StatusCode,
                    Title = localizer.LocalizeFromError(error),
                    Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}"
                };

                problemDetails.AddErrorKey(error.Key);
                problemDetails.AddErrors(error.SubErrors ?? Array.Empty<string>());

                if (error.Value is not null)
                {
                    problemDetails.Extensions.TryAdd("detail", error.Value);
                }

                problemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
                problemDetails.Extensions.TryAdd("environment", env.EnvironmentName);

                return Results.Problem(problemDetails);
            }
        );
    }
}

internal sealed class ResultToCreatedResponseTransformer<T>(IServiceProvider serviceProvider, IWebHostEnvironment env)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        // See ResultToResponseTransformer above for why this check exists.
        if (resultObj is AspNetResult shortCircuit)
        {
            return shortCircuit;
        }

        if (resultObj is not Result<T> result)
        {
            throw new InvalidOperationException(
                $"{nameof(ResultToCreatedResponseTransformer<T>)} can only be used with Result<{nameof(T)}> type.");
        }

        return result.Match(
            value => Results.Created((Uri?)null, value),
            error =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();

                var problemDetails = new ProblemDetails
                {
                    Status = (int)error.StatusCode,
                    Title = localizer.LocalizeFromError(error),
                    Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}"
                };

                problemDetails.AddErrorKey(error.Key);
                problemDetails.AddErrors(error.SubErrors ?? Array.Empty<string>());

                if (error.Value is not null)
                {
                    problemDetails.Extensions.TryAdd("detail", error.Value);
                }

                problemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
                problemDetails.Extensions.TryAdd("environment", env.EnvironmentName);

                return Results.Problem(problemDetails);
            }
        );
    }
}

internal sealed class ResultToResponseTransformer<T>(IServiceProvider serviceProvider, IWebHostEnvironment env)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultObj = await next(context);

        // See ResultToResponseTransformer above for why this check exists.
        if (resultObj is AspNetResult shortCircuit)
        {
            return shortCircuit;
        }

        if (resultObj is not Result<T> result)
        {
            throw new InvalidOperationException(
                $"{nameof(ResultToResponseTransformer<T>)} can only be used with Result<{nameof(T)}> type.");
        }

        return result.Match(
            value => Results.Ok(value),
            error =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();

                var problemDetails = new ProblemDetails
                {
                    Status = (int)error.StatusCode,
                    Title = localizer.LocalizeFromError(error),
                    Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}"
                };

                problemDetails.AddErrorKey(error.Key);
                problemDetails.AddErrors(error.SubErrors ?? Array.Empty<string>());

                if (error.Value is not null)
                {
                    problemDetails.Extensions.TryAdd("detail", error.Value);
                }

                problemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
                problemDetails.Extensions.TryAdd("environment", env.EnvironmentName);

                return Results.Problem(problemDetails);
            }
        );
    }
}
