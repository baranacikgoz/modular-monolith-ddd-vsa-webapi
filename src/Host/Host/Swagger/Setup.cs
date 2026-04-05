using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal static class Setup
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen(cfg =>
            {
                cfg.OperationFilter<SwaggerDefaultValues>();
                cfg.CustomSchemaIds(SchemaIdGenerator);
                cfg.OperationFilter<DefaultResponsesOperationFilter>();
                cfg.SchemaFilter<StronglyTypedIdSchemaFilter>();
                cfg.OperationFilter<RemoveDefaultResponseSchemaFilter>();
            });
    }

    private static string SchemaIdGenerator(Type type)
    {
        var fullName = type.FullName ?? type.Name;

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
