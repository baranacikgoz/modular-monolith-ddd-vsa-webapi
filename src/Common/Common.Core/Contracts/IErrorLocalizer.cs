using Common.Core.Contracts.Results;
using Microsoft.Extensions.Localization;

namespace Common.Core.Contracts;

public interface IErrorLocalizer
{
    string Localize(Error error, IStringLocalizer<IErrorLocalizer> stringLocalizer);
}
