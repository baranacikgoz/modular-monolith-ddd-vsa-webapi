using Common.Core.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal static class Setup
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        => services
            .AddEndpointsApiExplorer()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen(cfg =>
            {
                cfg.OperationFilter<SwaggerDefaultValues>();
                cfg.CustomSchemaIds(SchemaIdGenerator);
                cfg.OperationFilter<DefaultResponsesOperationFilter>();
                cfg.SchemaFilter<DateOnlySchemaFilter>();
                cfg.SchemaFilter<StronglyTypedIdSchemaFilter>();
                cfg.OperationFilter<RemoveDefaultResponseSchemaFilter>();
            });

    /// <summary>
    /// This method is used to shorten the schema names to generate shorter names for openapi generator for the front-end.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string SchemaIdGenerator(Type type)
    {
        // For example, for: "Sales.Features.UseCases.v1.Stores.Create.Request" return "Stores.Create.Request".
        // If "Features" existst but "UseCases" doesn't, return "Stores.Create.Request" as well.
        // If neither exist, such as Common.Core.Contracts.IEmpty200Response, return IEmpty200Response (the last part of the name).

        var name = type.FullName ?? type.Name;

        var splitted = name.Split('.').ToList();

        if (!splitted.Contains("Features"))
        {
            return splitted[^1];
        }

        var indexOfFeatures = splitted.IndexOf("Features");

        if (!splitted.Contains("UseCases"))
        {
            return string.Join('.', splitted[(indexOfFeatures + 1)..]).Replace('+', '.');
        }

        var indexOfUseCases = splitted.IndexOf("UseCases");
        splitted.RemoveAt(indexOfUseCases);

        return string.Join('.', splitted[(indexOfFeatures + 1)..]).Replace('+', '.');
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (app is not WebApplication webApplication)
        {
            throw new InvalidOperationException("This method can only be called on a WebApplication");
        }

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in webApplication.DescribeApiVersions())
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName);
                    }
                });
        }

        return app;
    }
}
