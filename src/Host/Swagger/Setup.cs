﻿namespace Host.Swagger;

internal static class Setup
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
     => services.AddSwaggerGen(cfg =>
     {
         cfg.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
         cfg.OperationFilter<DefaultResponsesOperationFilter>();
         cfg.SchemaFilter<DateOnlySchemaFilter>();
     });

    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
