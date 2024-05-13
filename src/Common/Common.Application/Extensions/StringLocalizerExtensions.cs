using Common.Domain.ResultMonad;
using Microsoft.Extensions.Localization;

namespace Common.Application.Extensions;

public static class StringLocalizerExtensions
{
    public static string LocalizeFromError(this IStringLocalizer localizer, Error error)
    {
        if (error.ParameterName is null)
        {
            // Probably a custom business rule error like "Store exceeds product limit."
            return localizer[error.Key];
        }

        if (error.Value is null)
        {
            throw new InvalidOperationException("Value should not be null when parameter name is not null.");
        }

        // Assume parameter is "Store" and key is "NotFound" and value is "123".
        // Then it will generate a string like "Store (123) is not found."
        // '({0}) is not found.' will come from the localization resource file as the value of NotFound key.
        return $"{localizer[error.ParameterName]} {localizer[error.Key, error.Value]}";
    }
}
