using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class BackgroundJobsOptions
{
    public required int PollingFrequencyInSeconds { get; set; }

    public required string DashboardPath { get; set; }
}

public class BackgroundJobsOptionsValidator : CustomValidator<BackgroundJobsOptions>
{
    public BackgroundJobsOptionsValidator()
    {
        RuleFor(x => x.PollingFrequencyInSeconds)
            .GreaterThan(0)
                .WithMessage("Polling frequency should be greater than 0.");

        RuleFor(x => x.DashboardPath)
            .NotEmpty()
                .WithMessage("Dashboard path should not be empty.");
    }
}
