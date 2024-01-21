using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;

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
                // cfg.OperationFilter<RemoveDefaultResponseSchemaFilter>()
            });

    private static string SchemaIdGenerator(Type type)
    {
        // For example, for: "Appointments.Features.Venues.UseCases.Create.Request" return Venue.Create.Request
        // If "Features" existst but "UseCases" doesn't, return Venue.Create.Request again.
        // If neither exist, such as Common.Core.Contracts.IEmpty200Response, return IEmpty200Response.

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
