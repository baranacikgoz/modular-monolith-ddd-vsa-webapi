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
    {
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CreateProblemDetails(result.Error!, localizer);
    }

    public IResult TranslateToMinimalApiResult(Result result, IStringLocalizer<IErrorTranslator> localizer)
    {
        return result.IsSuccess
            ? Results.Ok()
            : CreateProblemDetails(result.Error!, localizer);
    }
    private CustomProblemDetails CreateProblemDetails(Error error, IStringLocalizer<IErrorTranslator> localizer)
    {
        var localizedErrorMessage = errorTranslator.Translate(error, localizer);

        return new CustomProblemDetails
        {
            Status = (int)error.StatusCode,
            Title = localizedErrorMessage,
            Type = error.Key,
            Instance = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty,
            RequestId = httpContextAccessor.HttpContext?.TraceIdentifier ?? string.Empty,
            Errors = error.Errors ?? Enumerable.Empty<string>()
        };
    }
}
