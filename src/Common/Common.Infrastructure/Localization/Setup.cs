using System.Globalization;
using Common.Application.Options;
using Common.Application.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResXGenerator.Registration;

namespace Common.Infrastructure.Localization;

public static class Setup
{
    public static IServiceCollection AddCommonResxLocalization(this IServiceCollection services)
    {
        return services
            // Same Accept-Language → CurrentUICulture source as IResxLocalizer; resolves the search config per request.
            .AddSingleton<ISearchLanguageResolver, SearchLanguageResolver>()
            .AddLocalization()
            .UsingResXGenerator();
    }

    public static IApplicationBuilder UseCommonResxLocalization(this IApplicationBuilder app)
    {
        var locOptions = app.ApplicationServices.GetRequiredService<IOptions<ResxLocalizationOptions>>().Value;

        app.UseRequestLocalization(opt =>
        {
            opt.DefaultRequestCulture = new RequestCulture(locOptions.DefaultCulture);
            opt.SupportedCultures = locOptions.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            opt.SupportedUICultures = locOptions.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            opt.FallBackToParentCultures = true;
            opt.FallBackToParentUICultures = true;
            opt.RequestCultureProviders.Clear();
            opt.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
            opt.ApplyCurrentCultureToResponseHeaders = true;
        });

        return app;
    }
}
