using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Http;

namespace Common.Core.Contracts;

public interface IResultTranslator
{
    Microsoft.AspNetCore.Http.IResult TranslateToMinimalApiResult<T>(Result<T> result);
    Microsoft.AspNetCore.Http.IResult TranslateToMinimalApiResult(Result result);
}
