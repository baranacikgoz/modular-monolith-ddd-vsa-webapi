using System.Globalization;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Localization;

public static class Setup
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services, string ResourcesPath)
    {
        return services.AddLocalization(options => options.ResourcesPath = ResourcesPath);
    }

    public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
    {
        var locOptions = app.ApplicationServices.GetRequiredService<IOptions<CustomLocalizationOptions>>().Value;

        app.UseRequestLocalization(opt =>
        {
            opt.DefaultRequestCulture = new RequestCulture(locOptions.DefaultCulture);
            opt.SupportedCultures = locOptions.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            opt.FallBackToParentCultures = true;
        });

        return app;
    }
}
