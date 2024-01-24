using System.Collections.Frozen;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Interfaces;
using Microsoft.Extensions.Localization;

namespace Host.Infrastructure;

public class AggregatedErrorLocalizer : IErrorLocalizer
{
    // See https://learn.microsoft.com/en-us/dotnet/api/system.collections.frozen.frozendictionary-2?view=net-8.0
    private readonly FrozenDictionary<string, Func<IStringLocalizer, string>> _aggregatedErrorsAndLocalizations;
    public AggregatedErrorLocalizer(IEnumerable<KeyValuePair<string, Func<IStringLocalizer, string>>> errorsAndLocalizations)
    {
        // The service will be singleton, so don't worry about this operation being expensive.
        _aggregatedErrorsAndLocalizations = errorsAndLocalizations
                                            .ToDictionary(x => x.Key, x => x.Value)
                                            .ToFrozenDictionary();
    }

    public string Localize(Error error, IStringLocalizer localizer)
    {
        if (_aggregatedErrorsAndLocalizations.TryGetValue(error.Key, out var localizerFunc))
        {
            return localizerFunc(localizer);
        }

        throw new NotImplementedException($"Localization of error '{error.Key}' is not implemented.");
    }
}
