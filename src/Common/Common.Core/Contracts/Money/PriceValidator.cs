using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Core.Contracts.Money;
public class PriceValidator : CustomValidator<Price>
{
    public PriceValidator(IStringLocalizer localizer)
    {
        RuleFor(m => m.Currency)
            .IsInEnum()
                .WithMessage(localizer["PriceValidator.Currency.IsInEnum"]);

        RuleFor(m => m.Amount)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["PriceValidator.Amount..GreaterThanOrEqualTo(0)"]);
    }
}
