using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.Extensions.Localization;

namespace Common.Core.Implementations;

public class LocalizedErrorTranslator : IErrorTranslator
{
    private static readonly Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> _aggregatedErrorsAndMessages = new();

    public LocalizedErrorTranslator(params Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>>[] errorKeysToMessages)
    {
        // The service will be singleton, so this will be called only once at the startup (n^2 complexity) will not be a problem.
        foreach (var errorKeyToMessage in errorKeysToMessages)
        {
            foreach (var (key, value) in errorKeyToMessage)
            {
                _aggregatedErrorsAndMessages.Add(key, value);
            }
        }
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
