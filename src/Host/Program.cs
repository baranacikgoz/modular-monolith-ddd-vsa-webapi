using Common.Options;
using Host.Configurations;
using Host.Logging;
using Serilog;
using Common.Localization;
using NimbleMediator.ServiceExtensions;
using Common.Core;
using System.Globalization;
using Common.Caching;
using IdentityAndAuth;
using Host.Swagger;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using FluentValidation;
using System.Reflection;
using Host.Validation;
using Appointments;
using Host.Middlewares;
using Common.Core.Contracts;
using Common.Core.Implementations;
using Microsoft.Extensions.Localization;

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                .CreateLogger();

Log.Information("Server Booting Up...");

try
{
    // Create the builder and add initially required services.
    var builder = WebApplication.CreateBuilder(args);
    builder.AddConfigurations();
    builder.Services.AddCommonOptions(builder.Configuration);
    builder.UseSerilogAsLoggingProvider();

    // Add services to the container.
    builder
        .Services
            .AddHttpContextAccessor()
            .AddSingleton<RequestResponseLoggingMiddleware>()
            .AddCustomLocalization("Resources")
            .AddSingleton<ExceptionHandlingMiddleware>()
            .AddSingleton<IErrorTranslator, LocalizedErrorTranslator>(sp =>
            {
                var identityAndAuthModuleErrors = IdentityAndAuth.ErrorsToLocalize.GetErrorsAndMessages();
                var appointmentsModuleErrors = Appointments.ErrorsToLocalize.GetErrorsAndMessages();

                return new LocalizedErrorTranslator(
                    identityAndAuthModuleErrors,
                    appointmentsModuleErrors
                    );

            })
            .AddSingleton<IResultTranslator, ResultTranslator>()
            .AddCaching()
            .AddNimbleMediator(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                typeof(AppointmentsModuleAssemblyReference).Assembly,
                typeof(IdentityAndAuthModuleAssemblyReference).Assembly);
            })
            .AddValidatorsFromAssemblies(
                new List<Assembly>()
                {
                    typeof(AppointmentsModuleAssemblyReference).Assembly,
                    typeof(IdentityAndAuthModuleAssemblyReference).Assembly
                }
            )
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>())
            .AddEndpointsApiExplorer()
            .AddCustomSwagger();

    // Add modules to the container.
    builder
        .Services
            .InstallIdentityAndAuthModule(builder.Configuration)
            .InstallAppointmentsModule();

    // Build the app and configure pipeline.
    var app = builder.Build();
    app
        .UseMiddleware<RequestResponseLoggingMiddleware>()
        .UseHttpsRedirection()
        .UseCustomLocalization()
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseAuth();

    app.UseCustomSwagger();

    var rootGroup = app
        .MapGroup("/")
        .RequireAuthorization()
        .AddFluentValidationAutoValidation();

    app.UseIdentityAndAuthModule(rootGroup);
    app.UseAppointmentsModule(rootGroup);

    app.Run();
}
#pragma warning disable CA1031
catch (Exception ex)
#pragma warning restore CA1031
{
    Log.Fatal(ex, "Server terminated unexpectedly.");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
