using Asp.Versioning.ApiExplorer;
using Common.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal sealed class ConfigureSwaggerOptions(
    IApiVersionDescriptionProvider provider,
    IOptions<OpenApiOptions> openApiOptionsProvider
) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter your Bearer token without 'Bearer ' prefix.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

        options.AddSecurityRequirement(document =>
        {
            var schemeReference = new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document);

            return new OpenApiSecurityRequirement { [schemeReference] = [] };
        });
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = openApiOptionsProvider.Value.Title,
            Version = description.ApiVersion.ToString(),
            Description = openApiOptionsProvider.Value.Description,
            Contact = new OpenApiContact
            {
                Name = openApiOptionsProvider.Value.ContactName, Email = openApiOptionsProvider.Value.ContactEmail
            },
            License = new OpenApiLicense { Name = openApiOptionsProvider.Value.LicenseName }
        };

        if (!string.IsNullOrEmpty(openApiOptionsProvider.Value.LicenseUrl) &&
            Uri.TryCreate(openApiOptionsProvider.Value.LicenseUrl, UriKind.Absolute, out var uri))
        {
            info.License.Url = uri;
        }

        if (description.IsDeprecated)
        {
            info.Description += " **This API version has been deprecated!**";
        }

        return info;
    }
}
