using System.Collections.Frozen;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Interfaces;
using Microsoft.Extensions.Localization;

namespace Common.Core.EndpointFilters;

public static class StringLocalizerExtensions
{
    public static string LocalizeFromError(this IStringLocalizer localizer, Error error)
    {
        if (error.Arguments is null)
        {
            return localizer[error.Key];
        }

        return localizer[error.Key, error.Arguments];
    }
}