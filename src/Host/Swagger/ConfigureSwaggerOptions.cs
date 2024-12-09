using Asp.Versioning.ApiExplorer;
using Common.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal class ConfigureSwaggerOptions(
    IApiVersionDescriptionProvider provider,
    IOptions<OpenApiOptions> openApiOptionsProvider
    ) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly OpenApiOptions _openApiOptions = openApiOptionsProvider.Value;
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter your Bearer token without Bearer prefix.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = _openApiOptions.Title,
            Version = description.ApiVersion.ToString(),
            Description = _openApiOptions.Description,
            Contact = new OpenApiContact
            {
                Name = _openApiOptions.ContactName,
                Email = _openApiOptions.ContactEmail
            },
            License = new OpenApiLicense
            {
                Name = _openApiOptions.LicenseName,
                Url = Uri.TryCreate(_openApiOptions.LicenseUrl, UriKind.Absolute, out var uri) ? uri : null
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated!";
        }

        return info;
    }
}
