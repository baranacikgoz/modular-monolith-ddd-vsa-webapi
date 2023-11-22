namespace Host.Swagger;

internal static class Setup
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
     => services.AddSwaggerGen(cfg =>
     {
         cfg.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
         cfg.OperationFilter<DefaultResponsesOperationFilter>();
         cfg.SchemaFilter<DateOnlySchemaFilter>();
     });

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
