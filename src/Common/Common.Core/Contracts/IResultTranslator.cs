using Common.Core.Contracts.Results;
using Microsoft.Extensions.Localization;

namespace Common.Core.Contracts;

public interface IResultTranslator
{
    Microsoft.AspNetCore.Http.IResult TranslateToMinimalApiResult<T>(Result<T> result, IStringLocalizer<IErrorTranslator> localizer);
    Microsoft.AspNetCore.Http.IResult TranslateToMinimalApiResult(Result result, IStringLocalizer<IErrorTranslator> localizer);
}
