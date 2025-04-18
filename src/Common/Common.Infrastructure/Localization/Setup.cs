using System.Globalization;
using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Localization;

public static class Setup
{
    public static IServiceCollection AddCommonResxLocalization(this IServiceCollection services)
    {
        return services.AddLocalization(options => options.ResourcesPath = "Resources");
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
