using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public string? Name { get; init; }

    [FromQuery] public string? Description { get; init; }

    [FromQuery] public string? Address { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Search_Name_MaximumLength,
                Constants.NameMaxLength))
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Search_Description_MaximumLength,
                Constants.DescriptionMaxLength))
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Search_Address_MaximumLength,
                Constants.AddressMaxLength))
            .When(x => x.Address is not null);
    }
}
