using System.Collections.Frozen;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.Extensions.Localization;

namespace Host.Infrastructure;

public class LocalizedErrorTranslator : IErrorTranslator
{
    // See https://learn.microsoft.com/en-us/dotnet/api/system.collections.frozen.frozendictionary-2?view=net-8.0
    private readonly FrozenDictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> _aggregatedErrorsAndMessages;
    public LocalizedErrorTranslator(
       params IEnumerable<KeyValuePair<string, Func<IStringLocalizer<IErrorTranslator>, string>>>[] errorKeysToMessages)
    {
        // The service will be singleton, so don't worry about this operation being expensive.
        _aggregatedErrorsAndMessages = errorKeysToMessages
                                        .SelectMany(x => x)
                                        .ToDictionary(x => x.Key, x => x.Value)
                                        .ToFrozenDictionary();
    }

    public string Translate(Error error, IStringLocalizer<IErrorTranslator> localizer)
    {
        if (_aggregatedErrorsAndMessages.TryGetValue(error.Key, out var message))
        {
            return message(localizer);
        }

        throw new NotImplementedException($"Error key {error.Key} is not implemented.");
    }
}
