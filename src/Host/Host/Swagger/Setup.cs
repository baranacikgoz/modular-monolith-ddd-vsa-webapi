using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal static class Setup
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        // The document describes enums from these MVC options, not from the minimal API options registered in
        // Infrastructure/Setup.cs, so without this converter every enum is documented as an integer.
        services.Configure<JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));

        return services
            .AddEndpointsApiExplorer()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen(cfg =>
            {
                cfg.OperationFilter<SwaggerDefaultValues>();
                cfg.CustomSchemaIds(SchemaIdGenerator);
                cfg.CustomOperationIds(OperationIdGenerator);
                cfg.OperationFilter<DefaultResponsesOperationFilter>();
                cfg.SchemaFilter<StronglyTypedIdSchemaFilter>();
                cfg.OperationFilter<RemoveDefaultResponseSchemaFilter>();
            });
    }

    private static string SchemaIdGenerator(Type type)
    {
        if (!type.IsGenericType)
        {
            return TrimToFeature(type.FullName ?? type.Name);
        }

        // A constructed generic type's FullName carries the assembly name of every argument, so it is rebuilt
        // from the definition name and the argument ids instead.
        var definition = type.GetGenericTypeDefinition();
        var definitionName = TrimToFeature(definition.FullName ?? definition.Name);
        var arityIndex = definitionName.IndexOf('`', StringComparison.Ordinal);
        if (arityIndex >= 0)
        {
            definitionName = definitionName[..arityIndex];
        }

        return $"{definitionName}Of{string.Join("And", type.GetGenericArguments().Select(SchemaIdGenerator))}";
    }

    private static string TrimToFeature(string fullName)
    {
        var splitted = fullName.Split('.');

        var cutOffIndex = Array.IndexOf(splitted, "Application");
        if (cutOffIndex == -1)
        {
            cutOffIndex = Array.IndexOf(splitted, "Endpoints");
        }

        if (cutOffIndex == -1 || cutOffIndex == splitted.Length - 1)
        {
            return fullName.Replace('+', '.');
        }

        return string.Join('.', splitted[(cutOffIndex + 1)..]).Replace('+', '.');
    }

    // Built from the HTTP method and the route, so it is unique for as long as the route is: PostV1ProductsByIdImages.
    private static string OperationIdGenerator(ApiDescription apiDescription)
    {
        var path = (apiDescription.RelativePath ?? string.Empty).Split('?', 2)[0];
        var operationId = new StringBuilder(MethodName(apiDescription.HttpMethod));

        foreach (var segment in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
        {
            if (segment.StartsWith('{') && segment.EndsWith('}'))
            {
                operationId.Append("By").Append(ToPascalCase(segment[1..^1].Split(':', 2)[0]));
            }
            else
            {
                operationId.Append(ToPascalCase(segment));
            }
        }

        return operationId.ToString();
    }

    private static string MethodName(string? httpMethod)
    {
        return httpMethod?.ToUpperInvariant() switch
        {
            "POST" => "Post",
            "PUT" => "Put",
            "PATCH" => "Patch",
            "DELETE" => "Delete",
            "HEAD" => "Head",
            "OPTIONS" => "Options",
            _ => "Get"
        };
    }

    private static string ToPascalCase(string value)
    {
        var result = new StringBuilder(value.Length);
        var capitalizeNext = true;

        foreach (var character in value)
        {
            if (!char.IsLetterOrDigit(character))
            {
                capitalizeNext = true;
                continue;
            }

            result.Append(capitalizeNext ? char.ToUpperInvariant(character) : character);
            capitalizeNext = false;
        }

        return result.ToString();
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (app is not WebApplication webApplication)
        {
            throw new InvalidOperationException("This method can only be called on a WebApplication");
        }

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in webApplication.DescribeApiVersions())
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }

                options.ConfigObject.AdditionalItems["persistAuthorization"] = true;
            });
        }

        return app;
    }
}
