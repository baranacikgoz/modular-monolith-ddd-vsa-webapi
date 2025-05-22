using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Endpoints.Stores.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery]
    public string? Name { get; init; }

    [FromQuery]
    public string? Description { get; init; }

    [FromQuery]
    public string? Address { get; init; }

    public Request() { }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
            .WithMessage(localizer["Stores.Search.Name.MaximumLength {0}", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
            .WithMessage(localizer["Stores.Search.Description.MaximumLength {0}", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .MaximumLength(Domain.Stores.Constants.AddressMaxLength)
            .WithMessage(localizer["Stores.Search.Address.MaximumLength {0}", Domain.Stores.Constants.AddressMaxLength])
            .When(x => x.Address is not null);
    }
}
