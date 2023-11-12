using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Common.Core.Implementations;

public class ResultTranslator(
    IErrorTranslator errorTranslator,
    IHttpContextAccessor httpContextAccessor
    ) : IResultTranslator
{
    public IResult TranslateToMinimalApiResult<T>(Result<T> result, IStringLocalizer<IErrorTranslator> localizer)
        => result.Match(
            onSuccess: value => Results.Ok(value),
            onFailure: error => CreateProblemDetails(error, localizer)
        );

    public IResult TranslateToMinimalApiResult(Result result, IStringLocalizer<IErrorTranslator> localizer)
        => result.Match(
            onSuccess: () => Results.Ok(),
            onFailure: error => CreateProblemDetails(error, localizer)
        );

    private CustomProblemDetails CreateProblemDetails(Error error, IStringLocalizer<IErrorTranslator> localizer)
        => new()
        {
            Status = (int)error.StatusCode,
            Title = errorTranslator.Translate(error, localizer),
            Type = error.Key,
            Instance = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty,
            RequestId = httpContextAccessor.HttpContext?.TraceIdentifier ?? string.Empty,
            Errors = error.Errors ?? Enumerable.Empty<string>()
        };
}
