using Common.Domain.ResultMonad;
using Products.Domain.ProductTemplates;
using Common.Application.CQS;
using Products.Application.Persistence;

namespace Products.Application.ProductTemplates.Features.Create;

public sealed class CreateProductTemplateCommandHandler(IProductsDbContext dbContext)
    : ICommandHandler<CreateProductTemplateCommand, ProductTemplateId>
{
    public async Task<Result<ProductTemplateId>> Handle(CreateProductTemplateCommand command, CancellationToken cancellationToken)
        => await Result<ProductTemplate>
            .Create(() => ProductTemplate.Create(command.Brand, command.Model, command.Color))
            .Tap(product => dbContext.ProductTemplates.Add(product))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(product => product.Id);
}
