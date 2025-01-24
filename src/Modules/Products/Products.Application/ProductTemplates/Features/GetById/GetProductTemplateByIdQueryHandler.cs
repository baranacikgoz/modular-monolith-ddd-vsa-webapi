using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Application.ProductTemplates.DTOs;
using Products.Application.ProductTemplates.Specifications;
using Common.Application.CQS;

namespace Products.Application.ProductTemplates.Features.GetById;

public class GetProductTemplateByIdQueryHandler(IRepository<ProductTemplate> repository) : IQueryHandler<GetProductTemplateByIdQuery, ProductTemplateDto>
{
    public async Task<Result<ProductTemplateDto>> Handle(GetProductTemplateByIdQuery query, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductTemplateByIdSpec<ProductTemplateDto>(query.Id, x => new ProductTemplateDto
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                Color = x.Color,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
                LastModifiedOn = x.LastModifiedOn,
                LastModifiedBy = x.LastModifiedBy
            }), cancellationToken);
}
