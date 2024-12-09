using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class DatabaseOptions
{
    public required string ConnectionString { get; set; }

}

public class DatabaseOptionsValidator : CustomValidator<DatabaseOptions>
{
    public DatabaseOptionsValidator()
    {
        RuleFor(o => o.ConnectionString)
            .NotEmpty()
            .WithMessage("ConnectionString must not be empty.");
    }
}
