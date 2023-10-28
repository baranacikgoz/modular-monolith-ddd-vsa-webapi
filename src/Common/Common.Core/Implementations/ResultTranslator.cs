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
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CreateProblemDetails(result.Error!);
    }

    public IResult TranslateToMinimalApiResult(Result result)
    {
        return result.IsSuccess
            ? Results.Ok()
            : CreateProblemDetails(result.Error!);
    }

    private CustomProblemDetails CreateProblemDetails(Error error)
    {
        var localizedErrorMessage = errorTranslator.Translate(error);

        return new CustomProblemDetails
        {
            Status = (int)error.StatusCode,
            Title = localizedErrorMessage,
            Type = error.Key,
            Instance = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty,
            TraceId = httpContextAccessor.HttpContext?.TraceIdentifier ?? string.Empty,
            Errors = error.Errors ?? Enumerable.Empty<string>()
        };
    }
}
