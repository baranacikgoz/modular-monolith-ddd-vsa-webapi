using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Core.Implementations;

public class ResultTranslator(
    IErrorTranslator errorTranslator,
    IHttpContextAccessor httpContextAccessor
    ) : IResultTranslator
{
    public IResult TranslateToMinimalApiResult<T>(Result<T> result)
    {
        return result.IsSucceeded
            ? Results.Ok(result.Value)
            : CreateProblemDetails(result.Failure!);
    }

    public IResult TranslateToMinimalApiResult(Result result)
    {
        return result.IsSucceeded
            ? Results.Ok()
            : CreateProblemDetails(result.Failure!);
    }

    private CustomProblemDetails CreateProblemDetails(Failure failure)
    {
        var localizedErrorMessage = errorTranslator.Translate(failure);

        return new CustomProblemDetails
        {
            Status = (int)failure.StatusCode,
            Title = localizedErrorMessage,
            Type = failure.GetType().FullName ?? string.Empty,
            Instance = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty,
            TraceId = httpContextAccessor.HttpContext?.TraceIdentifier ?? string.Empty,
            Errors = failure.Errors ?? Enumerable.Empty<string>()
        };
    }
}
