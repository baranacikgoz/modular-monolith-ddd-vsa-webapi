using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.GetOwnerIdByStoreId;

public sealed record GetOwnerIdByStoreIdQuery(StoreId StoreId) : IQuery<ApplicationUserId>;

public sealed class GetOwnerIdByStoreIdQueryValidator : CustomValidator<GetOwnerIdByStoreIdQuery>
{
    public GetOwnerIdByStoreIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["Products.GetOwnerIdByStoreId.StoreId.NotEmpty"]);
    }
}
