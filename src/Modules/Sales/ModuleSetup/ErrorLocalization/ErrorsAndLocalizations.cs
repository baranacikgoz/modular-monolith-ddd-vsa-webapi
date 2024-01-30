using Common.Core.Contracts;
using Common.Core.Interfaces;
using Microsoft.Extensions.Localization;
using Sales.Features.Stores.Domain.Errors;

namespace Sales.ModuleSetup.ErrorLocalization;

public static class ErrorsAndLocalizations
{
    public static IEnumerable<KeyValuePair<string, Func<IStringLocalizer, string>>> Get()
    {
        yield return new KeyValuePair<string, Func<IStringLocalizer, string>>(
            nameof(StoreErrors.ProductNotFound),
            localizer => localizer["Ürün bulunamadı."]
        );
    }
}
