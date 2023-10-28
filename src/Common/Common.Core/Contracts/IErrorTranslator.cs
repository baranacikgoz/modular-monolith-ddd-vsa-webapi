using Common.Core.Contracts.Results;
using Common.Core.Implementations;
using Microsoft.Extensions.Localization;

namespace Common.Core.Contracts;

public interface IErrorTranslator
{
    string Translate(Error error, IStringLocalizer<IErrorTranslator> localizer);
}
