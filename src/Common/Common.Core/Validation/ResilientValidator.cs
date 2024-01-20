using FluentValidation;

namespace Common.Core.Validation;

public class ResilientValidator<TRequest> : AbstractValidator<TRequest>
{
    public ResilientValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}
