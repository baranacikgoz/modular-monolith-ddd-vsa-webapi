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
            // Assume parameter is "Store" and key is "NotFound".
            // Then it will generate a string like "Store is not found."
            return $"{localizer[error.ParameterName]} {localizer[error.Key]}";
        }

        // Assume parameter is "Store" and key is "NotFound" and value is "123".
        // Then it will generate a string like "Store (123) is not found."
        // The value is composed here, not through a "{0}" in the resource text: the branch above uses the same
        // resource without a value, and a placeholder there would reach the client unformatted.
        return $"{localizer[error.ParameterName]} ({error.Value}) {localizer[error.Key]}";
    }
}
