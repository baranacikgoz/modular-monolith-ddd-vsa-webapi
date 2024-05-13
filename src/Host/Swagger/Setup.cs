using Microsoft.Extensions.Options;
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
        // For example, for: "Inventory.Application.Products.v1.Create.Request" return "Products.v1.Create.Request".

        var name = type.FullName ?? type.Name;

        var splitted = name.Split('.').ToList();

        var indexOfApplication = splitted.IndexOf("Application");

        return string.Join('.', splitted[(indexOfApplication + 1)..]).Replace('+', '.');
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
