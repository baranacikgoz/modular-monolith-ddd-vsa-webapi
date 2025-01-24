using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.GetStoreIdByOwnerId;

public sealed record GetStoreIdByOwnerIdQuery(ApplicationUserId OwnerId) : IQuery<StoreId>;

public sealed class GetStoreIdByOwnerIdQueryValidator : CustomValidator<GetStoreIdByOwnerIdQuery>
{
    public GetStoreIdByOwnerIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
                .WithMessage(localizer["Products.GetStoreIdByOwnerId.OwnerId.NotEmpty"]);
    }
}
