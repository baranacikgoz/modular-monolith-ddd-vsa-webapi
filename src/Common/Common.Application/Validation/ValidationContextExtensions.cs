using FluentValidation;
using Microsoft.Extensions.Hosting;

namespace Common.Application.Validation;

public static class ValidationContextExtensions
{
    /// <summary>
    ///     Key <see cref="Options.Setup.AddCommonOptions"/> stores the booting <see cref="IHostEnvironment"/>
    ///     under in <see cref="ValidationContext{T}.RootContextData"/>, so option validators can gate
    ///     production-only rules without taking a constructor dependency (keeps them Activator-constructible).
    /// </summary>
    public const string HostEnvironmentKey = "HostEnvironment";

    public static bool IsProduction<T>(this ValidationContext<T> context)
        => context.RootContextData.TryGetValue(HostEnvironmentKey, out var value)
           && value is IHostEnvironment environment
           && environment.IsProduction();
}
