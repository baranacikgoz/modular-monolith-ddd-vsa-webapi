using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public sealed class ModulesOptions
{
    public required IReadOnlyList<string> EnabledModules { get; init; }
}

public sealed class ModulesOptionsValidator : CustomValidator<ModulesOptions>
{
    public ModulesOptionsValidator()
    {
        RuleFor(o => o.EnabledModules)
            .NotEmpty()
            .WithMessage("EnabledModules must not be empty. Use '*' to load all modules or specify explicit module names.");
    }
}
