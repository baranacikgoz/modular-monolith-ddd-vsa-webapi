using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Allow-list of client application ids accepted when a device registers a session (mobile-app-1, web-app-1, ...).</summary>
public class DevicesOptions
{
    public required IReadOnlyCollection<string> AllowedClientIds { get; init; }
}

public class DevicesOptionsValidator : CustomValidator<DevicesOptions>
{
    public DevicesOptionsValidator()
    {
        RuleFor(o => o.AllowedClientIds)
            .NotEmpty()
            .WithMessage("AllowedClientIds must not be empty.");

        RuleForEach(o => o.AllowedClientIds)
            .NotEmpty()
            .WithMessage("AllowedClientIds entries must not be empty.");
    }
}
