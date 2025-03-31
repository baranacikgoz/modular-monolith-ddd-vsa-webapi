using System.Reflection;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Options;

public static class Setup
{
    public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DatabaseOptions).Assembly; // Use an arbitrary option to get the namespace

        // Get all option classes in the specified namespace
        var optionTypes = assembly.GetTypes()
            .Where(
                t => t.IsClass
                     && !t.IsAbstract
                     && !(t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(CustomValidator<>)) // Omits validators' itselves
                     && t.Name.EndsWith("Options", StringComparison.Ordinal));

        var executedValidators = new HashSet<Type>();

        foreach (var type in optionTypes)
        {
            var sectionName = type.Name; // Use the class name as the section name
            var section = configuration.GetSection(sectionName);

            // Validate settings using FluentValidation
            var validatorType = typeof(IValidator<>).MakeGenericType(type);

            if (!executedValidators.Contains(type) && services.BuildServiceProvider().GetService(validatorType) is IValidator validator)
            {
                var optionInstance = section.Get(type) ?? throw new InvalidOperationException($"Could not bind section {sectionName} to {type.Name}");

                var validationResult = validator.Validate(new ValidationContext<object>(optionInstance));

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation for {type.Name} failed: {Environment.NewLine}{errors}");
                }

                // Mark this type as validated
                executedValidators.Add(type);
            }

            // Explicitly find the correct Configure<TOptions> method
            var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(m => m.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure)
                                      && m.GetParameters().Length == 2
                                      && m.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                                      && m.GetParameters()[1].ParameterType == typeof(IConfiguration));

            if (configureMethod != null)
            {
                var genericMethod = configureMethod.MakeGenericMethod(type);
                genericMethod.Invoke(null, [services, section]);
            }
            else
            {
                throw new InvalidOperationException($"Could not find the Configure method for type {type.Name}");
            }
        }

        return services;
    }
}
