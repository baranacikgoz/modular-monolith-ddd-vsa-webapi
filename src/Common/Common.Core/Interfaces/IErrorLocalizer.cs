using Common.Core.Contracts.Results;
using Microsoft.Extensions.Localization;

namespace Common.Core.Interfaces;

public interface IErrorLocalizer
{
    string Localize(Error error, IStringLocalizer localizer);
}
